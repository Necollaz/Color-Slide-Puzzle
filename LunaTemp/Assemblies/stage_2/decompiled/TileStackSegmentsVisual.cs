using System.Collections.Generic;
using UnityEngine;

public class TileStackSegmentsVisual
{
	private readonly TileConfig _config;

	private readonly TileStackStackSizeCalculator _sizeCalculator;

	private readonly TileStackSegmentsPool _segmentsPool;

	private readonly TileShaderPropertyIds _shaderPropertyIds;

	private readonly MaterialPropertyBlock _propertyBlock;

	private float _segmentStepLocal = 1f;

	private float _segmentBaseLocalY = 0f;

	private float _currentWorldScaleY = 1f;

	public TileStackSegmentsVisual(TileConfig config, MeshRenderer rootRenderer)
	{
		_config = config;
		_shaderPropertyIds = new TileShaderPropertyIds();
		_segmentsPool = new TileStackSegmentsPool(config, rootRenderer);
		_sizeCalculator = new TileStackStackSizeCalculator(config, rootRenderer);
		_propertyBlock = new MaterialPropertyBlock();
		if (rootRenderer != null && _config != null)
		{
			_segmentsPool.PrewarmSegments();
		}
	}

	public void ApplyStackSize(Transform stackTransform)
	{
		if (!(_config == null) && !(stackTransform == null) && !(_segmentsPool.RootRenderer == null))
		{
			if (!stackTransform.TryGetComponent<TileStackRoot>(out var _))
			{
				_sizeCalculator.ApplyRegularStackSize(stackTransform, out _segmentStepLocal, out _segmentBaseLocalY, out _currentWorldScaleY);
			}
			else
			{
				_sizeCalculator.ApplySpawnStackSize(stackTransform, out _segmentStepLocal, out _segmentBaseLocalY, out _currentWorldScaleY);
			}
		}
	}

	public void RebuildVisualStack(IReadOnlyList<Color> segmentColors, Transform stackTransform)
	{
		if (!(_segmentsPool.RootRenderer == null) && !(_config == null) && !(stackTransform == null))
		{
			int count = segmentColors?.Count ?? 0;
			_segmentsPool.EnsureSegmentsCountFor(count);
			_segmentsPool.UpdateSegmentsActiveState(count);
			float gapLocal = ((_currentWorldScaleY > 0f) ? (_config.SegmentGap / _currentWorldScaleY) : 0f);
			_segmentsPool.UpdateSegmentsPositions(count, stackTransform, _segmentBaseLocalY, _segmentStepLocal, gapLocal);
			ApplyColorsToSegments(segmentColors);
		}
	}

	private void ApplyColorsToSegments(IReadOnlyList<Color> segmentColors)
	{
		if (segmentColors == null || segmentColors.Count == 0)
		{
			return;
		}
		MeshRenderer rootRenderer = _segmentsPool.RootRenderer;
		if (rootRenderer == null)
		{
			return;
		}
		Material tileMaterial = rootRenderer.sharedMaterial;
		if (tileMaterial == null)
		{
			return;
		}
		IReadOnlyList<MeshRenderer> segments = _segmentsPool.Segments;
		int lastColorIndex = segmentColors.Count - 1;
		for (int i = 0; i < segments.Count; i++)
		{
			MeshRenderer segment = segments[i];
			if (!(segment == null) && segment.gameObject.activeSelf)
			{
				int colorIndex = Mathf.Min(i, lastColorIndex);
				Color segmentColor = segmentColors[colorIndex];
				_propertyBlock.Clear();
				if (tileMaterial.HasProperty(_shaderPropertyIds.BaseColorId))
				{
					_propertyBlock.SetColor(_shaderPropertyIds.BaseColorId, segmentColor);
				}
				else if (tileMaterial.HasProperty(_shaderPropertyIds.ColorId))
				{
					_propertyBlock.SetColor(_shaderPropertyIds.ColorId, segmentColor);
				}
				segment.SetPropertyBlock(_propertyBlock);
			}
		}
	}
}
