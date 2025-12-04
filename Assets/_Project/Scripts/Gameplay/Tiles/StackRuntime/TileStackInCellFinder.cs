public class TileStackInCellFinder
{
    public TileStackView FindActiveStackInCell(HexCellView cell)
    {
        if (cell == null)
            return null;

        TileStackView[] stacks = cell.GetComponentsInChildren<TileStackView>(true);

        TileStackView best = null;

        for (int i = 0; i < stacks.Length; i++)
        {
            TileStackView candidate = stacks[i];

            if (candidate == null)
                continue;

            if (!candidate.gameObject.activeInHierarchy)
                continue;

            if (candidate.IsEmpty)
                continue;

            if (best == null || candidate.TilesCount > best.TilesCount)
                best = candidate;
        }

        return best;
    }
}