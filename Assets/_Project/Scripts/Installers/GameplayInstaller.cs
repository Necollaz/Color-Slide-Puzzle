using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private Grid _grid;
    [SerializeField] private HexCellView _cellPrefab;
    [SerializeField] private GridDefinitionConfig _gridDefinitionConfig;

    public override void InstallBindings()
    {
        Container.BindInstance(_grid).AsSingle();
        Container.BindInstance(_cellPrefab).AsSingle();
        Container.BindInstance(_gridDefinitionConfig).AsSingle();

        Container.Bind<HexTileGridBuilder>().AsSingle();
    }

    public override void Start()
    {
        HexTileGridBuilder gridBuilder = Container.Resolve<HexTileGridBuilder>();
        gridBuilder.Build(_gridDefinitionConfig);
    }
}