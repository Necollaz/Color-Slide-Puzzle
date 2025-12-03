using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private Grid _grid;
    [SerializeField] private HexCellView _cellPrefab;
   
    [Header("Stacks")]
    [SerializeField] private TileStackView _cellStackPrefab;
    [SerializeField] private TileStackRoot _spawnStackRootPrefab;
    
    [Header("Configs")]
    [SerializeField] private TileConfig _tileConfig;
    [SerializeField] private GridDefinitionConfig _gridDefinitionConfig;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_grid).AsSingle();
        Container.BindInstance(_cellPrefab).AsSingle();
        Container.BindInstance(_cellStackPrefab).AsSingle();
        Container.BindInstance(_spawnStackRootPrefab).AsSingle();
        Container.BindInstance(_gridDefinitionConfig).AsSingle();
        Container.BindInstance(_tileConfig).AsSingle();

        Container.Bind<TileShaderPropertyIds>().AsSingle();
        
        Container.Bind<ListRandomizer>().AsSingle();
        Container.Bind<GridNeighborOffsetProvider>().AsSingle();
        Container.Bind<GridCellsBuilder>().AsSingle();
        Container.Bind<TileEmptyCellSelector>().AsSingle();
        Container.Bind<TileColorStatistics>().AsSingle();
        Container.Bind<TileStackColorBlocksBuilder>().AsSingle();
        Container.Bind<TileStackColorsGenerator>().AsSingle();
        Container.Bind<TileStackPoolCleaner>().AsSingle();
        Container.Bind<TileStackFactory>().AsSingle();
        Container.Bind<TileStackClusterBuilder>().AsSingle();
        Container.Bind<TileStackFieldGenerator>().AsSingle();
        Container.Bind<HexTileGridBuilder>().AsSingle();
        Container.Bind<TileSpawnUsefulTopColorsCollector>().AsSingle();
        Container.Bind<TileColorRemainderSorter>().AsSingle();
        Container.Bind<TileSpawnStackFromColorsBuilder>().AsSingle();
        Container.Bind<TileSpawnMatchPlanner>().AsSingle();
        Container.Bind<TileSpawnTopColorSelector>().AsSingle();
        Container.Bind<TileStackInCellFinder>().AsSingle();
        Container.Bind<TileMatchGroupFinder>().AsSingle();
        Container.Bind<TileGroupTransferPlanner>().AsSingle();
        
        Container.Bind<TileMergeSegmentTemplate>().AsSingle();
        Container.Bind<TileMergeSegmentPool>().AsSingle();
        Container.Bind<TileMergeColorApplier>().AsSingle();
        Container.Bind<TileMergePositionCalculator>().AsSingle();

        Container.Bind<TileStackMatchResolver>().AsSingle();
        Container.Bind<TileStackMergeAnimator>().AsSingle();

        Container.Bind<GameplayInputActions>().AsSingle().NonLazy();
    }

    public override void Start()
    {
        HexTileGridBuilder gridBuilder = Container.Resolve<HexTileGridBuilder>();
        gridBuilder.Build(_gridDefinitionConfig);
    }
}