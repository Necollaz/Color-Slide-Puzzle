using UnityEngine;
using Zenject;
using DG.Tweening;

public class TileMergeSegmentPool
{
    private const string SEGMENT_INSTANCE_NAME = "TileSegment_Flying";
    
    private readonly TileMergeSegmentTemplate _template;
    private readonly ObjectPool<FlyingSegment> _pool;

    [Inject]
    public TileMergeSegmentPool(TileMergeSegmentTemplate template)
    {
        _template = template;
        _pool = new ObjectPool<FlyingSegment>(CreateSegment, OnGetSegment, OnReleaseSegment);
    }

    public FlyingSegment GetSegment() => _pool.Get();

    public void ReleaseSegment(FlyingSegment segment) => _pool.Release(segment);

    private FlyingSegment CreateSegment()
    {
        MeshRenderer templateRenderer = _template.SegmentTemplateRenderer;
        Transform rootParent = _template.RootParent;

        if (templateRenderer == null || rootParent == null)
            return null;

        MeshRenderer instanceRenderer = Object.Instantiate(templateRenderer, rootParent);
        instanceRenderer.name = SEGMENT_INSTANCE_NAME;
        instanceRenderer.gameObject.SetActive(false);

        Transform segmentTransform = instanceRenderer.transform;

        FlyingSegment segment = new FlyingSegment
        {
            Transform = segmentTransform,
            Renderer = instanceRenderer,
            IsBusy = false
        };

        return segment;
    }

    private void OnGetSegment(FlyingSegment segment)
    {
        if (segment == null || segment.Transform == null)
            return;

        segment.IsBusy = true;
        segment.ActiveTween = null;
        segment.Transform.gameObject.SetActive(true);
    }

    private void OnReleaseSegment(FlyingSegment segment)
    {
        if (segment == null || segment.Transform == null)
            return;

        if (segment.ActiveTween != null && segment.ActiveTween.IsActive())
            segment.ActiveTween.Kill();

        segment.ActiveTween = null;
        segment.IsBusy = false;
        segment.Transform.gameObject.SetActive(false);
    }
}