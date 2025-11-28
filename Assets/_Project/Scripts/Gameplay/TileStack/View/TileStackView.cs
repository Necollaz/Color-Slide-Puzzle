using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileStackView : MonoBehaviour
{
    public const int MAX_STACK_SIZE = 10;

    [SerializeField] private MeshRenderer _renderer;

    private readonly List<MeshRenderer> _segments = new List<MeshRenderer>();
    private readonly List<Color> _segmentColors = new List<Color>();

    private readonly int _baseColorId = Shader.PropertyToID("_BaseColor");
    private readonly int _colorId = Shader.PropertyToID("_Color");

    private TileConfig _config;
    private MaterialPropertyBlock _propertyBlock;
    private int _count;

    [Inject]
    public void Construct(TileConfig config)
    {
        _config = config;
    }
    
    private void Awake()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();

        _propertyBlock = new MaterialPropertyBlock();

        transform.localScale = Vector3.one;

        if (_renderer != null)
            _segments.Add(_renderer);
    }

    public void Initialize(IReadOnlyList<Color> segmentColors)
    {
        _segmentColors.Clear();
        
        if (segmentColors != null && segmentColors.Count > 0)
        {
            int maxCount = Mathf.Min(segmentColors.Count, _config.MaxStackSize);

            for (int i = 0; i < maxCount; i++)
                _segmentColors.Add(segmentColors[i]);
        }
        else
        {
            _segmentColors.Add(Color.white);
        }

        _count = _segmentColors.Count;

        TrySetSize();
        RebuildVisualStack();
    }

    private void TrySetSize()
    {
        if (_config == null)
            return;

        Vector3 scale = Vector3.one;
        scale.x = _config.XzScaleFactor;
        scale.z = _config.XzScaleFactor;
        scale.y = _config.StackHeight;

        transform.localScale = scale;
    }

    private void RebuildVisualStack()
    {
        if (_renderer == null || _config == null)
            return;
        
        while (_segments.Count < _count)
        {
            GameObject segmentObject = Instantiate(_renderer.gameObject, _renderer.transform.parent);

            if (!segmentObject.TryGetComponent(out MeshRenderer newSegmentRenderer))
            {
                segmentObject.SetActive(false);
                
                continue;
            }

            newSegmentRenderer.transform.localScale = _renderer.transform.localScale;
            _segments.Add(newSegmentRenderer);
        }
        
        for (int i = 0; i < _segments.Count; i++)
        {
            bool isActive = i < _count && _segments[i] != null;

            if (_segments[i] != null)
                _segments[i].gameObject.SetActive(isActive);
        }
        
        float baseY = _config.YOffset + _config.SegmentHeight * 0.5f;

        for (int i = 0; i < _count; i++)
        {
            MeshRenderer segment = _segments[i];
            
            if (segment == null)
                continue;

            Transform tileTransform = segment.transform;
            Vector3 localPosition = tileTransform.localPosition;

            localPosition.x = 0f;
            localPosition.z = 0f;
            localPosition.y = baseY + i * (_config.SegmentHeight + _config.SegmentGap);

            tileTransform.localPosition = localPosition;
        }
        
        TryApplyColorsToSegments();
    }
    
    private void TryApplyColorsToSegments()
    {
        if (_segments.Count == 0 || _segmentColors.Count == 0)
            return;

        Material tileMaterial = _renderer.sharedMaterial;
        
        if (tileMaterial == null)
            return;

        int lastColorIndex = _segmentColors.Count - 1;

        for (int i = 0; i < _segments.Count; i++)
        {
            MeshRenderer segment = _segments[i];
            
            if (segment == null || !segment.gameObject.activeSelf)
                continue;

            Color segmentColor = _segmentColors[Mathf.Min(i, lastColorIndex)];

            _propertyBlock.Clear();

            if (tileMaterial.HasProperty(_baseColorId))
                _propertyBlock.SetColor(_baseColorId, segmentColor);
            else if (tileMaterial.HasProperty(_colorId))
                _propertyBlock.SetColor(_colorId, segmentColor);

            segment.SetPropertyBlock(_propertyBlock);
        }
    }
}