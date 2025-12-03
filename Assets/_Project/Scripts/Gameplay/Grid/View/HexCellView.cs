using UnityEngine;

public class HexCellView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    
    public Vector2Int Coordinates { get; private set; }
    
    public void Initialize(Grid grid, Vector2Int coordinates)
    {
        Coordinates = coordinates;
        
        FitToGridCell(grid);
    }
    
    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }
    
    private void FitToGridCell(Grid grid)
    {
        if (_renderer == null)
            return;

        Vector3 boundsSize = _renderer.bounds.size;

        if (boundsSize.x <= 0.0001f || boundsSize.z <= 0.0001f)
            return;

        float targetSizeX;
        float targetSizeZ;

        if (grid.cellLayout == GridLayout.CellLayout.Hexagon)
        {
            float radius = grid.cellSize.x * 0.5f;
            targetSizeX = 2f * radius;
            targetSizeZ = Mathf.Sqrt(3f) * radius;
        }
        else
        {
            targetSizeX = grid.cellSize.x;
            targetSizeZ = grid.cellSize.y;
        }

        Vector3 scale = transform.localScale;
        scale.x *= targetSizeX / boundsSize.x;
        scale.z *= targetSizeZ / boundsSize.z;
        transform.localScale = scale;
    }
}