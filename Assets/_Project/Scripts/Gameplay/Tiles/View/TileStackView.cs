using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileStackView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;

    private TileConfig _config;
    private TileStackSegmentsModel _model;
    private TileStackSegmentsVisual _visual;

    [Inject]
    public void Construct(TileConfig config)
    {
        _config = config;
    }

    public bool IsEmpty => _model == null || _model.IsEmpty;

    private void Awake()
    {
        if (_renderer == null)
            _renderer = GetComponentInChildren<MeshRenderer>();
    }

    public int ExtractAllTilesOfColor(Color color)
    {
        EnsureCoreInitialized();

        if (_model == null || _visual == null)
            return 0;
        
        int removedCount = _model.ExtractAllTilesOfColor(color);

        if (removedCount > 0)
        {
            if (_model.IsEmpty)
                gameObject.SetActive(false);
            else
                _visual.RebuildVisualStack(_model.SegmentColors, transform);
        }

        return removedCount;
    }

    public bool TryGetTopColor(out Color color)
    {
        EnsureCoreInitialized();

        if (_model == null)
        {
            color = default;
            
            return false;
        }

        bool result = _model.TryGetTopColor(out color);

        return result;
    }
    
    public void Initialize(IReadOnlyList<Color> segmentColors)
    {
        EnsureCoreInitialized();

        if (_model == null || _visual == null)
            return;

        _model.Initialize(segmentColors);
        _visual.ApplyStackSize(transform);
        _visual.RebuildVisualStack(_model.SegmentColors, transform);

        gameObject.SetActive(!_model.IsEmpty);
    }
    
    public void AddTilesOnTop(Color color, int count)
    {
        EnsureCoreInitialized();

        if (_model == null || _visual == null)
            return;

        if (count <= 0)
            return;

        _model.AddTilesOnTop(color, count);

        if (_model.IsEmpty)
        {
            gameObject.SetActive(false);
            
            return;
        }

        _visual.RebuildVisualStack(_model.SegmentColors, transform);
    }

    public void ForceTopColor(Color color)
    {
        EnsureCoreInitialized();

        if (_model == null || _visual == null)
            return;

        _model.ForceTopColor(color);

        if (_model.IsEmpty)
            gameObject.SetActive(false);
        else
            _visual.RebuildVisualStack(_model.SegmentColors, transform);
    }

    public void AppendColorsToDictionary(Dictionary<Color, int> colorCounts)
    {
        EnsureCoreInitialized();

        if (_model == null)
            return;

        _model.AppendColorsToDictionary(colorCounts);
    }
    
    private void EnsureCoreInitialized()
    {
        if (_model != null && _visual != null)
            return;

        if (_config == null)
            return;

        if (_renderer == null)
            _renderer = GetComponentInChildren<MeshRenderer>();

        _model = new TileStackSegmentsModel(_config);
        _visual = new TileStackSegmentsVisual(_config, _renderer);
    }
}