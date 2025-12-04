using System.Collections.Generic;
using UnityEngine;

public class TileStackView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private TileConfig _config;

    private TileStackRuntime _runtime;

    public bool IsEmpty => _runtime == null || _runtime.IsEmpty;
    public int TilesCount => _runtime == null ? 0 : _runtime.TilesCount;

    private void Awake()
    {
        if (_renderer == null)
            _renderer = GetComponentInChildren<MeshRenderer>();

        if (_renderer != null && _config != null)
            _runtime = new TileStackRuntime(_config, _renderer, transform);
    }

    public int CountTopTilesOfColor(Color color) => _runtime == null ? 0 : _runtime.CountTopTilesOfColor(color);

    public int RemoveTopTiles(int count) => _runtime == null ? 0 : _runtime.RemoveTopTiles(count);

    public int RemoveTilesOfColorFromBottom(Color color, int count) => _runtime == null ? 0
        : _runtime.RemoveTilesOfColorFromBottom(color, count);

    public bool TryGetBottomColorIndex(Color color, out int indexFromBottom)
    {
        if (_runtime == null)
        {
            indexFromBottom = -1;
            
            return false;
        }

        return _runtime.TryGetBottomColorIndex(color, out indexFromBottom);
    }

    public bool TryGetTopColor(out Color color)
    {
        color = default;

        if (_runtime == null)
            return false;

        return _runtime.TryGetTopColor(out color);
    }

    public bool TryGetCompletedColor(out Color color, out int completedCount)
    {
        color = default;
        completedCount = 0;

        if (_runtime == null)
            return false;

        return _runtime.TryGetCompletedColor(out color, out completedCount);
    }

    public void Initialize(IReadOnlyList<Color> segmentColors)
    {
        if (_runtime == null)
            return;

        _runtime.Initialize(segmentColors);
    }

    public void AddTilesOnTop(Color color, int count) => _runtime?.AddTilesOnTop(color, count);

    public void ForceTopColor(Color color) => _runtime?.ForceTopColor(color);

    public void AppendColorsToDictionary(Dictionary<Color, int> colorCounts) =>
        _runtime?.AppendColorsToDictionary(colorCounts);
}