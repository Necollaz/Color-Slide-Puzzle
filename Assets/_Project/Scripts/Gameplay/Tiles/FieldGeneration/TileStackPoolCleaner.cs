using System.Collections.Generic;
using UnityEngine;

public class TileStackPoolCleaner
{
    private readonly List<TileStackRoot> _stackBuffer = new();

    public void DeactivateStacks(IReadOnlyDictionary<Vector2Int, HexCellView> cellViewsByCoordinates)
    {
        foreach (HexCellView cellView in cellViewsByCoordinates.Values)
        {
            if (cellView == null)
                continue;

            _stackBuffer.Clear();
            cellView.GetComponentsInChildren(true, _stackBuffer);

            for (int index = 0; index < _stackBuffer.Count; index++)
            {
                TileStackRoot stackRoot = _stackBuffer[index];
                
                if (stackRoot == null)
                    continue;

                stackRoot.gameObject.SetActive(false);
            }
        }

        _stackBuffer.Clear();
    }
}