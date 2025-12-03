using System.Collections.Generic;
using UnityEngine;

public class TileEmptyCellSelector
{
    public Vector2Int ChooseEmptyCellClosestToCamera(IReadOnlyDictionary<Vector2Int, HexCellView> cells)
    {
        Camera mainCamera = Camera.main;

        if (mainCamera == null || cells.Count == 0)
        {
            foreach (var pair in cells)
                return pair.Key;

            return default;
        }
        
        Vector3 cameraWorldPosition = mainCamera.transform.position;
        Vector2 cameraPositionXZ = new Vector2(cameraWorldPosition.x, cameraWorldPosition.z);
        
        float bestDistance = float.MaxValue;
        Vector2Int bestCoordinates = default;
        bool firstFound = false;

        foreach ( (Vector2Int coordinates, HexCellView cellView) in cells)
        {
            if (cellView == null)
                continue;
            
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

        return bestCoordinates;
    }
}