using UnityEngine;

public class HexCellView : MonoBehaviour
{
	[SerializeField]
	private MeshRenderer _renderer;

	public Vector2Int Coordinates { get; private set; }

	public void Initialize(Vector2Int coordinates)
	{
		Coordinates = coordinates;
	}

	public void SetVisible(bool visible)
	{
		base.gameObject.SetActive(visible);
	}
}
