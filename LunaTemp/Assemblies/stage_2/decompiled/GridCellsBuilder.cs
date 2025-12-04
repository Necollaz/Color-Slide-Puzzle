using System.Collections.Generic;
using UnityEngine;

public class GridCellsBuilder
{
	private const float HEX_RADIUS = 0.5f;

	private const float HEX_HORIZONTAL_STEP = 1.5f;

	private const float HEX_ROW_OFFSET_FACTOR = 0.5f;

	private readonly Grid _grid;

	private readonly HexCellView _cellPrefab;

	private readonly Dictionary<Vector2Int, HexCellView> _cells = new Dictionary<Vector2Int, HexCellView>();

	private readonly List<HexCellView> _cellPool = new List<HexCellView>();

	private int _usedCellCount;

	public IReadOnlyDictionary<Vector2Int, HexCellView> Cells => _cells;

	public GridCellsBuilder(Grid grid, HexCellView cellPrefab)
	{
		_grid = grid;
		_cellPrefab = cellPrefab;
	}

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
				InstantiateCell(new Vector2Int(i, j));
			}
		}
	}

	private void Clear()
	{
		foreach (HexCellView cell in _cellPool)
		{
			if (cell != null)
			{
				cell.gameObject.SetActive(false);
			}
		}
		_cells.Clear();
		_usedCellCount = 0;
	}

	private void InstantiateCell(Vector2Int coordinates)
	{
		HexCellView cellView;
		if (_usedCellCount < _cellPool.Count)
		{
			cellView = _cellPool[_usedCellCount];
		}
		else
		{
			cellView = Object.Instantiate(_cellPrefab, _grid.transform);
			_cellPool.Add(cellView);
		}
		_usedCellCount++;
		cellView.gameObject.SetActive(true);
		Vector3 pos = ComputeHexPosition(coordinates);
		cellView.transform.localPosition = pos;
		cellView.Initialize(coordinates);
		_cells[coordinates] = cellView;
	}

	private Vector3 ComputeHexPosition(Vector2Int c)
	{
		float x = 0.75f * (float)c.x;
		float z = 0.5f * Mathf.Sqrt(3f) * ((float)c.y + 0.5f * (float)c.x);
		return new Vector3(x, 0f, z);
	}
}
