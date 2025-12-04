using System.Collections.Generic;
using UnityEngine;

public class TileStackDragInput : MonoBehaviour
{
	private const float GROUND_RAY_MAX_DISTANCE = 500f;

	private const float STACK_RAY_MAX_DISTANCE = 200f;

	private const float MIN_RAY_DIRECTION_EPSILON = 0.0001f;

	[SerializeField]
	private Camera _camera;

	[SerializeField]
	private LayerMask _stackLayerMask;

	[SerializeField]
	private float _maxCellPickDistance = 1f;

	private GridCellsBuilder _cellsBuilder;

	private TileStackFactory _tileStackFactory;

	private TileStackDraggable _activeDraggableStack;

	private TileStackMatchResolver _matchResolver;

	private Vector2 _currentPointerPosition;

	public void Construct(TileStackFactory tileStackFactory, GridCellsBuilder cellsBuilder, TileStackMatchResolver matchResolver)
	{
		_tileStackFactory = tileStackFactory;
		_cellsBuilder = cellsBuilder;
		_matchResolver = matchResolver;
	}

	private void Awake()
	{
		if (_camera == null)
		{
			_camera = Camera.main;
		}
	}

	private void OnEnable()
	{
		if (_camera == null)
		{
			_camera = Camera.main;
		}
	}

	private void Update()
	{
		if (_camera == null)
		{
			return;
		}
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			_currentPointerPosition = touch.position;
			switch (touch.phase)
			{
			case TouchPhase.Began:
				TryBeginDrag();
				break;
			case TouchPhase.Moved:
			case TouchPhase.Stationary:
				if (_activeDraggableStack != null)
				{
					Vector3 worldPosition2 = GetWorldPointOnGround(_currentPointerPosition);
					_activeDraggableStack.UpdateDrag(worldPosition2);
				}
				break;
			case TouchPhase.Ended:
			case TouchPhase.Canceled:
				TryEndDrag();
				break;
			}
			return;
		}
		_currentPointerPosition = Input.mousePosition;
		if (Input.GetMouseButtonDown(0))
		{
			TryBeginDrag();
		}
		else if (Input.GetMouseButton(0))
		{
			if (_activeDraggableStack != null)
			{
				Vector3 worldPosition = GetWorldPointOnGround(_currentPointerPosition);
				_activeDraggableStack.UpdateDrag(worldPosition);
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			TryEndDrag();
		}
	}

	private Vector3 GetWorldPointOnGround(Vector2 screenPosition)
	{
		if (_camera == null)
		{
			return Vector3.zero;
		}
		Ray ray = _camera.ScreenPointToRay(screenPosition);
		if (Physics.Raycast(ray, out var hit, 500f))
		{
			return hit.point;
		}
		float rayDirectionY = ray.direction.y;
		if (Mathf.Abs(rayDirectionY) > 0.0001f)
		{
			float distance = (0f - ray.origin.y) / rayDirectionY;
			return ray.origin + ray.direction * distance;
		}
		return ray.origin;
	}

	private bool TryPlaceActiveStack()
	{
		if (_activeDraggableStack == null)
		{
			return false;
		}
		if (!TryFindCellUnderPointer(out var cellView))
		{
			return false;
		}
		if (cellView == null)
		{
			return false;
		}
		if (_tileStackFactory.HasActiveStack(cellView))
		{
			return false;
		}
		_activeDraggableStack.PlaceToCell(cellView);
		_matchResolver.ResolveMatchesFromCell();
		return true;
	}

	private bool RaycastForStack(Vector2 screenPosition, out TileStackDraggable draggable)
	{
		draggable = null;
		if (_camera == null)
		{
			return false;
		}
		Ray ray = _camera.ScreenPointToRay(screenPosition);
		if (Physics.Raycast(ray, out var hit, 200f, _stackLayerMask))
		{
			TileStackRoot stackRoot = hit.collider.GetComponentInParent<TileStackRoot>();
			if (stackRoot != null)
			{
				draggable = stackRoot.Draggable;
			}
		}
		return draggable != null;
	}

	private bool TryFindCellUnderPointer(out HexCellView cellView)
	{
		cellView = null;
		if (_camera == null || _cellsBuilder == null)
		{
			return false;
		}
		Vector3 worldPoint = GetWorldPointOnGround(_currentPointerPosition);
		float bestDistanceSqr = float.MaxValue;
		HexCellView bestCell = null;
		IReadOnlyDictionary<Vector2Int, HexCellView> cells = _cellsBuilder.Cells;
		foreach (HexCellView candidate in cells.Values)
		{
			if (!(candidate == null))
			{
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
		}
		if (bestCell == null)
		{
			return false;
		}
		if (bestDistanceSqr > _maxCellPickDistance * _maxCellPickDistance)
		{
			return false;
		}
		cellView = bestCell;
		return true;
	}

	private void TryBeginDrag()
	{
		if (_activeDraggableStack == null && (_matchResolver == null || !_matchResolver.IsResolvingMatches) && RaycastForStack(_currentPointerPosition, out var draggable) && draggable.CanBeDragged)
		{
			_activeDraggableStack = draggable;
			Vector3 worldPosition = GetWorldPointOnGround(_currentPointerPosition);
			_activeDraggableStack.BeginDrag(worldPosition);
		}
	}

	private void TryEndDrag()
	{
		if (_activeDraggableStack != null)
		{
			if (!TryPlaceActiveStack())
			{
				_activeDraggableStack.ReturnToSpawn();
			}
			_activeDraggableStack.StopDrag();
			_activeDraggableStack = null;
		}
	}
}
