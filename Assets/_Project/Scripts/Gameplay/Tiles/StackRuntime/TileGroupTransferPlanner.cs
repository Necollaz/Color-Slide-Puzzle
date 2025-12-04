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
    
    public TileGroupTransferPlanner(Grid grid, GridNeighborOffsetProvider gridNeighborOffset, 
        GridCellsBuilder cellsBuilder, TileStackInCellFinder stackFinder)
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
            yield break;
        
        HexCellView bestTargetCell = null;
        int bestMovableCount = -1;

        for (int i = 0; i < group.Count; i++)
        {
            HexCellView cell = group[i];
            TileStackView stack = _stackFinder.FindActiveStackInCell(cell);

            if (stack == null)
                continue;

            int movableCount = stack.CountTopTilesOfColor(groupColor);

            if (movableCount > bestMovableCount)
            {
                bestMovableCount = movableCount;
                bestTargetCell = cell;
            }
        }

        if (bestTargetCell == null || bestMovableCount <= 0)
            yield break;

        _parents.Clear();
        _depths.Clear();
        _leafBuffer.Clear();
        _localVisited.Clear();
        _groupSet.Clear();

        for (int i = 0; i < group.Count; i++)
            _groupSet.Add(group[i]);

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

            for (int i = 0; i < neighborOffsets.Length; i++)
            {
                Vector2Int neighborCoordinates = currentCoordinates + neighborOffsets[i];

                HexCellView neighborCell;
                if (!cells.TryGetValue(neighborCoordinates, out neighborCell))
                    continue;

                if (neighborCell == null)
                    continue;

                if (!_groupSet.Contains(neighborCell))
                    continue;

                if (_localVisited.Contains(neighborCell))
                    continue;

                TileStackView neighborStack = _stackFinder.FindActiveStackInCell(neighborCell);
                if (neighborStack == null)
                    continue;

                Color neighborTopColor;
                if (!neighborStack.TryGetTopColor(out neighborTopColor) || neighborTopColor != groupColor)
                    continue;

                _localVisited.Add(neighborCell);
                _bfsQueue.Enqueue(neighborCell);

                _parents[neighborCell] = current;
                _depths[neighborCell] = _depths[current] + 1;
            }
        }

        int maxDepth = 0;
        
        foreach (KeyValuePair<HexCellView, int> pair in _depths)
        {
            if (pair.Value > maxDepth)
                maxDepth = pair.Value;
        }

        foreach (KeyValuePair<HexCellView, int> pair in _depths)
        {
            HexCellView cell = pair.Key;
            int depth = pair.Value;

            HexCellView parent;
            
            if (depth != maxDepth)
                continue;

            if (!_parents.TryGetValue(cell, out parent) || parent == null)
                continue;

            _leafBuffer.Add(cell);
        }

        if (_leafBuffer.Count == 0)
            yield break;

        int chainStepIndex = 0;

        for (int i = 0; i < _leafBuffer.Count; i++)
        {
            HexCellView leafCell = _leafBuffer[i];

            HexCellView parentCell;
            
            if (!_parents.TryGetValue(leafCell, out parentCell) || parentCell == null)
                continue;

            TileStackView sourceStack = _stackFinder.FindActiveStackInCell(leafCell);
            TileStackView targetStack = _stackFinder.FindActiveStackInCell(parentCell);

            if (sourceStack == null || targetStack == null)
                continue;

            int movableCount = sourceStack.CountTopTilesOfColor(groupColor);
            
            if (movableCount <= 0)
                continue;

            yield return new TileTransferStep(sourceStack, targetStack, chainStepIndex);
            
            chainStepIndex++;
        }
    }
}