using System.Collections.Generic;
using UnityEngine;

public class TileEmptyCellSelector
{
	public Vector2Int ChooseEmptyCellClosestToCamera(IReadOnlyDictionary<Vector2Int, HexCellView> cells)
	{
		Camera mainCamera = Camera.main;
		if (mainCamera == null || cells.Count == 0)
		{
			using (IEnumerator<KeyValuePair<Vector2Int, HexCellView>> enumerator = cells.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current.Key;
				}
			}
			return default(Vector2Int);
		}
		Vector3 cameraWorldPosition = mainCamera.transform.position;
		Vector2 cameraPositionXZ = new Vector2(cameraWorldPosition.x, cameraWorldPosition.z);
		float bestDistance = float.MaxValue;
		Vector2Int bestCoordinates = default(Vector2Int);
		bool firstFound = false;
		foreach (KeyValuePair<Vector2Int, HexCellView> cell in cells)
		{
			cell.Deconstruct(out var key, out var value);
			Vector2Int coordinates = key;
			HexCellView cellView = value;
			if (!(cellView == null))
			{
				Vector3 cellWorldPosition = cellView.transform.position;
				Vector2 cellPositionXZ = new Vector2(cellWorldPosition.x, cellWorldPosition.z);
				float distance = (cellPositionXZ - cameraPositionXZ).magnitude;
				if (!firstFound || distance < bestDistance)
				{
					firstFound = true;
					bestDistance = distance;
					bestCoordinates = coordinates;
				}
			}
		}
		return bestCoordinates;
	}
}
