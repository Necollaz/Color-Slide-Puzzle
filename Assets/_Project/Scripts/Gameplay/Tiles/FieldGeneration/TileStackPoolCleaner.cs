using System.Collections.Generic;
using UnityEngine;

public class TileStackPoolCleaner
{
    private readonly ObjectPool<List<TileStackRoot>> _stackBufferPool;

    public TileStackPoolCleaner()
    {
        _stackBufferPool = new ObjectPool<List<TileStackRoot>>(() => new List<TileStackRoot>(), 
            buffer => buffer.Clear(),
            buffer => buffer.Clear());
    }

    public void DeactivateStacks(IReadOnlyDictionary<Vector2Int, HexCellView> cellViewsByCoordinates)
    {
        if (cellViewsByCoordinates == null || cellViewsByCoordinates.Count == 0)
            return;

        foreach (HexCellView cellView in cellViewsByCoordinates.Values)
        {
            if (cellView == null)
                continue;

            List<TileStackRoot> stackBuffer = _stackBufferPool.Get();

            cellView.GetComponentsInChildren(true, stackBuffer);

            for (int index = 0; index < stackBuffer.Count; index++)
            {
                TileStackRoot stackRoot = stackBuffer[index];

                if (stackRoot == null)
                    continue;

                stackRoot.gameObject.SetActive(false);
            }

            _stackBufferPool.Release(stackBuffer);
        }
    }
}