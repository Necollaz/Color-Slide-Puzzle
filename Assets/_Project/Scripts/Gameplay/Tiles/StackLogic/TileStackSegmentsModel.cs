using System.Collections.Generic;
using UnityEngine;

public class TileStackSegmentsModel
{
    private readonly TileConfig _config;
    private readonly List<Color> _segmentColors = new List<Color>();
    private readonly Dictionary<Color, int> _colorCounts = new Dictionary<Color, int>();

    public TileStackSegmentsModel(TileConfig config)
    {
        _config = config;
    }

    public IReadOnlyList<Color> SegmentColors => _segmentColors;
    public bool IsEmpty => _segmentColors.Count == 0;
    public int Count => _segmentColors.Count;

    public void Initialize(IReadOnlyList<Color> segmentColors)
    {
        _segmentColors.Clear();

        if (segmentColors != null && segmentColors.Count > 0)
        {
            int maxCount = Mathf.Min(segmentColors.Count, _config.MaxStackSize - 1);

            for (int i = 0; i < maxCount; i++)
                _segmentColors.Add(segmentColors[i]);
        }
        else
        {
            _segmentColors.Add(Color.white);
        }
    }

    public void AddTilesOnTop(Color color, int count)
    {
        if (_config == null || count <= 0)
            return;

        for (int i = 0; i < count; i++)
            _segmentColors.Add(color);

        TryClearCompletedColors();
    }

    public int ExtractAllTilesOfColor(Color color)
    {
        if (_segmentColors.Count == 0)
            return 0;

        int removedCount = 0;

        while (_segmentColors.Count > 0)
        {
            int lastIndex = _segmentColors.Count - 1;

            if (_segmentColors[lastIndex] != color)
                break;

            _segmentColors.RemoveAt(lastIndex);
            
            removedCount++;
        }

        return removedCount;
    }

    public bool TryGetTopColor(out Color color)
    {
        if (_segmentColors.Count == 0)
        {
            color = default;
            
            return false;
        }

        color = _segmentColors[_segmentColors.Count - 1];
        
        return true;
    }

    public void ForceTopColor(Color color)
    {
        if (_segmentColors.Count == 0)
            return;

        int lastIndex = _segmentColors.Count - 1;
        _segmentColors[lastIndex] = color;
    }

    public void AppendColorsToDictionary(Dictionary<Color, int> colorCounts)
    {
        if (colorCounts == null || _segmentColors.Count == 0)
            return;

        for (int i = 0; i < _segmentColors.Count; i++)
        {
            Color color = _segmentColors[i];

            if (colorCounts.TryGetValue(color, out int existing))
                colorCounts[color] = existing + 1;
            else
                colorCounts[color] = 1;
        }
    }

    private void TryClearCompletedColors()
    {
        if (_config == null || _segmentColors.Count == 0)
            return;

        int threshold = _config.MaxStackSize;

        _colorCounts.Clear();

        for (int i = 0; i < _segmentColors.Count; i++)
        {
            Color color = _segmentColors[i];

            if (_colorCounts.TryGetValue(color, out int count))
                _colorCounts[color] = count + 1;
            else
                _colorCounts[color] = 1;
        }

        bool removedAny = false;
        List<Color> colorsToClear = null;

        foreach (KeyValuePair<Color, int> pair in _colorCounts)
        {
            if (pair.Value >= threshold)
            {
                colorsToClear ??= new List<Color>();
                
                colorsToClear.Add(pair.Key);
            }
        }

        if (colorsToClear == null || colorsToClear.Count == 0)
            return;

        for (int c = 0; c < colorsToClear.Count; c++)
        {
            Color removeColor = colorsToClear[c];

            for (int i = _segmentColors.Count - 1; i >= 0; i--)
            {
                if (_segmentColors[i] == removeColor)
                {
                    _segmentColors.RemoveAt(i);
                    
                    removedAny = true;
                }
            }
        }

        if (!removedAny)
            return;
    }
}