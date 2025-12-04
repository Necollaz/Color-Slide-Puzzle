using UnityEngine;

public class TileStackSpawnPoint : MonoBehaviour
{
	private const bool SPAWN_ON_START = true;

	private TileStackFactory _tileStackFactory;

	private TileStackRoot _currentStackRoot;

	public void Construct(TileStackFactory tileStackFactory)
	{
		_tileStackFactory = tileStackFactory;
	}

	private void Start()
	{
		bool flag = true;
		TrySpawnStack();
	}

	public void NotifyStackPlaced(TileStackDraggable draggable)
	{
		if (!(_currentStackRoot == null))
		{
			TileStackDraggable currentDraggable = _currentStackRoot.Draggable;
			if (currentDraggable != null && currentDraggable == draggable)
			{
				_currentStackRoot = null;
				TrySpawnStack();
			}
		}
	}

	private void TrySpawnStack()
	{
		if (_tileStackFactory != null && (!(_currentStackRoot != null) || !_currentStackRoot.gameObject.activeSelf))
		{
			_currentStackRoot = _tileStackFactory.CreateOrReuseSpawnStack(base.transform);
			if (!(_currentStackRoot == null))
			{
				_currentStackRoot.Draggable?.Initialize(this);
			}
		}
	}
}
