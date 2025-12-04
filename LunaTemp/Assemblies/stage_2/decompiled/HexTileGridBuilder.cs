using System.Collections.Generic;
using UnityEngine;

public class HexTileGridBuilder
{
	private readonly GridCellsBuilder _cellsBuilder;

	private readonly TileStackFieldGenerator _stackGenerator;

	public HexTileGridBuilder(GridCellsBuilder cellsBuilder, TileStackFieldGenerator stackGenerator = null)
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
		foreach (KeyValuePair<Vector2Int, HexCellView> cell in _cellsBuilder.Cells)
		{
			cell.Value.SetVisible(true);
		}
		if (_stackGenerator != null)
		{
			_stackGenerator.TryGenerateStacks(_cellsBuilder.Cells, config.OccupiedCellsCount);
		}
	}
}
