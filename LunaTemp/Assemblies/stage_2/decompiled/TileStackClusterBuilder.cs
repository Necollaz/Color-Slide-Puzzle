using System.Collections.Generic;
using UnityEngine;

public class TileStackClusterBuilder
{
	private const int MIN_TARGET_OCCUPIED_CELLS = 1;

	private const int MIN_CLUSTER_SIZE_VALUE = 1;

	private readonly Grid _grid;

	private readonly TileConfig _config;

	private readonly TileStackFactory _stackFactory;

	private readonly TileStackPoolCleaner _poolCleaner;

	private readonly GridNeighborOffsetProvider _gridNeighborOffset;

	private readonly ListRandomizer _randomizer;

	private readonly List<Vector2Int> _neighborOffsetsBuffer = new List<Vector2Int>();

	private readonly List<Vector2Int> _freeCoordinatesBuffer = new List<Vector2Int>();

	private readonly Queue<Vector2Int> _coordinatesQueue = new Queue<Vector2Int>();

	public TileStackClusterBuilder(Grid grid, TileStackFactory stackFactory, TileStackPoolCleaner poolCleaner, TileConfig config, GridNeighborOffsetProvider gridNeighborOffset, ListRandomizer randomizer)
	{
		_grid = grid;
		_stackFactory = stackFactory;
		_poolCleaner = poolCleaner;
		_config = config;
		_gridNeighborOffset = gridNeighborOffset;
		_randomizer = randomizer;
	}

	public void BuildRandomClusters(IReadOnlyDictionary<Vector2Int, HexCellView> cellViewsByCoordinates, HashSet<Vector2Int> availableCoordinates, Vector2Int emptyCellCoordinates, int targetOccupiedCells)
	{
		_poolCleaner.DeactivateStacks(cellViewsByCoordinates);
		if (cellViewsByCoordinates != null && cellViewsByCoordinates.Count != 0 && availableCoordinates != null && availableCoordinates.Count != 0)
		{
			int maxPossibleCells = availableCoordinates.Count;
			int remainingCellsToFill = Mathf.Clamp(targetOccupiedCells, 1, maxPossibleCells);
			Vector2Int[] neighborOffsets = _gridNeighborOffset.GetNeighborOffsets(_grid);
			int minClusterSize = Mathf.Max(1, _config.MinClusterSize);
			int maxClusterSize = Mathf.Max(minClusterSize, _config.MaxClusterSize);
			while (availableCoordinates.Count > 0 && remainingCellsToFill > 0)
			{
				Vector2Int seedCoordinates = GetRandomFromSet(availableCoordinates);
				int targetClusterSize = GetTargetClusterSize(minClusterSize, maxClusterSize, remainingCellsToFill, availableCoordinates.Count);
				int paintedInCluster = GrowCluster(seedCoordinates, targetClusterSize, availableCoordinates, cellViewsByCoordinates, neighborOffsets);
				remainingCellsToFill -= paintedInCluster;
			}
			EnsureExactOccupiedCount(cellViewsByCoordinates, emptyCellCoordinates, targetOccupiedCells);
		}
	}

	private int GrowCluster(Vector2Int seedCoordinates, int targetClusterSize, HashSet<Vector2Int> availableCoordinates, IReadOnlyDictionary<Vector2Int, HexCellView> cellViewsByCoordinates, Vector2Int[] neighborOffsets)
	{
		_coordinatesQueue.Clear();
		_coordinatesQueue.Enqueue(seedCoordinates);
		availableCoordinates.Remove(seedCoordinates);
		int paintedCellCount = 0;
		_neighborOffsetsBuffer.Clear();
		_neighborOffsetsBuffer.AddRange(neighborOffsets);
		_randomizer.Shuffle(_neighborOffsetsBuffer);
		while (_coordinatesQueue.Count > 0 && paintedCellCount < targetClusterSize)
		{
			Vector2Int currentCoordinates = _coordinatesQueue.Dequeue();
			if (cellViewsByCoordinates.TryGetValue(currentCoordinates, out var cellView))
			{
				_stackFactory.CreateOrReuseRandomStackInCell(cellView);
				paintedCellCount++;
			}
			for (int index = 0; index < _neighborOffsetsBuffer.Count; index++)
			{
				Vector2Int neighborOffset = _neighborOffsetsBuffer[index];
				Vector2Int neighborCoordinates = currentCoordinates + neighborOffset;
				if (availableCoordinates.Contains(neighborCoordinates) && cellViewsByCoordinates.ContainsKey(neighborCoordinates))
				{
					availableCoordinates.Remove(neighborCoordinates);
					_coordinatesQueue.Enqueue(neighborCoordinates);
				}
			}
		}
		return paintedCellCount;
	}

	private int GetTargetClusterSize(int minClusterSize, int maxClusterSize, int remainingCellsToFill, int availableCoordinatesCount)
	{
		int randomClusterSize = Random.Range(minClusterSize, maxClusterSize + 1);
		return Mathf.Min(randomClusterSize, remainingCellsToFill, availableCoordinatesCount);
	}

	private Vector2Int GetRandomFromSet(HashSet<Vector2Int> coordinatesSet)
	{
		if (coordinatesSet == null || coordinatesSet.Count == 0)
		{
			return default(Vector2Int);
		}
		int targetIndex = Random.Range(0, coordinatesSet.Count);
		int currentIndex = 0;
		foreach (Vector2Int coordinates in coordinatesSet)
		{
			if (currentIndex == targetIndex)
			{
				return coordinates;
			}
			currentIndex++;
		}
		using (HashSet<Vector2Int>.Enumerator enumerator2 = coordinatesSet.GetEnumerator())
		{
			if (enumerator2.MoveNext())
			{
				return enumerator2.Current;
			}
		}
		return default(Vector2Int);
	}

	private void EnsureExactOccupiedCount(IReadOnlyDictionary<Vector2Int, HexCellView> cellViewsByCoordinates, Vector2Int emptyCellCoordinates, int targetOccupiedCells)
	{
		_freeCoordinatesBuffer.Clear();
		int currentOccupiedCellCount = 0;
		foreach (KeyValuePair<Vector2Int, HexCellView> pair in cellViewsByCoordinates)
		{
			Vector2Int coordinates2 = pair.Key;
			HexCellView cellView2 = pair.Value;
			if (!(cellView2 == null) && !(coordinates2 == emptyCellCoordinates))
			{
				if (_stackFactory.HasActiveStack(cellView2))
				{
					currentOccupiedCellCount++;
				}
				else
				{
					_freeCoordinatesBuffer.Add(coordinates2);
				}
			}
		}
		if (currentOccupiedCellCount >= targetOccupiedCells)
		{
			return;
		}
		int remainingToFill = targetOccupiedCells - currentOccupiedCellCount;
		while (remainingToFill > 0 && _freeCoordinatesBuffer.Count > 0)
		{
			int randomIndex = Random.Range(0, _freeCoordinatesBuffer.Count);
			Vector2Int coordinates = _freeCoordinatesBuffer[randomIndex];
			_freeCoordinatesBuffer.RemoveAt(randomIndex);
			if (cellViewsByCoordinates.TryGetValue(coordinates, out var cellView) && !(cellView == null))
			{
				_stackFactory.CreateOrReuseRandomStackInCell(cellView);
				remainingToFill--;
			}
		}
	}
}
