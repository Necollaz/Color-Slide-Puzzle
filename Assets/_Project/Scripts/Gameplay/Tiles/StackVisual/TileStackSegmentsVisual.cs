using System.Collections.Generic;
using UnityEngine;

public class TileStackSegmentsVisual
{
    private const string TILE_BASE_COLOR_NAME = "_BaseColor";
    private const string TILE_COLOR_NAME = "_Color";

    private readonly TileConfig _config;
    private readonly TileStackStackSizeCalculator _sizeCalculator;
    private readonly TileStackSegmentsPool _segmentsPool;
    private readonly MaterialPropertyBlock _propertyBlock;

    private readonly int _baseColorId;
    private readonly int _colorId;

    private float _segmentStepLocal = 1.0f;
    private float _segmentBaseLocalY = 0.0f;
    private float _currentWorldScaleY = 1.0f;

    public TileStackSegmentsVisual(TileConfig config, MeshRenderer rootRenderer)
    {
        _config = config;

        _segmentsPool = new TileStackSegmentsPool(config, rootRenderer);
        _sizeCalculator = new TileStackStackSizeCalculator(config, rootRenderer);

        _propertyBlock = new MaterialPropertyBlock();

        _baseColorId = Shader.PropertyToID(TILE_BASE_COLOR_NAME);
        _colorId = Shader.PropertyToID(TILE_COLOR_NAME);

        if (rootRenderer != null && _config != null)
            _segmentsPool.PrewarmSegments();
    }

    public void ApplyStackSize(Transform stackTransform)
    {
        if (_config == null || stackTransform == null || _segmentsPool.RootRenderer == null)
            return;

        bool isSpawnStack = stackTransform.TryGetComponent(out TileStackRoot _);

        if (!isSpawnStack)
        {
            _sizeCalculator.ApplyRegularStackSize(stackTransform, out _segmentStepLocal, out _segmentBaseLocalY,
                out _currentWorldScaleY);
        }
        else
        {
            _sizeCalculator.ApplySpawnStackSize(stackTransform, out _segmentStepLocal, out _segmentBaseLocalY,
                out _currentWorldScaleY);
        }
    }

    public void RebuildVisualStack(IReadOnlyList<Color> segmentColors, Transform stackTransform)
    {
        if (_segmentsPool.RootRenderer == null || _config == null || stackTransform == null)
            return;

        int count = segmentColors != null ? segmentColors.Count : 0;

        _segmentsPool.EnsureSegmentsCountFor(count);
        _segmentsPool.UpdateSegmentsActiveState(count);

        float gapLocal = _currentWorldScaleY > 0.0f ? _config.SegmentGap / _currentWorldScaleY : 0.0f;
        _segmentsPool.UpdateSegmentsPositions(count, stackTransform, _segmentBaseLocalY, _segmentStepLocal, gapLocal);

        ApplyColorsToSegments(segmentColors);
    }

    private void ApplyColorsToSegments(IReadOnlyList<Color> segmentColors)
    {
        if (segmentColors == null || segmentColors.Count == 0)
            return;

        MeshRenderer rootRenderer = _segmentsPool.RootRenderer;

        if (rootRenderer == null)
            return;

        Material tileMaterial = rootRenderer.sharedMaterial;
        
        if (tileMaterial == null)
            return;

        IReadOnlyList<MeshRenderer> segments = _segmentsPool.Segments;
        int lastColorIndex = segmentColors.Count - 1;

        for (int i = 0; i < segments.Count; i++)
        {
            MeshRenderer segment = segments[i];

            if (segment == null || !segment.gameObject.activeSelf)
                continue;

            int colorIndex = Mathf.Min(i, lastColorIndex);
            Color segmentColor = segmentColors[colorIndex];

            _propertyBlock.Clear();

            if (tileMaterial.HasProperty(_baseColorId))
                _propertyBlock.SetColor(_baseColorId, segmentColor);
            else if (tileMaterial.HasProperty(_colorId))
                _propertyBlock.SetColor(_colorId, segmentColor);

            segment.SetPropertyBlock(_propertyBlock);
        }
    }
}