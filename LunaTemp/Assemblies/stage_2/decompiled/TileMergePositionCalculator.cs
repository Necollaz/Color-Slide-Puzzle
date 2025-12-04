using System;
using UnityEngine;

public class TileMergePositionCalculator
{
	private const float MIN_SCALE_EPSILON = 0.0001f;

	private const float FULL_CIRCLE_RADIANS = 3.14159265f * 2f;

	private const float ZERO_ANGLE = 0f;

	private const float ZERO_RADIUS = 0f;

	private readonly TileConfig _tileConfig;

	private readonly TileMergeSegmentTemplate _template;

	public TileMergePositionCalculator(TileConfig tileConfig, TileMergeSegmentTemplate template)
	{
		_tileConfig = tileConfig;
		_template = template;
	}

	public Vector3 CalculateTileWorldPosition(TileStackView stackView, int tilesCount, int localIndex)
	{
		if (stackView == null || _tileConfig == null)
		{
			return Vector3.zero;
		}
		Transform stackTransform = stackView.transform;
		float segmentStep = _tileConfig.SegmentHeight + _tileConfig.SegmentGap;
		float baseOffset = _tileConfig.YOffset + segmentStep * 0.5f;
		if (tilesCount <= 0)
		{
			tilesCount = 1;
		}
		int clampedIndexFromTop = Mathf.Clamp(localIndex, 0, tilesCount - 1);
		int indexFromBottom = tilesCount - 1 - clampedIndexFromTop;
		float height = baseOffset + segmentStep * (float)indexFromBottom;
		Vector3 worldPosition = stackTransform.position;
		worldPosition.y += height;
		return worldPosition;
	}

	public Vector3 CalculateSegmentLocalScaleFromSource(TileStackView sourceStack)
	{
		if (sourceStack == null)
		{
			return _template.SegmentLocalScale;
		}
		MeshRenderer sourceRenderer = sourceStack.GetComponentInChildren<MeshRenderer>(true);
		if (sourceRenderer == null)
		{
			return _template.SegmentLocalScale;
		}
		Vector3 targetWorldScale = sourceRenderer.transform.lossyScale;
		Transform rootParent = _template.RootParent;
		Vector3 parentWorldScale = ((rootParent != null) ? rootParent.lossyScale : Vector3.one);
		Vector3 localScale = default(Vector3);
		localScale.x = SafeDiv(targetWorldScale.x, parentWorldScale.x);
		localScale.y = SafeDiv(targetWorldScale.y, parentWorldScale.y);
		localScale.z = SafeDiv(targetWorldScale.z, parentWorldScale.z);
		return localScale;
	}

	public Vector3 GetRandomOffsetXZ(float radius)
	{
		if (radius <= 0f)
		{
			return Vector3.zero;
		}
		float angle = UnityEngine.Random.Range(0f, 3.14159265f * 2f);
		float length = UnityEngine.Random.Range(0f, radius);
		return new Vector3(Mathf.Cos(angle) * length, 0f, Mathf.Sin(angle) * length);
	}

	private float SafeDiv(float value, float divider)
	{
		if (Mathf.Abs(divider) < 0.0001f)
		{
			return value;
		}
		return value / divider;
	}
}
