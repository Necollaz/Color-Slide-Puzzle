using System.Collections.Generic;
using UnityEngine;

public class TileColorStatistics
{
	private readonly GridCellsBuilder _cellsBuilder;

	private readonly Dictionary<Color, int> _colorCounts = new Dictionary<Color, int>();

	public TileColorStatistics(GridCellsBuilder cellsBuilder)
	{
		_cellsBuilder = cellsBuilder;
	}

	public void Rebuild()
	{
		_colorCounts.Clear();
		IReadOnlyDictionary<Vector2Int, HexCellView> cells = _cellsBuilder.Cells;
		if (cells == null || cells.Count == 0)
		{
			return;
		}
		foreach (HexCellView cellView in cells.Values)
		{
			if (!(cellView == null))
			{
				TileStackView stackView = cellView.GetComponentInChildren<TileStackView>();
				if (!(stackView == null))
				{
					stackView.AppendColorsToDictionary(_colorCounts);
				}
			}
		}
	}

	public int GetColorCount(Color color)
	{
		if (_colorCounts.TryGetValue(color, out var count))
		{
			return count;
		}
		return 0;
	}
}
