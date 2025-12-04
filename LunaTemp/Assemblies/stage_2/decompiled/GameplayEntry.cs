using UnityEngine;

public class GameplayEntry : MonoBehaviour
{
	[Header("Grid")]
	[SerializeField]
	private Grid _grid;

	[SerializeField]
	private HexCellView _cellPrefab;

	[Header("Stacks")]
	[SerializeField]
	private TileStackView _cellStackPrefab;

	[SerializeField]
	private TileStackRoot _spawnStackRootPrefab;

	[Header("Dependencies (Manual)")]
	[SerializeField]
	private TileStackSpawnPoint[] _spawnPoints;

	[SerializeField]
	private TileStackDragInput _dragInput;

	[Header("Configs")]
	[SerializeField]
	private TileConfig _tileConfig;

	[SerializeField]
	private GridDefinitionConfig _gridDefinitionConfig;

	private HexTileGridBuilder _gridBuilder;

	private TileStackFactory _stackFactory;

	private TileStackMatchResolver _matchResolver;

	private void Awake()
	{
		Debug.Log("GameplayEntry.Awake: start init");
		if (_grid == null)
		{
			GameObject go = new GameObject("GridRuntime");
			go.transform.SetParent(base.transform);
			_grid = go.AddComponent<Grid>();
		}
		Debug.Log("_grid = " + (_grid != null));
		Debug.Log("_cellPrefab = " + (_cellPrefab != null));
		Debug.Log("_tileConfig = " + (_tileConfig != null));
		Debug.Log("_gridDefinitionConfig = " + (_gridDefinitionConfig != null));
		if (_grid == null || _cellPrefab == null || _tileConfig == null || _gridDefinitionConfig == null)
		{
			Debug.LogError("GameplayEntry: Required references missing");
			base.enabled = false;
			return;
		}
		GridNeighborOffsetProvider neighborOffsets = new GridNeighborOffsetProvider();
		TileEmptyCellSelector cellSelector = new TileEmptyCellSelector();
		TileStackInCellFinder inCellFinder = new TileStackInCellFinder();
		GridCellsBuilder cellBuilder = new GridCellsBuilder(_grid, _cellPrefab);
		ListRandomizer listRandomizer = new ListRandomizer();
		TileStackColorBlocksBuilder colorBlocksBuilder = new TileStackColorBlocksBuilder(_tileConfig, listRandomizer);
		TileStackColorsGenerator colorsGenerator = new TileStackColorsGenerator(_tileConfig, colorBlocksBuilder);
		TileColorStatistics stats = new TileColorStatistics(cellBuilder);
		TileColorRemainderSorter remainderSorter = new TileColorRemainderSorter(stats);
		TileStackPoolCleaner poolCleaner = new TileStackPoolCleaner();
		TileSpawnUsefulTopColorsCollector usefulCollector = new TileSpawnUsefulTopColorsCollector(_grid, cellBuilder, neighborOffsets);
		TileSpawnStackFromColorsBuilder stackFromColors = new TileSpawnStackFromColorsBuilder(_tileConfig, stats);
		TileSpawnMatchPlanner spawnMatchPlanner = new TileSpawnMatchPlanner(_tileConfig, cellBuilder, stats, usefulCollector, remainderSorter, stackFromColors);
		TileSpawnTopColorSelector spawnTopColor = new TileSpawnTopColorSelector(_grid, cellBuilder, _tileConfig, stats, neighborOffsets);
		_stackFactory = new TileStackFactory(_cellStackPrefab, _spawnStackRootPrefab, colorsGenerator, _tileConfig, cellBuilder, spawnMatchPlanner, spawnTopColor, stats);
		TileStackClusterBuilder clusters = new TileStackClusterBuilder(_grid, _stackFactory, poolCleaner, _tileConfig, neighborOffsets, listRandomizer);
		TileStackFieldGenerator fieldGenerator = new TileStackFieldGenerator(cellSelector, clusters, _grid, _tileConfig, neighborOffsets);
		_gridBuilder = new HexTileGridBuilder(cellBuilder, fieldGenerator);
		TileMergeSegmentTemplate segmentTemplate = new TileMergeSegmentTemplate(_cellStackPrefab, _grid);
		TileMergeSegmentPool segmentPool = new TileMergeSegmentPool(segmentTemplate);
		TileShaderPropertyIds shaderProps = new TileShaderPropertyIds();
		TileMergeColorApplier colorApplier = new TileMergeColorApplier(shaderProps);
		TileMergePositionCalculator posCalculator = new TileMergePositionCalculator(_tileConfig, segmentTemplate);
		TileEffectsPlayer effectsPlayer = new TileEffectsPlayer(_grid, _tileConfig, segmentTemplate);
		TileStackMergeAnimator mergeAnimator = new TileStackMergeAnimator(segmentTemplate, segmentPool, colorApplier, posCalculator, effectsPlayer);
		TileGroupTransferPlanner transferPlanner = new TileGroupTransferPlanner(_grid, neighborOffsets, cellBuilder, inCellFinder);
		TileMatchGroupFinder matchFinder = new TileMatchGroupFinder(_grid, cellBuilder, neighborOffsets, inCellFinder);
		_matchResolver = new TileStackMatchResolver(cellBuilder, mergeAnimator, inCellFinder, matchFinder, transferPlanner);
		TileStackSpawnPoint[] spawnPoints = _spawnPoints;
		foreach (TileStackSpawnPoint sp in spawnPoints)
		{
			if (sp != null)
			{
				sp.Construct(_stackFactory);
			}
		}
		if (_dragInput != null)
		{
			_dragInput.Construct(_stackFactory, cellBuilder, _matchResolver);
		}
	}

	private void Start()
	{
		Debug.Log("GameplayEntry.Start: build grid");
		if (_gridBuilder == null)
		{
			Debug.LogError("GameplayEntry.Start: _gridBuilder is null, grid will not be built");
			return;
		}
		_gridBuilder.Build(_gridDefinitionConfig);
		Debug.Log("GameplayEntry.Start: grid built");
	}

	private void Reset()
	{
		if (_grid == null)
		{
			_grid = GetComponent<Grid>();
		}
	}
}
