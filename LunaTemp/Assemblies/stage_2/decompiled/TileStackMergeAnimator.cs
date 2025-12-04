using System;
using DG.Tweening;
using UnityEngine;

public class TileStackMergeAnimator
{
	private const float MIN_DIRECTION_LENGTH_SQR = 0.0001f;

	private const float BASE_MOVE_DURATION_SECONDS = 0.35f;

	private const float SPEED_STEP_PERCENT = 0.3f;

	private const float MIN_SPEED_MULTIPLIER = 0.01f;

	private const float JUMP_ARC_HEIGHT = 0.4f;

	private const float TILE_RANDOM_OFFSET_RADIUS = 0.03f;

	private const float CLEAR_TILE_BASE_DURATION_SECONDS = 0.15f;

	private const float HALF_TURN_DEGREES = 180f;

	private const int SINGLE_JUMP_COUNT = 1;

	private readonly TileMergeSegmentTemplate _template;

	private readonly TileMergeSegmentPool _segmentPool;

	private readonly TileMergeColorApplier _colorApplier;

	private readonly TileMergePositionCalculator _positionCalculator;

	private readonly TileEffectsPlayer _effectsPlayer;

	public TileStackMergeAnimator(TileMergeSegmentTemplate template, TileMergeSegmentPool segmentPool, TileMergeColorApplier colorApplier, TileMergePositionCalculator positionCalculator, TileEffectsPlayer effectsPlayer)
	{
		_template = template;
		_segmentPool = segmentPool;
		_colorApplier = colorApplier;
		_positionCalculator = positionCalculator;
		_effectsPlayer = effectsPlayer;
	}

	public Sequence BuildMergeSequence(TileStackView sourceStack, TileStackView targetStack, Color color, int tilesCount, int stepIndex, Action onSingleTileCompleted)
	{
		Sequence sequence = DOTween.Sequence();
		if (tilesCount <= 0 || sourceStack == null || targetStack == null || _template.SegmentTemplateRenderer == null)
		{
			return sequence;
		}
		float speedMultiplier = Mathf.Pow(1.3f, stepIndex);
		float moveDuration = 0.35f / Mathf.Max(0.01f, speedMultiplier);
		int targetInitialCount = targetStack.TilesCount;
		Vector3 segmentLocalScale = _positionCalculator.CalculateSegmentLocalScaleFromSource(sourceStack);
		Transform rootParent = _template.RootParent;
		for (int i = 0; i < tilesCount; i++)
		{
			FlyingSegment segment = _segmentPool.GetSegment();
			if (segment != null && !(segment.Transform == null) && !(segment.Renderer == null))
			{
				int sourceTilesCount = sourceStack.TilesCount;
				Vector3 startPosition = _positionCalculator.CalculateTileWorldPosition(sourceStack, sourceTilesCount, i);
				startPosition += _positionCalculator.GetRandomOffsetXZ(0.03f);
				int targetIndex = targetInitialCount + i;
				Vector3 endPosition = _positionCalculator.CalculateTileWorldPosition(targetStack, targetIndex + 1, 0);
				Transform segmentTransform = segment.Transform;
				segmentTransform.SetParent(rootParent, false);
				segmentTransform.position = startPosition;
				segmentTransform.rotation = Quaternion.identity;
				segmentTransform.localScale = segmentLocalScale;
				_colorApplier.ApplyColorToRenderer(segment.Renderer, color);
				Vector3 direction = endPosition - startPosition;
				if (direction.sqrMagnitude < 0.0001f)
				{
					direction = Vector3.forward;
				}
				direction.Normalize();
				Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);
				if (rotationAxis.sqrMagnitude < 0.0001f)
				{
					rotationAxis = Vector3.right;
				}
				rotationAxis.Normalize();
				Quaternion targetRotation = Quaternion.AngleAxis(180f, rotationAxis);
				Sequence tileSequence = DOTween.Sequence();
				Tween moveTween = segmentTransform.DOJump(endPosition, 0.4f, 1, moveDuration).SetEase(Ease.InOutQuad);
				Tween rotateTween = segmentTransform.DORotateQuaternion(targetRotation, moveDuration).SetEase(Ease.InOutQuad);
				tileSequence.Join(moveTween);
				tileSequence.Join(rotateTween);
				segment.ActiveTween = tileSequence;
				FlyingSegment capturedSegment = segment;
				tileSequence.OnComplete(delegate
				{
					capturedSegment.ActiveTween = null;
					onSingleTileCompleted?.Invoke();
					_segmentPool.ReleaseSegment(capturedSegment);
				});
				sequence.Append(tileSequence);
			}
		}
		return sequence;
	}

	public Sequence BuildClearCompletedStackSequence(TileStackView stack, Color color, int tilesToRemove)
	{
		if (stack == null || _template.SegmentTemplateRenderer == null)
		{
			return null;
		}
		int availableTiles = stack.TilesCount;
		int safeTilesToRemove = Mathf.Min(tilesToRemove, availableTiles);
		if (safeTilesToRemove <= 0)
		{
			return null;
		}
		Sequence sequence = DOTween.Sequence();
		Vector3 segmentLocalScale = _positionCalculator.CalculateSegmentLocalScaleFromSource(stack);
		Transform rootParent = _template.RootParent;
		for (int i = 0; i < safeTilesToRemove; i++)
		{
			if (!stack.TryGetBottomColorIndex(color, out var indexFromBottom))
			{
				break;
			}
			int tilesCount = stack.TilesCount;
			int localIndexFromTop = Mathf.Clamp(tilesCount - 1 - indexFromBottom, 0, tilesCount - 1);
			Vector3 tileWorldPosition = _positionCalculator.CalculateTileWorldPosition(stack, tilesCount, localIndexFromTop);
			tileWorldPosition += _positionCalculator.GetRandomOffsetXZ(0.03f);
			FlyingSegment segment = _segmentPool.GetSegment();
			if (segment == null || segment.Transform == null || segment.Renderer == null)
			{
				continue;
			}
			Transform segmentTransform = segment.Transform;
			segmentTransform.SetParent(rootParent, false);
			segmentTransform.position = tileWorldPosition;
			segmentTransform.rotation = Quaternion.identity;
			segmentTransform.localScale = segmentLocalScale;
			_colorApplier.ApplyColorToRenderer(segment.Renderer, color);
			float speedMultiplier = Mathf.Pow(1.3f, i);
			float clearDuration = 0.15f / Mathf.Max(0.01f, speedMultiplier);
			Sequence tileSequence = DOTween.Sequence();
			Tween scaleTween = segmentTransform.DOScale(Vector3.zero, clearDuration).SetEase(Ease.InBack);
			tileSequence.Append(scaleTween);
			FlyingSegment capturedSegment = segment;
			Vector3 capturedPosition = tileWorldPosition;
			tileSequence.OnComplete(delegate
			{
				stack.RemoveTilesOfColorFromBottom(color, 1);
				_effectsPlayer?.PlayTileClearEffect(capturedPosition);
				if (stack.IsEmpty)
				{
					_effectsPlayer?.PlayStackClearEffect(capturedPosition);
				}
				_segmentPool.ReleaseSegment(capturedSegment);
			});
			segment.ActiveTween = tileSequence;
			sequence.Append(tileSequence);
		}
		return sequence;
	}
}
