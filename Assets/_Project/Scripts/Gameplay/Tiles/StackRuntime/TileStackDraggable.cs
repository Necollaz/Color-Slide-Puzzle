using UnityEngine;

public class TileStackDraggable
{
    private readonly Transform _transform;
    private readonly float _dragLerpSpeed;
    
    private TileStackSpawnPoint _spawnPoint;
    private Vector3 _dragTargetPosition;
    
    private float _dragPlaneY;
    private bool _isPlaced;
    private bool _isDragging;
    
    public bool CanBeDragged => !_isPlaced && _spawnPoint != null;

    public TileStackDraggable(Transform transform, float dragLerpSpeed)
    {
        _transform = transform;
        _dragLerpSpeed = dragLerpSpeed;
    }
    
    public void Tick(float deltaTime)
    {
        if (!_isDragging || _transform == null)
            return;

        Vector3 currentPosition = _transform.position;
        Vector3 targetPosition = _dragTargetPosition;

        _transform.position = Vector3.Lerp(currentPosition, targetPosition, deltaTime * _dragLerpSpeed);
    }
    
    public void Initialize(TileStackSpawnPoint spawnPoint)
    {
        _spawnPoint = spawnPoint;
        _isPlaced = false;
        _isDragging = false;

        ReturnToSpawn();
    }

    public void BeginDrag(Vector3 worldPosition)
    {
        if (!CanBeDragged)
            return;

        _isDragging = true;

        _dragPlaneY = _transform.position.y;
        worldPosition.y = _dragPlaneY;
        _dragTargetPosition = worldPosition;
    }

    public void UpdateDrag(Vector3 worldPosition)
    {
        if (!_isDragging)
            return;

        worldPosition.y = _dragPlaneY;
        _dragTargetPosition = worldPosition;
    }

    public void StopDrag()
    {
        _isDragging = false;
    }

    public void PlaceToCell(HexCellView cellView)
    {
        if (cellView == null)
            return;

        _isDragging = false;
        _isPlaced = true;

        _transform.SetParent(cellView.transform, worldPositionStays: true);
        _transform.localPosition = Vector3.zero;
        
        if (_spawnPoint != null)
            _spawnPoint.NotifyStackPlaced(this);
    }

    public void ReturnToSpawn()
    {
        _isDragging = false;
        _isPlaced = false;

        if (_spawnPoint == null)
            return;

        _transform.SetParent(_spawnPoint.transform, worldPositionStays: true);
        _transform.localPosition = Vector3.zero;
    }
}