using System.Collections.Generic;
using UnityEngine;

public class TileGroupTransferPlanner
{
	private readonly Grid _grid;

	private readonly GridNeighborOffsetProvider _gridNeighborOffset;

	private readonly GridCellsBuilder _cellsBuilder;

	private readonly TileStackInCellFinder _stackFinder;

	private readonly Dictionary<HexCellView, HexCellView> _parents = new Dictionary<HexCellView, HexCellView>();

	private readonly Dictionary<HexCellView, int> _depths = new Dictionary<HexCellView, int>();

	private readonly HashSet<HexCellView> _localVisited = new HashSet<HexCellView>();

	private readonly HashSet<HexCellView> _groupSet = new HashSet<HexCellView>();

	private readonly Queue<HexCellView> _bfsQueue = new Queue<HexCellView>();

	private readonly List<HexCellView> _leafBuffer = new List<HexCellView>();

	public TileGroupTransferPlanner(Grid grid, GridNeighborOffsetProvider gridNeighborOffset, GridCellsBuilder cellsBuilder, TileStackInCellFinder stackFinder)
	{
		_grid = grid;
		_gridNeighborOffset = gridNeighborOffset;
		_cellsBuilder = cellsBuilder;
		_stackFinder = stackFinder;
	}

	public IEnumerable<TileTransferStep> BuildTransferSteps(List<HexCellView> group, Color groupColor)
	{
		IReadOnlyDictionary<Vector2Int, HexCellView> cells = _cellsBuilder.Cells;
		if (cells == null || cells.Count == 0 || group == null || group.Count == 0)
		{
			yield break;
		}
		HexCellView bestTargetCell = null;
		int bestMovableCount = -1;
		for (int i = 0; i < group.Count; i++)
		{
			HexCellView cell = group[i];
			TileStackView stack = _stackFinder.FindActiveStackInCell(cell);
			if (!(stack == null))
			{
				int movableCount = stack.CountTopTilesOfColor(groupColor);
				if (movableCount > bestMovableCount)
				{
					bestMovableCount = movableCount;
					bestTargetCell = cell;
				}
			}
		}
		if (bestTargetCell == null || bestMovableCount <= 0)
		{
			yield break;
		}
		_parents.Clear();
		_depths.Clear();
		_leafBuffer.Clear();
		_localVisited.Clear();
		_groupSet.Clear();
		for (int j = 0; j < group.Count; j++)
		{
			_groupSet.Add(group[j]);
		}
		Vector2Int[] neighborOffsets = _gridNeighborOffset.GetNeighborOffsets(_grid);
		_bfsQueue.Clear();
		_bfsQueue.Enqueue(bestTargetCell);
		_localVisited.Add(bestTargetCell);
		_parents[bestTargetCell] = null;
		_depths[bestTargetCell] = 0;
		while (_bfsQueue.Count > 0)
		{
			HexCellView current = _bfsQueue.Dequeue();
			Vector2Int currentCoordinates = current.Coordinates;
			for (int k = 0; k < neighborOffsets.Length; k++)
			{
				Vector2Int neighborCoordinates = currentCoordinates + neighborOffsets[k];
				if (cells.TryGetValue(neighborCoordinates, out var neighborCell) && !(neighborCell == null) && _groupSet.Contains(neighborCell) && !_localVisited.Contains(neighborCell))
				{
					TileStackView neighborStack = _stackFinder.FindActiveStackInCell(neighborCell);
					if (!(neighborStack == null) && neighborStack.TryGetTopColor(out var neighborTopColor) && !(neighborTopColor != groupColor))
					{
						_localVisited.Add(neighborCell);
						_bfsQueue.Enqueue(neighborCell);
						_parents[neighborCell] = current;
						_depths[neighborCell] = _depths[current] + 1;
						neighborCell = null;
						neighborTopColor = default(Color);
					}
				}
			}
		}
		int maxDepth = 0;
		foreach (KeyValuePair<HexCellView, int> pair2 in _depths)
		{
			if (pair2.Value > maxDepth)
			{
				maxDepth = pair2.Value;
			}
		}
		foreach (KeyValuePair<HexCellView, int> pair in _depths)
		{
			HexCellView cell2 = pair.Key;
			int depth = pair.Value;
			if (depth == maxDepth && _parents.TryGetValue(cell2, out var parent) && !(parent == null))
			{
				_leafBuffer.Add(cell2);
				parent = null;
			}
		}
		if (_leafBuffer.Count == 0)
		{
			yield break;
		}
		int chainStepIndex = 0;
		for (int l = 0; l < _leafBuffer.Count; l++)
		{
			HexCellView leafCell = _leafBuffer[l];
			if (!_parents.TryGetValue(leafCell, out var parentCell) || parentCell == null)
			{
				continue;
			}
			TileStackView sourceStack = _stackFinder.FindActiveStackInCell(leafCell);
			TileStackView targetStack = _stackFinder.FindActiveStackInCell(parentCell);
			if (!(sourceStack == null) && !(targetStack == null))
			{
				int movableCount2 = sourceStack.CountTopTilesOfColor(groupColor);
				if (movableCount2 > 0)
				{
					yield return new TileTransferStep(sourceStack, targetStack, chainStepIndex);
					chainStepIndex++;
					parentCell = null;
				}
			}
		}
	}
}
