using UnityEngine;

public class GameplayEntry : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private Grid _grid;
    [SerializeField] private HexCellView _cellPrefab;

    [Header("Stacks")]
    [SerializeField] private TileStackView _cellStackPrefab;
    [SerializeField] private TileStackRoot _spawnStackRootPrefab;

    [Header("Dependencies (Manual)")]
    [SerializeField] private TileStackSpawnPoint[] _spawnPoints;
    [SerializeField] private TileStackDragInput _dragInput;

    [Header("Configs")]
    [SerializeField] private TileConfig _tileConfig;
    [SerializeField] private GridDefinitionConfig _gridDefinitionConfig;

    private HexTileGridBuilder _gridBuilder;
    private TileStackFactory _stackFactory;
    private TileStackMatchResolver _matchResolver;

    private void Awake()
    {
        var neighborOffsets = new GridNeighborOffsetProvider();
        var cellSelector = new TileEmptyCellSelector();
        var inCellFinder = new TileStackInCellFinder();
        var cellBuilder = new GridCellsBuilder(_grid, _cellPrefab);

        var listRandomizer = new ListRandomizer();
        var colorBlocksBuilder = new TileStackColorBlocksBuilder(_tileConfig, listRandomizer);
        var colorsGenerator = new TileStackColorsGenerator(_tileConfig, colorBlocksBuilder);
        var stats = new TileColorStatistics(cellBuilder);
        var remainderSorter = new TileColorRemainderSorter(stats);
        var poolCleaner = new TileStackPoolCleaner();
        var usefulCollector = new TileSpawnUsefulTopColorsCollector(_grid, cellBuilder, neighborOffsets);
        var stackFromColors = new TileSpawnStackFromColorsBuilder(_tileConfig, stats);

        var spawnMatchPlanner = new TileSpawnMatchPlanner(_tileConfig, cellBuilder, stats, usefulCollector, remainderSorter, stackFromColors);
        var spawnTopColor = new TileSpawnTopColorSelector(_grid, cellBuilder, _tileConfig, stats, neighborOffsets);

        _stackFactory = new TileStackFactory(_cellStackPrefab, _spawnStackRootPrefab, colorsGenerator, _tileConfig, cellBuilder, spawnMatchPlanner, spawnTopColor, stats);

        var clusters = new TileStackClusterBuilder(_grid, _stackFactory, poolCleaner, _tileConfig, neighborOffsets, listRandomizer);
        var fieldGenerator = new TileStackFieldGenerator(cellSelector, clusters, _grid, _tileConfig, neighborOffsets);

        _gridBuilder = new HexTileGridBuilder(cellBuilder, fieldGenerator);

        var segmentTemplate = new TileMergeSegmentTemplate(_cellStackPrefab, _grid);
        var segmentPool = new TileMergeSegmentPool(segmentTemplate);
        var shaderProps = new TileShaderPropertyIds();
        var colorApplier = new TileMergeColorApplier(shaderProps);
        var posCalculator = new TileMergePositionCalculator(_tileConfig, segmentTemplate);
        var effectsPlayer = new TileEffectsPlayer(_grid, _tileConfig, segmentTemplate);

        var mergeAnimator = new TileStackMergeAnimator(segmentTemplate, segmentPool, colorApplier, posCalculator, effectsPlayer);

        var transferPlanner = new TileGroupTransferPlanner(_grid, neighborOffsets, cellBuilder, inCellFinder);
        var matchFinder = new TileMatchGroupFinder(_grid, cellBuilder, neighborOffsets, inCellFinder);

        _matchResolver = new TileStackMatchResolver(cellBuilder, mergeAnimator, inCellFinder, matchFinder, transferPlanner);

        foreach (var spawnPoint in _spawnPoints)
        {
            if (spawnPoint != null)
                spawnPoint.Construct(_stackFactory);
        }

        if (_dragInput != null)
            _dragInput.Construct(_stackFactory, cellBuilder, _matchResolver);
    }

    private void Start()
    {
        if (_gridBuilder == null)
        {
            Debug.LogError("GameplayEntry.Start: _gridBuilder is null, grid will not be built");
            return;
        }

        _gridBuilder.Build(_gridDefinitionConfig);
    }

    private void Reset()
    {
        if (_grid == null)
            _grid = GetComponent<Grid>();
    }
}
