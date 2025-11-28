using System.Collections.Generic;
using UnityEngine;

public class TileStackPoolCleaner
{
    private readonly List<TileStackView> _stackBuffer = new();

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
                TileStackView stackView = _stackBuffer[index];
                
                if (stackView == null)
                    continue;

                stackView.gameObject.SetActive(false);
            }
        }

        _stackBuffer.Clear();
    }
}