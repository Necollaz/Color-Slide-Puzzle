using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileStackFieldGenerator
{
    private readonly TileEmptyCellSelector _emptyCellSelector;
    private readonly TileStackClusterBuilder _clusterBuilder;

    [Inject]
    public TileStackFieldGenerator(TileEmptyCellSelector emptyCellSelector, TileStackClusterBuilder clusterBuilder)
    {
        _emptyCellSelector = emptyCellSelector;
        _clusterBuilder = clusterBuilder;
    }

    public void TryGenerateStacks(IReadOnlyDictionary<Vector2Int, HexCellView> cellViewsByCoordinates)
    {
        if (cellViewsByCoordinates == null || cellViewsByCoordinates.Count == 0)
            return;

        Vector2Int emptyCellCoordinates = _emptyCellSelector.ChooseEmptyCellClosestToCamera(cellViewsByCoordinates);

        HashSet<Vector2Int> availableCoordinates = new HashSet<Vector2Int>();

        foreach (Vector2Int coordinates in cellViewsByCoordinates.Keys)
        {
            if (coordinates == emptyCellCoordinates)
                continue;

            availableCoordinates.Add(coordinates);
        }

        _clusterBuilder.BuildRandomClusters(cellViewsByCoordinates, availableCoordinates, emptyCellCoordinates);
    }
}