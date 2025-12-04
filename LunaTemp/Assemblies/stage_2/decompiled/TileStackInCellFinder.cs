public class TileStackInCellFinder
{
	public TileStackView FindActiveStackInCell(HexCellView cell)
	{
		if (cell == null)
		{
			return null;
		}
		TileStackView[] stacks = cell.GetComponentsInChildren<TileStackView>(true);
		TileStackView best = null;
		foreach (TileStackView candidate in stacks)
		{
			if (!(candidate == null) && candidate.gameObject.activeInHierarchy && !candidate.IsEmpty && (best == null || candidate.TilesCount > best.TilesCount))
			{
				best = candidate;
			}
		}
		return best;
	}
}
