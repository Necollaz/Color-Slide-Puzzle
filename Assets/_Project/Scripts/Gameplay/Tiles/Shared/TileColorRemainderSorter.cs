using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileColorRemainderSorter
{
    private const int DEFAULT_REMAINDER = 0;

    private readonly TileColorStatistics _colorStatistics;

    [Inject]
    public TileColorRemainderSorter(TileColorStatistics colorStatistics)
    {
        _colorStatistics = colorStatistics;
    }

    public void SortByRemainder(List<Color> colors, int maxStackThreshold)
    {
        if (colors == null || colors.Count == 0)
            return;

        colors.Sort((firstColor, secondColor) =>
        {
            int firstRemainder = GetRemainderToThreshold(firstColor, maxStackThreshold);
            int secondRemainder = GetRemainderToThreshold(secondColor, maxStackThreshold);

            return firstRemainder.CompareTo(secondRemainder);
        });
    }

    private int GetRemainderToThreshold(Color color, int maxStackThreshold)
    {
        if (maxStackThreshold <= 0)
            return DEFAULT_REMAINDER;

        int totalCount = _colorStatistics.GetColorCount(color);
        int remainder = totalCount % maxStackThreshold;

        if (remainder == 0 && totalCount > 0)
            return maxStackThreshold;

        return remainder;
    }
}