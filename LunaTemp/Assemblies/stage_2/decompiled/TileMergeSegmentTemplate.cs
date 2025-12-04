using UnityEngine;

public class TileMergeSegmentTemplate
{
	private const string SEGMENT_TEMPLATE_NAME = "TileSegment_FlyingTemplate";

	private const string ROOT_OBJECT_NAME = "TileMergeAnimationsRoot";

	private readonly MeshRenderer _segmentTemplateRenderer;

	private readonly Transform _rootParent;

	private readonly Vector3 _segmentLocalScale = Vector3.one;

	public MeshRenderer SegmentTemplateRenderer => _segmentTemplateRenderer;

	public Transform RootParent => _rootParent;

	public Vector3 SegmentLocalScale => _segmentLocalScale;

	public TileMergeSegmentTemplate(TileStackView cellStackPrefab, Grid grid)
	{
		MeshRenderer prefabRenderer = ((cellStackPrefab != null) ? cellStackPrefab.GetComponentInChildren<MeshRenderer>() : null);
		if (prefabRenderer != null)
		{
			_segmentTemplateRenderer = Object.Instantiate(prefabRenderer);
			_segmentTemplateRenderer.name = "TileSegment_FlyingTemplate";
			_segmentTemplateRenderer.gameObject.SetActive(false);
			if (_segmentTemplateRenderer.TryGetComponent<TileStackView>(out var strayView))
			{
				strayView.enabled = false;
			}
			if (_segmentTemplateRenderer.TryGetComponent<Collider>(out var strayCollider))
			{
				strayCollider.enabled = false;
			}
			_segmentLocalScale = _segmentTemplateRenderer.transform.localScale;
		}
		_rootParent = new GameObject("TileMergeAnimationsRoot").transform;
		if (grid != null)
		{
			_rootParent.SetParent(grid.transform, false);
		}
	}
}
