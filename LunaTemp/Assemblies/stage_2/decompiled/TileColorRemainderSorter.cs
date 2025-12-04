using System.Collections.Generic;
using UnityEngine;

public class TileColorRemainderSorter
{
	private const int DEFAULT_REMAINDER = 0;

	private readonly TileColorStatistics _colorStatistics;

	public TileColorRemainderSorter(TileColorStatistics colorStatistics)
	{
		_colorStatistics = colorStatistics;
	}

	public void SortByRemainder(List<Color> colors, int maxStackThreshold)
	{
		if (colors != null && colors.Count != 0)
		{
			colors.Sort(delegate(Color firstColor, Color secondColor)
			{
				int remainderToThreshold = GetRemainderToThreshold(firstColor, maxStackThreshold);
				int remainderToThreshold2 = GetRemainderToThreshold(secondColor, maxStackThreshold);
				return remainderToThreshold.CompareTo(remainderToThreshold2);
			});
		}
	}

	private int GetRemainderToThreshold(Color color, int maxStackThreshold)
	{
		if (maxStackThreshold <= 0)
		{
			return 0;
		}
		int totalCount = _colorStatistics.GetColorCount(color);
		if (totalCount <= 0)
		{
			return maxStackThreshold + 1;
		}
		int remainder = totalCount % maxStackThreshold;
		if (remainder == 0)
		{
			return maxStackThreshold;
		}
		return remainder;
	}
}
