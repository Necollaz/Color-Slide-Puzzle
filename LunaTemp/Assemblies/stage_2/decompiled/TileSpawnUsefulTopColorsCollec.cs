using System.Collections.Generic;
using UnityEngine;

public class TileSpawnUsefulTopColorsCollector
{
	private readonly Grid _grid;

	private readonly GridCellsBuilder _cellsBuilder;

	private readonly GridNeighborOffsetProvider _gridNeighborOffset;

	public TileSpawnUsefulTopColorsCollector(Grid grid, GridCellsBuilder cellsBuilder, GridNeighborOffsetProvider gridNeighborOffset)
	{
		_grid = grid;
		_cellsBuilder = cellsBuilder;
		_gridNeighborOffset = gridNeighborOffset;
	}

	public void CollectUsefulTopColors(HashSet<Color> result)
	{
		result.Clear();
		IReadOnlyDictionary<Vector2Int, HexCellView> cells = _cellsBuilder.Cells;
		if (cells == null || cells.Count == 0)
		{
			return;
		}
		Vector2Int[] neighborOffsets = _gridNeighborOffset.GetNeighborOffsets(_grid);
		foreach (KeyValuePair<Vector2Int, HexCellView> pair in cells)
		{
			Vector2Int coordinates = pair.Key;
			HexCellView cellView = pair.Value;
			if (cellView == null)
			{
				continue;
			}
			TileStackView stackView = cellView.GetComponentInChildren<TileStackView>();
			if (stackView != null && stackView.gameObject.activeSelf && !stackView.IsEmpty)
			{
				continue;
			}
			for (int index = 0; index < neighborOffsets.Length; index++)
			{
				Vector2Int neighborCoordinates = coordinates + neighborOffsets[index];
				if (cells.TryGetValue(neighborCoordinates, out var neighborCell) && !(neighborCell == null))
				{
					TileStackView neighborStack = neighborCell.GetComponentInChildren<TileStackView>();
					if (!(neighborStack == null) && !neighborStack.IsEmpty && neighborStack.gameObject.activeSelf && neighborStack.TryGetTopColor(out var neighborTopColor))
					{
						result.Add(neighborTopColor);
					}
				}
			}
		}
	}
}
