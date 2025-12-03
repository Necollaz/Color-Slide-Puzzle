using System.Collections.Generic;
using UnityEngine;

public class TileStackSegmentsPool
{
    private const string SEGMENT_OBJECT_NAME = "TileSegment";
    private const float ZERO_POSITION = 0.0f;
    private const int MIN_SEGMENTS_TO_PREWARM = 1;

    private readonly TileConfig _config;
    private readonly MeshRenderer _rootRenderer;
    private readonly List<MeshRenderer> _segments = new List<MeshRenderer>();

    private readonly Vector3 _segmentLocalScale = Vector3.one;

    public TileStackSegmentsPool(TileConfig config, MeshRenderer rootRenderer)
    {
        _config = config;
        _rootRenderer = rootRenderer;

        if (_rootRenderer != null)
        {
            _segments.Add(_rootRenderer);
            _segmentLocalScale = _rootRenderer.transform.localScale;
        }
    }

    public IReadOnlyList<MeshRenderer> Segments => _segments;
    public MeshRenderer RootRenderer => _rootRenderer;

    public void PrewarmSegments()
    {
        int requiredCount = Mathf.Max(MIN_SEGMENTS_TO_PREWARM, _config.MaxStackSize);
        
        EnsureSegmentsCount(requiredCount);
    }

    public void EnsureSegmentsCountFor(int requiredCount)
    {
        EnsureSegmentsCount(requiredCount);
    }

    public void UpdateSegmentsActiveState(int count)
    {
        for (int i = 0; i < _segments.Count; i++)
        {
            MeshRenderer segment = _segments[i];

            if (segment == null)
                continue;

            bool isActive = i < count;
            segment.gameObject.SetActive(isActive);
            segment.transform.localScale = _segmentLocalScale;
        }
    }

    public void UpdateSegmentsPositions(int count, Transform stackTransform, float segmentBaseLocalY,
        float segmentStepLocal, float gapLocal)
    {
        if (stackTransform == null)
            return;

        for (int i = 0; i < count; i++)
        {
            MeshRenderer segment = _segments[i];

            if (segment == null)
                continue;

            Transform tileTransform = segment.transform;
            Vector3 localPosition = tileTransform.localPosition;

            localPosition.x = ZERO_POSITION;
            localPosition.z = ZERO_POSITION;
            localPosition.y = segmentBaseLocalY + i * (segmentStepLocal + gapLocal);

            tileTransform.localPosition = localPosition;
        }
    }

    private void EnsureSegmentsCount(int requiredCount)
    {
        if (requiredCount <= 0 || _rootRenderer == null)
            return;

        Transform parent = _rootRenderer.transform.parent;

        while (_segments.Count < requiredCount)
        {
            GameObject segmentObject = Object.Instantiate(_rootRenderer.gameObject, parent);
            segmentObject.name = SEGMENT_OBJECT_NAME;

            if (segmentObject.TryGetComponent(out TileStackView strayView))
                strayView.enabled = false;

            if (segmentObject.TryGetComponent(out Collider strayCollider))
                strayCollider.enabled = false;

            if (!segmentObject.TryGetComponent(out MeshRenderer newSegmentRenderer))
            {
                segmentObject.SetActive(false);
                
                continue;
            }

            newSegmentRenderer.transform.localScale = _segmentLocalScale;
            _segments.Add(newSegmentRenderer);
        }
    }
}