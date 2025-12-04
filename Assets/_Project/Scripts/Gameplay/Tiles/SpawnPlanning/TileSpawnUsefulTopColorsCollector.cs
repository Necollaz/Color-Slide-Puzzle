using System.Collections.Generic;
using UnityEngine;

public class TileSpawnUsefulTopColorsCollector
{
    private readonly Grid _grid;
    private readonly GridCellsBuilder _cellsBuilder;
    private readonly GridNeighborOffsetProvider _gridNeighborOffset;
    
    public TileSpawnUsefulTopColorsCollector(Grid grid, GridCellsBuilder cellsBuilder, 
        GridNeighborOffsetProvider gridNeighborOffset)
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
            return;

        Vector2Int[] neighborOffsets = _gridNeighborOffset.GetNeighborOffsets(_grid);

        foreach (KeyValuePair<Vector2Int, HexCellView> pair in cells)
        {
            Vector2Int coordinates = pair.Key;
            HexCellView cellView = pair.Value;

            if (cellView == null)
                continue;

            TileStackView stackView = cellView.GetComponentInChildren<TileStackView>();

            bool hasActiveStack = stackView != null && stackView.gameObject.activeSelf && !stackView.IsEmpty;

            if (hasActiveStack)
                continue;

            for (int index = 0; index < neighborOffsets.Length; index++)
            {
                Vector2Int neighborCoordinates = coordinates + neighborOffsets[index];

                if (!cells.TryGetValue(neighborCoordinates, out HexCellView neighborCell))
                    continue;

                if (neighborCell == null)
                    continue;

                TileStackView neighborStack = neighborCell.GetComponentInChildren<TileStackView>();

                if (neighborStack == null || neighborStack.IsEmpty || !neighborStack.gameObject.activeSelf)
                    continue;

                if (!neighborStack.TryGetTopColor(out Color neighborTopColor))
                    continue;

                result.Add(neighborTopColor);
            }
        }
    }
}