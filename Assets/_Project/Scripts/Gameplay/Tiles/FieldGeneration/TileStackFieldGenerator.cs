using System.Collections.Generic;
using UnityEngine;

public class TileStackFieldGenerator
{
    private readonly Grid _grid;
    private readonly TileConfig _tileConfig;
    private readonly TileEmptyCellSelector _emptyCellSelector;
    private readonly TileStackClusterBuilder _clusterBuilder;
    private readonly GridNeighborOffsetProvider _gridNeighborOffset;

    private readonly HashSet<Color> _forbiddenColors = new HashSet<Color>();
    private readonly List<Color> _candidateColors = new List<Color>();
    
    public TileStackFieldGenerator(TileEmptyCellSelector emptyCellSelector, TileStackClusterBuilder clusterBuilder,
        Grid grid, TileConfig tileConfig, GridNeighborOffsetProvider gridNeighborOffset)
    {
        _emptyCellSelector = emptyCellSelector;
        _clusterBuilder = clusterBuilder;
        _grid = grid;
        _tileConfig = tileConfig;
        _gridNeighborOffset = gridNeighborOffset;
    }
    
    public Vector2Int EmptyCellCoordinates { get; private set; }

    public void TryGenerateStacks(IReadOnlyDictionary<Vector2Int, HexCellView> cellViewsByCoordinates,
        int desiredOccupiedCells)
    {
        if (cellViewsByCoordinates == null || cellViewsByCoordinates.Count == 0)
            return;

        Vector2Int emptyCellCoordinates = _emptyCellSelector.ChooseEmptyCellClosestToCamera(cellViewsByCoordinates);
        EmptyCellCoordinates = emptyCellCoordinates;
        
        HashSet<Vector2Int> availableCoordinates = new HashSet<Vector2Int>();

        foreach (Vector2Int coordinates in cellViewsByCoordinates.Keys)
        {
            if (coordinates == emptyCellCoordinates)
                continue;

            availableCoordinates.Add(coordinates);
        }

        int totalFillableCells = availableCoordinates.Count;

        if (totalFillableCells <= 0)
            return;
        
        int targetOccupiedCells = Mathf.Clamp(desiredOccupiedCells, 1, totalFillableCells);

        _clusterBuilder.BuildRandomClusters(cellViewsByCoordinates, availableCoordinates, emptyCellCoordinates,
            targetOccupiedCells);
        
        RemoveInitialNeighborTopMatches(cellViewsByCoordinates);
    }
    
    private void RemoveInitialNeighborTopMatches(IReadOnlyDictionary<Vector2Int, HexCellView> cells)
    {
        if (cells == null || cells.Count == 0)
            return;

        IReadOnlyList<Color> palette = _tileConfig.Colors;

        if (palette == null || palette.Count == 0)
            return;

        Vector2Int[] neighborOffsets = _gridNeighborOffset.GetNeighborOffsets(_grid);

        foreach (KeyValuePair<Vector2Int, HexCellView> pair in cells)
        {
            Vector2Int coordinates = pair.Key;
            HexCellView cellView = pair.Value;

            if (cellView == null)
                continue;

            TileStackView stackView = cellView.GetComponentInChildren<TileStackView>();

            if (stackView == null)
                continue;

            if (!stackView.TryGetTopColor(out Color topColor))
                continue;

            bool hasNeighborWithSameColor = HasNeighborWithSameTopColor(coordinates, topColor, cells, neighborOffsets);

            if (!hasNeighborWithSameColor)
                continue;

            if (TryPickTopColor(coordinates, cells, neighborOffsets, palette, out Color newTopColor))
                stackView.ForceTopColor(newTopColor);
        }
    }

    private bool HasNeighborWithSameTopColor(Vector2Int cellCoordinates, Color topColor,
        IReadOnlyDictionary<Vector2Int, HexCellView> cells, Vector2Int[] neighborOffsets)
    {
        for (int i = 0; i < neighborOffsets.Length; i++)
        {
            Vector2Int neighborCoordinates = cellCoordinates + neighborOffsets[i];

            if (!cells.TryGetValue(neighborCoordinates, out HexCellView neighborCell))
                continue;

            if (neighborCell == null)
                continue;

            TileStackView neighborStack = neighborCell.GetComponentInChildren<TileStackView>();

            if (neighborStack == null)
                continue;

            if (!neighborStack.TryGetTopColor(out Color neighborTopColor))
                continue;

            if (neighborTopColor == topColor)
                return true;
        }

        return false;
    }

    private bool TryPickTopColor(Vector2Int cellCoordinates, IReadOnlyDictionary<Vector2Int, HexCellView> cells,
        Vector2Int[] neighborOffsets, IReadOnlyList<Color> palette, out Color resultColor)
    {
        _forbiddenColors.Clear();
        _candidateColors.Clear();
        
        for (int i = 0; i < neighborOffsets.Length; i++)
        {
            Vector2Int neighborCoordinates = cellCoordinates + neighborOffsets[i];

            if (!cells.TryGetValue(neighborCoordinates, out HexCellView neighborCell))
                continue;

            if (neighborCell == null)
                continue;

            TileStackView neighborStack = neighborCell.GetComponentInChildren<TileStackView>();

            if (neighborStack == null)
                continue;

            if (!neighborStack.TryGetTopColor(out Color neighborTopColor))
                continue;

            _forbiddenColors.Add(neighborTopColor);
        }

        if (_forbiddenColors.Count >= palette.Count)
        {
            resultColor = default;
            
            return false;
        }

        for (int i = 0; i < palette.Count; i++)
        {
            Color paletteColor = palette[i];

            if (_forbiddenColors.Contains(paletteColor))
                continue;

            _candidateColors.Add(paletteColor);
        }

        if (_candidateColors.Count == 0)
        {
            resultColor = default;
            
            return false;
        }

        int randomIndex = Random.Range(0, _candidateColors.Count);
        resultColor = _candidateColors[randomIndex];

        return true;
    }
}