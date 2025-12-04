using UnityEngine;

public class GridNeighborOffsetProvider
{
    private readonly Vector2Int[] _rectangleNeighborOffsets =
    {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
    };

    private readonly Vector2Int[] _hexagonNeighborOffsets =
    {
        new Vector2Int(1, 0),
        new Vector2Int(1, -1),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int(0, 1),
    };

    public Vector2Int[] GetNeighborOffsets(Grid grid)
    {
        if (grid == null)
            return _rectangleNeighborOffsets;

        return grid.cellLayout == GridLayout.CellLayout.Hexagon ? _hexagonNeighborOffsets : _rectangleNeighborOffsets;
    }
}