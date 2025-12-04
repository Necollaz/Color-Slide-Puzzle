using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TileStackRoot : MonoBehaviour
{
	[SerializeField]
	private TileStackView _tileStackView;

	[SerializeField]
	private float _dragLerpSpeed = 25f;

	private TileStackDraggable _draggable;

	public TileStackView TileStackView => _tileStackView;

	public TileStackDraggable Draggable => _draggable;

	private void Awake()
	{
		if (_tileStackView == null)
		{
			_tileStackView = GetComponentInChildren<TileStackView>();
		}
		_draggable = new TileStackDraggable(base.transform, _dragLerpSpeed);
	}

	private void Update()
	{
		_draggable?.Tick(Time.deltaTime);
	}
}
