using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HexTileGridBuilder
{
    private readonly Grid _grid;
    private readonly HexCellView _cellPrefab;
    private readonly DiContainer _container;

    private readonly Dictionary<Vector2Int, HexCellView> _mapCellViews = new();

    [Inject]
    public HexTileGridBuilder(DiContainer container, Grid grid, HexCellView cellPrefab)
    {
        _container = container;
        _grid = grid;
        _cellPrefab = cellPrefab;
    }
    
    public void Build(GridDefinitionConfig config)
    {
        ClearGrid();

        switch (config.ShapeType)
        {
            case GridShapeType.Rectangle:
                BuildRectangle(config.Width, config.Height);
                break;

            case GridShapeType.Hexagon:
                BuildHexagon(config.HexRadius);
                break;
        }
        
        foreach (KeyValuePair<Vector2Int, HexCellView> pair in _mapCellViews)
            pair.Value.SetVisible(true);
    }
    
    private Vector3 ComputeHexCellPosition(Vector2Int coordinates, Grid grid)
    {
        int q = coordinates.x;
        int r = coordinates.y;
        
        float size = grid.cellSize.x * 0.5f;

        float x = size * 1.5f * q;
        float z = size * Mathf.Sqrt(3f) * (r + 0.5f * q);

        return new Vector3(x, 0f, z);
    }

    private void BuildRectangle(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                
                InstantiateCell(coordinates);
            }
        }
    }
    
    private void BuildHexagon(int radius)
    {
        for (int q = -radius; q <= radius; q++)
        {
            int radiusMin = Mathf.Max(-radius, -q - radius);
            int radiusMax = Mathf.Min(radius, -q + radius);

            for (int r = radiusMin; r <= radiusMax; r++)
            {
                Vector2Int coordinates = new Vector2Int(q, r);
                
                InstantiateCell(coordinates);
            }
        }
    }
    
    private void ClearGrid()
    {
        foreach (HexCellView cellView in _mapCellViews.Values)
        {
            if (cellView != null)
                cellView.gameObject.SetActive(false);
        }

        _mapCellViews.Clear();
    }

    private void InstantiateCell(Vector2Int coordinates)
    {
        HexCellView cellView = _container.InstantiatePrefabForComponent<HexCellView>(_cellPrefab, _grid.transform);
        Vector3 localCenter;

        if (_grid.cellLayout == GridLayout.CellLayout.Hexagon)
        {
            localCenter = ComputeHexCellPosition(coordinates, _grid);
        }
        else
        {
            Vector3Int cellPosition = new Vector3Int(coordinates.x, coordinates.y, 0);
            Vector3 localCenterXY = _grid.CellToLocalInterpolated(new Vector3(cellPosition.x + 0.5f,
                cellPosition.y + 0.5f, 0f));

            localCenter = new Vector3(localCenterXY.x, 0f, localCenterXY.y);
        }

        cellView.transform.localPosition = localCenter;
        cellView.Initialize(_grid);

        _mapCellViews[coordinates] = cellView;
    }
}