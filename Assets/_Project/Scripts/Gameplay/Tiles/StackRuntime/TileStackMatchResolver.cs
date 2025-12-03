using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileStackMatchResolver
{
    private const int MIN_MATCH_GROUP_SIZE = 2;
    
    private readonly Grid _grid;
    private readonly GridCellsBuilder _cellsBuilder;
    private readonly GridNeighborOffsetProvider _gridNeighborOffset;
    
    private readonly HashSet<HexCellView> _visitedCells = new HashSet<HexCellView>();
    private readonly List<List<HexCellView>> _foundGroups = new List<List<HexCellView>>();
    private readonly Queue<HexCellView> _bfsQueue = new Queue<HexCellView>();
    private readonly List<List<HexCellView>> _groupPool = new List<List<HexCellView>>();

    [Inject]
    public TileStackMatchResolver(Grid grid, GridCellsBuilder cellsBuilder, GridNeighborOffsetProvider gridNeighborOffset)
    {
        _grid = grid;
        _cellsBuilder = cellsBuilder;
        _gridNeighborOffset = gridNeighborOffset;
    }

    public void ResolveMatchesFromCell()
    {
        while (TryResolveGlobalWave())
        {
        }
    }
    
    private bool TryResolveGlobalWave()
    {
        IReadOnlyDictionary<Vector2Int, HexCellView> cells = _cellsBuilder.Cells;

        if (cells == null || cells.Count == 0)
            return false;

        Vector2Int[] neighborOffsets = _gridNeighborOffset.GetNeighborOffsets(_grid);

        _visitedCells.Clear();
        _foundGroups.Clear();

        foreach (KeyValuePair<Vector2Int, HexCellView> pair in cells)
        {
            HexCellView cell = pair.Value;

            if (cell == null || _visitedCells.Contains(cell))
                continue;

            TileStackView stack = cell.GetComponentInChildren<TileStackView>();

            if (stack == null || !stack.TryGetTopColor(out Color topColor))
            {
                _visitedCells.Add(cell);
                
                continue;
            }

            List<HexCellView> group = GetGroupFromPool();
            group.Clear();

            _bfsQueue.Clear();
            _bfsQueue.Enqueue(cell);
            _visitedCells.Add(cell);

            while (_bfsQueue.Count > 0)
            {
                HexCellView currentCellView = _bfsQueue.Dequeue();
                group.Add(currentCellView);

                Vector2Int currentCoordinates = currentCellView.Coordinates;

                for (int i = 0; i < neighborOffsets.Length; i++)
                {
                    Vector2Int nextCoordinates = currentCoordinates + neighborOffsets[i];

                    if (!cells.TryGetValue(nextCoordinates, out HexCellView neighborCell))
                        continue;

                    if (neighborCell == null || _visitedCells.Contains(neighborCell))
                        continue;

                    TileStackView neighborStack = neighborCell.GetComponentInChildren<TileStackView>();

                    if (neighborStack == null || !neighborStack.TryGetTopColor(out Color neighborTopColor))
                        continue;

                    if (neighborTopColor != topColor)
                        continue;

                    _visitedCells.Add(neighborCell);
                    _bfsQueue.Enqueue(neighborCell);
                }
            }

            if (group.Count >= MIN_MATCH_GROUP_SIZE)
                _foundGroups.Add(group);
            else
                ReturnGroupToPool(group);
        }

        if (_foundGroups.Count == 0)
            return false;
        
        for (int groupIndex = 0; groupIndex < _foundGroups.Count; groupIndex++)
        {
            List<HexCellView> group = _foundGroups[groupIndex];

            TileStackView targetStack = group[0].GetComponentInChildren<TileStackView>();

            if (targetStack == null)
                continue;

            if (!targetStack.TryGetTopColor(out Color groupColor))
                continue;

            int collected = 0;

            for (int i = 1; i < group.Count; i++)
            {
                TileStackView stackView = group[i].GetComponentInChildren<TileStackView>();

                if (stackView == null)
                    continue;

                collected += stackView.ExtractAllTilesOfColor(groupColor);
            }

            if (collected > 0)
                targetStack.AddTilesOnTop(groupColor, collected);
        }
        
        for (int i = 0; i < _foundGroups.Count; i++)
            ReturnGroupToPool(_foundGroups[i]);

        _foundGroups.Clear();

        return true;
    }

    private List<HexCellView> GetGroupFromPool()
    {
        if (_groupPool.Count == 0)
            return new List<HexCellView>();

        int lastIndex = _groupPool.Count - 1;
        List<HexCellView> group = _groupPool[lastIndex];
        _groupPool.RemoveAt(lastIndex);

        return group;
    }

    private void ReturnGroupToPool(List<HexCellView> group)
    {
        if (group == null)
            return;

        group.Clear();
        _groupPool.Add(group);
    }
}