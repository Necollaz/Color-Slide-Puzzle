using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GridCellsBuilder
{
    private const float CELL_CENTER_OFFSET = 0.5f;
    private const float HEX_HORIZONTAL_STEP = 1.5f;
    private const float HEX_ROW_OFFSET_FACTOR = 0.5f;
    
    private readonly Grid _grid;
    private readonly HexCellView _cellPrefab;
    private readonly DiContainer _container;

    private readonly Dictionary<Vector2Int, HexCellView> _cells = new();
    private readonly List<HexCellView> _cellPool = new();

    private int _usedCellCount;
    
    [Inject]
    public GridCellsBuilder(Grid grid, HexCellView cellPrefab, DiContainer container)
    {
        _grid = grid;
        _cellPrefab = cellPrefab;
        _container = container;
    }

    public IReadOnlyDictionary<Vector2Int, HexCellView> Cells => _cells;

    public void TryBuildRectangle(int width, int height)
    {
        Clear();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                
                InstantiateCell(coordinates);
            }
        }
    }

    public void TryBuildHexagon(int radius)
    {
        Clear();

        for (int i = -radius; i <= radius; i++)
        {
            int radiusMin = Mathf.Max(-radius, -i - radius);
            int radiusMax = Mathf.Min(radius, -i + radius);

            for (int j = radiusMin; j <= radiusMax; j++)
            {
                Vector2Int coordinates = new Vector2Int(i, j);
                
                InstantiateCell(coordinates);
            }
        }
    }

    private void Clear()
    {
        _cells.Clear();
        _usedCellCount = 0;

        for (int i = 0; i < _cellPool.Count; i++)
        {
            HexCellView cell = _cellPool[i];
            
            if (cell == null)
                return;
            
            cell.gameObject.SetActive(false);
        }
    }
    
    private void InstantiateCell(Vector2Int coordinates)
    {
        HexCellView cellView;

        if (_usedCellCount < _cellPool.Count && _cellPool[_usedCellCount] != null)
        {
            cellView = _cellPool[_usedCellCount];
        }
        else
        {
            cellView = _container.InstantiatePrefabForComponent<HexCellView>(_cellPrefab, _grid.transform);
            _cellPool.Add(cellView);
        }
        
        _usedCellCount++;
        
        cellView.gameObject.SetActive(true);

        Vector3 localCenter;

        if (_grid.cellLayout == GridLayout.CellLayout.Hexagon)
        {
            localCenter = ComputeHexCellPosition(coordinates, _grid);
        }
        else
        {
            Vector3Int cellPosition = new Vector3Int(coordinates.x, coordinates.y, 0);
            Vector3 localCenterXY = _grid.CellToLocalInterpolated(new Vector3(cellPosition.x + CELL_CENTER_OFFSET, cellPosition.y + CELL_CENTER_OFFSET, 0f));
            localCenter = new Vector3(localCenterXY.x, 0, localCenterXY.y);
        }
        
        cellView.transform.localPosition = localCenter;
        cellView.Initialize(_grid);
        
        _cells[coordinates] = cellView;
    }

    private Vector3 ComputeHexCellPosition(Vector2Int coordinates, Grid grid)
    {
        int axialQ = coordinates.x;
        int axialR = coordinates.y;

        float hexRadius = grid.cellSize.x * CELL_CENTER_OFFSET;

        float horizontalOffset = hexRadius * HEX_HORIZONTAL_STEP * axialQ;
        float verticalOffset = hexRadius * Mathf.Sqrt(3f) * (axialR + HEX_ROW_OFFSET_FACTOR * axialQ);

        return new Vector3(horizontalOffset, 0f, verticalOffset);
    }
}