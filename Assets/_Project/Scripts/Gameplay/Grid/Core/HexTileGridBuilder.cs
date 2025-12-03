using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HexTileGridBuilder
{
    private readonly GridCellsBuilder _cellsBuilder;
    private readonly TileStackFieldGenerator _stackGenerator;

    [Inject]
    public HexTileGridBuilder(GridCellsBuilder cellsBuilder, TileStackFieldGenerator stackGenerator)
    {
        _cellsBuilder = cellsBuilder;
        _stackGenerator = stackGenerator;
    }
    
    public void Build(GridDefinitionConfig config)
    {
        switch (config.ShapeType)
        {
            case GridShapeType.Rectangle:
                _cellsBuilder.TryBuildRectangle(config.Width, config.Height);
                break;

            case GridShapeType.Hexagon:
                _cellsBuilder.TryBuildHexagon(config.HexRadius);
                break;
        }

        foreach (KeyValuePair<Vector2Int, HexCellView> pair in _cellsBuilder.Cells)
            pair.Value.SetVisible(true);

        _stackGenerator.TryGenerateStacks(_cellsBuilder.Cells, config.OccupiedCellsCount);
    }
}