using UnityEngine;

public class TileEffectsPlayer
{
	private readonly TileEffectPool _tileClearEffectPool;

	private readonly TileEffectPool _stackClearEffectPool;

	public TileEffectsPlayer(Grid grid, TileConfig config, TileMergeSegmentTemplate segmentTemplate)
	{
		Transform rootParent = new GameObject("TileEffectsRoot").transform;
		if (grid != null)
		{
			rootParent.SetParent(grid.transform, false);
		}
		Vector3 tileScale = segmentTemplate.SegmentLocalScale;
		if (config.TileClearEffectPrefab != null)
		{
			_tileClearEffectPool = new TileEffectPool(config.TileClearEffectPrefab, rootParent, config.TileClearEffectPrewarmCount, tileScale);
		}
		if (config.StackClearEffectPrefab != null)
		{
			_stackClearEffectPool = new TileEffectPool(config.StackClearEffectPrefab, rootParent, config.StackClearEffectPrewarmCount, tileScale);
		}
	}

	public void PlayTileClearEffect(Vector3 position)
	{
		_tileClearEffectPool?.PlayEffectAt(position);
	}

	public void PlayStackClearEffect(Vector3 position)
	{
		_stackClearEffectPool?.PlayEffectAt(position);
	}
}
