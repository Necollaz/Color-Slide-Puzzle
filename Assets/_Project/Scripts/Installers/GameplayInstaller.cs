using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private Grid _grid;
    [SerializeField] private HexCellView _cellPrefab;
    [SerializeField] private TileStackView _stackPrefab;
    [SerializeField] private GridDefinitionConfig _gridDefinitionConfig;
    [SerializeField] private TileConfig _tileConfig;

    public override void InstallBindings()
    {
        Container.BindInstance(_grid).AsSingle();
        Container.BindInstance(_cellPrefab).AsSingle();
        Container.BindInstance(_stackPrefab).AsSingle();
        Container.BindInstance(_gridDefinitionConfig).AsSingle();
        Container.BindInstance(_tileConfig).AsSingle();

        Container.Bind<GridCellsBuilder>().AsSingle();
        Container.Bind<TileEmptyCellSelector>().AsSingle();

        Container.Bind<TileStackColorsGenerator>().AsSingle();
        Container.Bind<TileStackPoolCleaner>().AsSingle();
        Container.Bind<TileStackFactory>().AsSingle();
        Container.Bind<TileStackClusterBuilder>().AsSingle();

        Container.Bind<TileStackFieldGenerator>().AsSingle();
        Container.Bind<HexTileGridBuilder>().AsSingle();
    }

    public override void Start()
    {
        HexTileGridBuilder gridBuilder = Container.Resolve<HexTileGridBuilder>();
        gridBuilder.Build(_gridDefinitionConfig);
    }
}