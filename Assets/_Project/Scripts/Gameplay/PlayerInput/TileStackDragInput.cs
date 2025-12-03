using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class TileStackDragInput : MonoBehaviour, GameplayInputActions.IGameplayActions
{
    private const float GROUND_RAY_MAX_DISTANCE = 500.0f;
    private const float STACK_RAY_MAX_DISTANCE = 200.0f;
    private const float MIN_RAY_DIRECTION_EPSILON = 0.0001f;
    
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _stackLayerMask;
    [SerializeField] private float _maxCellPickDistance = 1.0f;
    
    private GameplayInputActions _inputActions;
    private GridCellsBuilder _cellsBuilder;
    private TileStackFactory _tileStackFactory;
    private TileStackDraggable _activeDraggableStack;
    private TileStackMatchResolver _matchResolver;
    
    private Vector2 _currentPointerPosition;

    [Inject]
    public void Construct(GameplayInputActions inputActions, TileStackFactory tileStackFactory, 
        GridCellsBuilder cellsBuilder, TileStackMatchResolver matchResolver)
    {
        _inputActions = inputActions;
        _tileStackFactory = tileStackFactory;
        _cellsBuilder = cellsBuilder;
        _matchResolver = matchResolver;
    }

    private void OnEnable()
    {
        if (_camera == null)
            _camera = Camera.main;
        
        if (_inputActions == null)
            return;

        _inputActions.Gameplay.AddCallbacks(this);
        _inputActions.Gameplay.Enable();
    }

    private void OnDisable()
    {
        if (_inputActions == null)
            return;
        
        _inputActions.Gameplay.RemoveCallbacks(this);
        _inputActions.Gameplay.Disable();
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        _currentPointerPosition = context.ReadValue<Vector2>();

        if (_activeDraggableStack == null)
            return;

        Vector3 worldPosition = GetWorldPointOnGround(_currentPointerPosition);
        _activeDraggableStack.UpdateDrag(worldPosition);
    }

    public void OnPress(InputAction.CallbackContext context)
    {
        if (context.started)
            TryBeginDrag();
        else if (context.canceled)
            TryEndDrag();
    }

    private Vector3 GetWorldPointOnGround(Vector2 screenPosition)
    {
        if (_camera == null)
            return Vector3.zero;

        Ray ray = _camera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, GROUND_RAY_MAX_DISTANCE))
            return hit.point;

        float rayDirectionY = ray.direction.y;

        if (Mathf.Abs(rayDirectionY) > MIN_RAY_DIRECTION_EPSILON)
        {
            float distance = -ray.origin.y / rayDirectionY;
            
            return ray.origin + ray.direction * distance;
        }

        return ray.origin;
    }
    
    private bool TryPlaceActiveStack()
    {
        if (_activeDraggableStack == null)
            return false;

        if (!TryFindCellUnderPointer(out HexCellView cellView))
            return false;

        if (cellView == null)
            return false;
        
        if (_tileStackFactory.HasActiveStack(cellView))
            return false;

        _activeDraggableStack.PlaceToCell(cellView);
        _matchResolver.ResolveMatchesFromCell();
        
        return true;
    }

    private bool RaycastForStack(Vector2 screenPosition, out TileStackDraggable draggable)
    {
        draggable = null;

        if (_camera == null)
            return false;

        Ray ray = _camera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, STACK_RAY_MAX_DISTANCE, _stackLayerMask))
        {
            TileStackRoot stackRoot = hit.collider.GetComponentInParent<TileStackRoot>();

            if (stackRoot != null)
                draggable = stackRoot.Draggable;
        }

        return draggable != null;
    }

    private bool TryFindCellUnderPointer(out HexCellView cellView)
    {
        cellView = null;

        if (_camera == null || _cellsBuilder == null)
            return false;

        Vector3 worldPoint = GetWorldPointOnGround(_currentPointerPosition);
        float bestDistanceSqr = float.MaxValue;
        HexCellView bestCell = null;

        IReadOnlyDictionary<Vector2Int, HexCellView> cells = _cellsBuilder.Cells;

        foreach (HexCellView candidate in cells.Values)
        {
            if (candidate == null)
                continue;

            Vector3 cellPosition = candidate.transform.position;
            Vector2 cellXZ = new Vector2(cellPosition.x, cellPosition.z);
            Vector2 pointXZ = new Vector2(worldPoint.x, worldPoint.z);

            float distanceSqr = (cellXZ - pointXZ).sqrMagnitude;

            if (distanceSqr < bestDistanceSqr)
            {
                bestDistanceSqr = distanceSqr;
                bestCell = candidate;
            }
        }

        if (bestCell == null)
            return false;
        
        if (bestDistanceSqr > _maxCellPickDistance * _maxCellPickDistance)
            return false;

        cellView = bestCell;
        
        return true;
    }
    
    private void TryBeginDrag()
    {
        if (_activeDraggableStack != null)
            return;

        if (_matchResolver != null && _matchResolver.IsResolvingMatches)
            return;
        
        if (!RaycastForStack(_currentPointerPosition, out TileStackDraggable draggable))
            return;

        if (!draggable.CanBeDragged)
            return;

        _activeDraggableStack = draggable;
        Vector3 worldPosition = GetWorldPointOnGround(_currentPointerPosition);
        _activeDraggableStack.BeginDrag(worldPosition);
    }

    private void TryEndDrag()
    {
        if (_activeDraggableStack == null)
            return;

        bool placedSuccessfully = TryPlaceActiveStack();

        if (!placedSuccessfully)
            _activeDraggableStack.ReturnToSpawn();

        _activeDraggableStack.StopDrag();
        _activeDraggableStack = null;
    }
}