using System.Collections.Generic;
using UnityEngine;

public class TileMatchGroupFinder
{
	private const int MIN_MATCH_GROUP_SIZE = 2;

	private readonly Grid _grid;

	private readonly GridCellsBuilder _cellsBuilder;

	private readonly GridNeighborOffsetProvider _gridNeighborOffset;

	private readonly TileStackInCellFinder _stackFinder;

	private readonly HashSet<HexCellView> _visitedCells = new HashSet<HexCellView>();

	private readonly Queue<HexCellView> _bfsQueue = new Queue<HexCellView>();

	private readonly List<List<HexCellView>> _foundGroups = new List<List<HexCellView>>();

	private readonly List<List<HexCellView>> _groupPool = new List<List<HexCellView>>();

	public IReadOnlyList<List<HexCellView>> FoundGroups => _foundGroups;

	public TileMatchGroupFinder(Grid grid, GridCellsBuilder cellsBuilder, GridNeighborOffsetProvider gridNeighborOffset, TileStackInCellFinder stackFinder)
	{
		_grid = grid;
		_cellsBuilder = cellsBuilder;
		_gridNeighborOffset = gridNeighborOffset;
		_stackFinder = stackFinder;
	}

	public void FindTopColorGroups()
	{
		_foundGroups.Clear();
		_visitedCells.Clear();
		IReadOnlyDictionary<Vector2Int, HexCellView> cells = _cellsBuilder.Cells;
		if (cells == null || cells.Count == 0)
		{
			return;
		}
		Vector2Int[] neighborOffsets = _gridNeighborOffset.GetNeighborOffsets(_grid);
		foreach (KeyValuePair<Vector2Int, HexCellView> item in cells)
		{
			HexCellView startCell = item.Value;
			if (startCell == null || _visitedCells.Contains(startCell))
			{
				continue;
			}
			TileStackView startStack = _stackFinder.FindActiveStackInCell(startCell);
			if (startStack == null || !startStack.TryGetTopColor(out var topColor))
			{
				_visitedCells.Add(startCell);
				continue;
			}
			List<HexCellView> group = GetGroupFromPool();
			group.Clear();
			_bfsQueue.Clear();
			_bfsQueue.Enqueue(startCell);
			_visitedCells.Add(startCell);
			while (_bfsQueue.Count > 0)
			{
				HexCellView currentCellView = _bfsQueue.Dequeue();
				group.Add(currentCellView);
				Vector2Int currentCoordinates = currentCellView.Coordinates;
				for (int i = 0; i < neighborOffsets.Length; i++)
				{
					Vector2Int nextCoordinates = currentCoordinates + neighborOffsets[i];
					if (cells.TryGetValue(nextCoordinates, out var neighborCell) && !(neighborCell == null) && !_visitedCells.Contains(neighborCell))
					{
						TileStackView neighborStack = _stackFinder.FindActiveStackInCell(neighborCell);
						if (!(neighborStack == null) && neighborStack.TryGetTopColor(out var neighborTopColor) && !(neighborTopColor != topColor))
						{
							_visitedCells.Add(neighborCell);
							_bfsQueue.Enqueue(neighborCell);
						}
					}
				}
			}
			if (group.Count >= 2)
			{
				_foundGroups.Add(group);
			}
			else
			{
				ReturnGroupToPool(group);
			}
		}
	}

	public void ReleaseGroups()
	{
		for (int i = 0; i < _foundGroups.Count; i++)
		{
			ReturnGroupToPool(_foundGroups[i]);
		}
		_foundGroups.Clear();
	}

	private List<HexCellView> GetGroupFromPool()
	{
		if (_groupPool.Count == 0)
		{
			return new List<HexCellView>();
		}
		int lastIndex = _groupPool.Count - 1;
		List<HexCellView> group = _groupPool[lastIndex];
		_groupPool.RemoveAt(lastIndex);
		return group;
	}

	private void ReturnGroupToPool(List<HexCellView> group)
	{
		if (group != null)
		{
			group.Clear();
			_groupPool.Add(group);
		}
	}
}
