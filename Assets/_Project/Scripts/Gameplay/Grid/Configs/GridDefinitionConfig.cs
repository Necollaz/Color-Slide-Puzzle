using UnityEngine;

[CreateAssetMenu(fileName = "LevelGridDefinition", menuName = "Game/Level/Grid Definition")]
public class GridDefinitionConfig : ScriptableObject
{
    [Header("Shape")]
    [SerializeField] private GridShapeType _shapeType = GridShapeType.Rectangle;
    [SerializeField, Min(1)] private int _width = 5;
    [SerializeField, Min(1)] private int _height = 5;
    [SerializeField, Min(1)] private int _hexRadius = 3;
    
    [Header("Stacks")]
    [SerializeField, Min(1)] private int _occupiedCellsCount = 5;

    public GridShapeType ShapeType => _shapeType;
    public int Width => _width;
    public int Height => _height;
    public int HexRadius => _hexRadius;
    public int OccupiedCellsCount => _occupiedCellsCount;
}