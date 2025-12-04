using System.Collections.Generic;
using UnityEngine;

public class TileStackSegmentsPool
{
    private const string SEGMENT_OBJECT_NAME = "TileSegment";
    private const float ZERO_POSITION = 0.0f;
    private const int MIN_SEGMENTS_TO_PREWARM = 1;

    private readonly TileConfig _config;
    private readonly ObjectPool<MeshRenderer> _segmentPool;
    private readonly MeshRenderer _rootRenderer;
    
    private readonly Vector3 _segmentLocalScale = Vector3.one;

    private readonly List<MeshRenderer> _segments = new List<MeshRenderer>();

    public TileStackSegmentsPool(TileConfig config, MeshRenderer rootRenderer)
    {
        _config = config;
        _rootRenderer = rootRenderer;

        if (_rootRenderer != null)
        {
            _segments.Add(_rootRenderer);
            _segmentLocalScale = _rootRenderer.transform.localScale;
        }

        int initialCapacity = _config != null ? _config.MaxStackSize : 0;

        _segmentPool = new ObjectPool<MeshRenderer>(CreateSegmentInstance, OnGetSegment, OnReleaseSegment, initialCapacity);
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
            MeshRenderer newSegment = _segmentPool.Get();

            if (newSegment == null)
                break;

            if (newSegment.transform.parent != parent)
                newSegment.transform.SetParent(parent, worldPositionStays: false);

            _segments.Add(newSegment);
        }
    }

    private MeshRenderer CreateSegmentInstance()
    {
        if (_rootRenderer == null)
            return null;

        Transform parent = _rootRenderer.transform.parent;

        MeshRenderer newSegmentRenderer = Object.Instantiate(_rootRenderer, parent);
        newSegmentRenderer.name = SEGMENT_OBJECT_NAME;

        if (newSegmentRenderer.TryGetComponent(out TileStackView strayView))
            strayView.enabled = false;

        if (newSegmentRenderer.TryGetComponent(out Collider strayCollider))
            strayCollider.enabled = false;

        newSegmentRenderer.transform.localScale = _segmentLocalScale;

        return newSegmentRenderer;
    }

    private void OnGetSegment(MeshRenderer segment)
    {
        if (segment == null)
            return;

        segment.transform.localScale = _segmentLocalScale;
        segment.gameObject.SetActive(true);
    }

    private void OnReleaseSegment(MeshRenderer segment)
    {
        if (segment == null)
            return;

        segment.gameObject.SetActive(false);
    }
}