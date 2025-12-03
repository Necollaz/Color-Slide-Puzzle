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

    public int CountTopTilesOfColor(Color color)
    {
        int count = 0;

        for (int i = _segmentColors.Count - 1; i >= 0; i--)
        {
            if (_segmentColors[i] != color)
                break;

            count++;
        }

        return count;
    }

    public int RemoveTopTiles(int count)
    {
        if (count <= 0 || _segmentColors.Count == 0) return 0;

        int removed = 0;

        for (int i = 0; i < count && _segmentColors.Count > 0; i++)
        {
            int lastIndex = _segmentColors.Count - 1;
            _segmentColors.RemoveAt(lastIndex);
            
            removed++;
        }

        return removed;
    }
    
    public int RemoveColorFromBottom(Color color, int count)
    {
        if (count <= 0 || _segmentColors.Count == 0)
            return 0;

        int removed = 0;

        for (int i = 0; i < _segmentColors.Count && removed < count;)
        {
            if (_segmentColors[i] == color)
            {
                _segmentColors.RemoveAt(i);
                
                removed++;
            }
            else
            {
                i++;
            }
        }

        return removed;
    }
    
    public bool TryGetBottomColorIndex(Color color, out int indexFromBottom)
    {
        indexFromBottom = -1;

        if (_segmentColors.Count == 0)
            return false;

        for (int i = 0; i < _segmentColors.Count; i++)
        {
            if (_segmentColors[i] == color)
            {
                indexFromBottom = i;
                return true;
            }
        }

        return false;
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
    
    public bool TryGetCompletedColor(out Color color, out int completedCount)
    {
        color = default;
        completedCount = 0;

        if (_config == null || _segmentColors.Count == 0)
            return false;

        int threshold = _config.MaxStackSize;

        _colorCounts.Clear();

        for (int i = 0; i < _segmentColors.Count; i++)
        {
            Color c = _segmentColors[i];

            if (_colorCounts.TryGetValue(c, out int count))
                _colorCounts[c] = count + 1;
            else
                _colorCounts[c] = 1;
        }

        Color bestColor = default;
        int bestCount = 0;

        foreach (KeyValuePair<Color, int> pair in _colorCounts)
        {
            if (pair.Value >= threshold && pair.Value > bestCount)
            {
                bestColor = pair.Key;
                bestCount = pair.Value;
            }
        }

        if (bestCount <= 0)
            return false;

        color = bestColor;
        completedCount = bestCount;

        return true;
    }
    
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
}