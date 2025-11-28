using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileStackClusterBuilder
{
    private readonly Grid _grid;
    private readonly TileConfig _config;
    private readonly TileStackFactory _stackFactory;
    private readonly TileStackPoolCleaner _poolCleaner;

    private readonly Vector2Int[] _rectangleNeighborOffsets =
    {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
    };
    private readonly Vector2Int[] _hexagonNeighborOffsets =
    {
        new Vector2Int(1, 0),
        new Vector2Int(1, -1),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int(0, 1),
    };

    private readonly List<Vector2Int> _neighborOffsetsBuffer = new();

    [Inject]
    public TileStackClusterBuilder(Grid grid, TileStackFactory stackFactory, TileStackPoolCleaner poolCleaner, 
        TileConfig config)
    {
        _grid = grid;
        _stackFactory = stackFactory;
        _poolCleaner = poolCleaner;
        _config = config;
    }

    public void BuildRandomClusters(IReadOnlyDictionary<Vector2Int, HexCellView> cellViewsByCoordinates,
        HashSet<Vector2Int> availableCoordinates, Vector2Int emptyCellCoordinates)
    {
        _poolCleaner.DeactivateStacks(cellViewsByCoordinates);

        if (cellViewsByCoordinates == null || cellViewsByCoordinates.Count == 0)
            return;

        if (availableCoordinates == null || availableCoordinates.Count == 0)
            return;

        bool isHexagonGrid = _grid.cellLayout == GridLayout.CellLayout.Hexagon;

        while (availableCoordinates.Count > 0)
        {
            Vector2Int seedCoordinates = GetRandomFromSet(availableCoordinates);

            int minClusterSize = Mathf.Max(1, _config.MinClusterSize);
            int maxClusterSize = Mathf.Max(minClusterSize, _config.MaxClusterSize);
            int targetClusterSizeInclusiveMax = maxClusterSize + 1;

            int targetClusterSize = Random.Range(minClusterSize, targetClusterSizeInclusiveMax);

            GrowCluster(seedCoordinates, targetClusterSize, isHexagonGrid, availableCoordinates, cellViewsByCoordinates);
        }

        EnsureAllCellsFilledExcept(emptyCellCoordinates, cellViewsByCoordinates);
    }

    private void GrowCluster(Vector2Int seedCoordinates, int targetClusterSize, bool isHexagonGrid,
        HashSet<Vector2Int> availableCoordinates, IReadOnlyDictionary<Vector2Int, HexCellView> cellViewsByCoordinates)
    {
        Queue<Vector2Int> coordinatesQueue = new Queue<Vector2Int>();
        coordinatesQueue.Enqueue(seedCoordinates);
        availableCoordinates.Remove(seedCoordinates);

        int paintedCellCount = 0;

        Vector2Int[] baseNeighborOffsets = isHexagonGrid ? _hexagonNeighborOffsets : _rectangleNeighborOffsets;

        _neighborOffsetsBuffer.Clear();
        _neighborOffsetsBuffer.AddRange(baseNeighborOffsets);

        Shuffle(_neighborOffsetsBuffer);

        while (coordinatesQueue.Count > 0 && paintedCellCount < targetClusterSize)
        {
            Vector2Int currentCoordinates = coordinatesQueue.Dequeue();

            if (cellViewsByCoordinates.TryGetValue(currentCoordinates, out HexCellView cellView))
            {
                _stackFactory.CreateOrReuseRandomStackInCell(cellView);
                paintedCellCount++;
            }

            for (int index = 0; index < _neighborOffsetsBuffer.Count; index++)
            {
                Vector2Int neighborOffset = _neighborOffsetsBuffer[index];
                Vector2Int neighborCoordinates = currentCoordinates + neighborOffset;

                if (!availableCoordinates.Contains(neighborCoordinates))
                    continue;

                if (!cellViewsByCoordinates.ContainsKey(neighborCoordinates))
                    continue;

                availableCoordinates.Remove(neighborCoordinates);
                coordinatesQueue.Enqueue(neighborCoordinates);
            }
        }
    }

    private void EnsureAllCellsFilledExcept(Vector2Int emptyCellCoordinates,
        IReadOnlyDictionary<Vector2Int, HexCellView> cellViewsByCoordinates)
    {
        foreach ((Vector2Int coordinates, HexCellView cellView) in cellViewsByCoordinates)
        {
            if (coordinates == emptyCellCoordinates)
                continue;

            if (cellView == null)
                continue;

            if (_stackFactory.HasActiveStack(cellView))
                continue;

            _stackFactory.CreateOrReuseRandomStackInCell(cellView);
        }
    }

    private Vector2Int GetRandomFromSet(HashSet<Vector2Int> coordinatesSet)
    {
        int randomIndex = Random.Range(0, coordinatesSet.Count);
        int currentIndex = 0;

        foreach (Vector2Int coordinates in coordinatesSet)
        {
            if (currentIndex == randomIndex)
                return coordinates;

            currentIndex++;
        }

        foreach (Vector2Int coordinates in coordinatesSet)
            return coordinates;

        return default;
    }

    private void Shuffle(List<Vector2Int> list)
    {
        int count = list.Count;

        for (int index = count - 1; index > 0; index--)
        {
            int randomIndex = Random.Range(0, index + 1);
            (list[index], list[randomIndex]) = (list[randomIndex], list[index]);
        }
    }
}