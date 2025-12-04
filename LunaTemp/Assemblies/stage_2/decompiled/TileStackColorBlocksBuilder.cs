using System.Collections.Generic;
using UnityEngine;

public class TileStackColorBlocksBuilder
{
	private const int MIN_DISTINCT_COLORS = 1;

	private const int FALLBACK_DISTINCT_COLORS = 2;

	private const int TRIPLE_DISTINCT_COLORS = 3;

	private const int MIN_RANDOM_INCLUSIVE = 0;

	private const int RANDOM_RANGE_INCLUSIVE_OFFSET = 1;

	private const int MIN_TOTAL_WEIGHT = 0;

	private const int MIN_EFFECTIVE_COLORS_FOR_THREE = 3;

	private const int SINGLE_REMAINING_COLOR = 1;

	private const int OTHER_COLORS_OFFSET = 1;

	private readonly TileConfig _config;

	private readonly ListRandomizer _randomizer;

	private readonly List<TileColorBlock> _colorBlocksBuffer = new List<TileColorBlock>();

	public TileStackColorBlocksBuilder(TileConfig config, ListRandomizer randomizer)
	{
		_config = config;
		_randomizer = randomizer;
	}

	public int ChooseDistinctColorsCount(int maxDistinctColors, int stackSize, int minimumTilesPerColor)
	{
		int minDistinctColors = Mathf.Clamp(_config.MinColorBlocksPerStack, 1, maxDistinctColors);
		if (maxDistinctColors <= minDistinctColors)
		{
			return maxDistinctColors;
		}
		if (maxDistinctColors <= 1)
		{
			return 1;
		}
		int maxColorsBySize = Mathf.Max(1, stackSize / minimumTilesPerColor);
		int effectiveMaxColors = Mathf.Min(maxDistinctColors, maxColorsBySize);
		if (effectiveMaxColors <= 1)
		{
			return 1;
		}
		(int oneColorWeight, int twoColorsWeight, int threeColorsWeight) tuple = CalculateColorWeights(effectiveMaxColors);
		int oneColorWeight = tuple.oneColorWeight;
		int twoColorsWeight = tuple.twoColorsWeight;
		int threeColorsWeight = tuple.threeColorsWeight;
		int totalWeight = oneColorWeight + twoColorsWeight + threeColorsWeight;
		if (totalWeight <= 0)
		{
			return Mathf.Clamp(2, minDistinctColors, effectiveMaxColors);
		}
		int roll = Random.Range(0, totalWeight);
		int desiredColorsCount = ResolveDesiredColorsCountFromRoll(roll, oneColorWeight, twoColorsWeight);
		return Mathf.Clamp(desiredColorsCount, minDistinctColors, effectiveMaxColors);
	}

	public void BuildStackFromDistinctColors(IReadOnlyList<Color> distinctColors, int stackSize, int minimumTilesPerColor, List<Color> output)
	{
		output.Clear();
		if (distinctColors == null || distinctColors.Count == 0)
		{
			return;
		}
		int distinctCount = distinctColors.Count;
		int remainingTiles = stackSize;
		int remainingColors = distinctCount;
		int maxTilesPerColor = _config.MaxTilesPerColorSpawn;
		_colorBlocksBuffer.Clear();
		_colorBlocksBuffer.Capacity = Mathf.Max(_colorBlocksBuffer.Capacity, distinctCount);
		for (int index = 0; index < distinctCount; index++)
		{
			Color color = distinctColors[index];
			(int minTilesForThisColor, int maxTilesForThisColor) tuple = CalculateTilesRangeForColor(remainingTiles, remainingColors, minimumTilesPerColor, maxTilesPerColor);
			int minTilesForThisColor = tuple.minTilesForThisColor;
			int maxTilesForThisColor = tuple.maxTilesForThisColor;
			int tilesForThisColor = AllocateTilesForColor(remainingTiles, remainingColors, minTilesForThisColor, maxTilesForThisColor, maxTilesPerColor);
			remainingTiles -= tilesForThisColor;
			remainingColors--;
			_colorBlocksBuffer.Add(new TileColorBlock(color, tilesForThisColor));
		}
		DistributeRemainingTiles(remainingTiles, maxTilesPerColor, _colorBlocksBuffer);
		_randomizer.Shuffle(_colorBlocksBuffer);
		for (int i = 0; i < _colorBlocksBuffer.Count; i++)
		{
			TileColorBlock block = _colorBlocksBuffer[i];
			for (int j = 0; j < block.Count; j++)
			{
				output.Add(block.Color);
			}
		}
	}

	private (int oneColorWeight, int twoColorsWeight, int threeColorsWeight) CalculateColorWeights(int effectiveMaxColors)
	{
		int oneColorWeight = Mathf.Max(0, _config.OneColorStackWeightPercent);
		int twoColorsWeight = Mathf.Max(0, _config.TwoColorsStackWeightPercent);
		int threeColorsWeight = ((effectiveMaxColors >= 3) ? Mathf.Max(0, _config.ThreeColorsStackWeightPercent) : 0);
		if (effectiveMaxColors == 2)
		{
			threeColorsWeight = 0;
		}
		return (oneColorWeight, twoColorsWeight, threeColorsWeight);
	}

	private (int minTilesForThisColor, int maxTilesForThisColor) CalculateTilesRangeForColor(int remainingTiles, int remainingColors, int minimumTilesPerColor, int maxTilesPerColor)
	{
		int minimalSumForOtherColors = (remainingColors - 1) * minimumTilesPerColor;
		int maxTilesForThisColor = Mathf.Min(maxTilesPerColor, remainingTiles - minimalSumForOtherColors);
		if (maxTilesForThisColor < minimumTilesPerColor)
		{
			int safeMaxTilesForThisColor = remainingTiles - (remainingColors - 1) * minimumTilesPerColor;
			maxTilesForThisColor = Mathf.Min(maxTilesPerColor, Mathf.Max(minimumTilesPerColor, safeMaxTilesForThisColor));
		}
		return (minimumTilesPerColor, maxTilesForThisColor);
	}

	private int ResolveDesiredColorsCountFromRoll(int roll, int oneColorWeight, int twoColorsWeight)
	{
		int twoColorsThreshold = oneColorWeight + twoColorsWeight;
		if (roll < oneColorWeight)
		{
			return 1;
		}
		if (roll < twoColorsThreshold)
		{
			return 2;
		}
		return 3;
	}

	private int AllocateTilesForColor(int remainingTiles, int remainingColors, int minTilesForThisColor, int maxTilesForThisColor, int maxTilesPerColor)
	{
		int tilesForThisColor;
		if (remainingColors == 1)
		{
			tilesForThisColor = Mathf.Clamp(remainingTiles, minTilesForThisColor, maxTilesPerColor);
		}
		else
		{
			int maxExclusive = maxTilesForThisColor + 1;
			tilesForThisColor = Random.Range(minTilesForThisColor, maxExclusive);
		}
		return Mathf.Clamp(tilesForThisColor, minTilesForThisColor, maxTilesPerColor);
	}

	private void DistributeRemainingTiles(int remainingTiles, int maxTilesPerColor, List<TileColorBlock> colorBlocks)
	{
		int iterationsLeft = _config.MaxStackDistributionIterations;
		while (remainingTiles > 0 && iterationsLeft > 0)
		{
			iterationsLeft--;
			bool addedAny = false;
			for (int index = 0; index < colorBlocks.Count; index++)
			{
				if (remainingTiles <= 0)
				{
					break;
				}
				TileColorBlock block = colorBlocks[index];
				int canTake = maxTilesPerColor - block.Count;
				if (canTake > 0)
				{
					int add = Mathf.Min(canTake, remainingTiles);
					if (add > 0)
					{
						block.Count += add;
						remainingTiles -= add;
						colorBlocks[index] = block;
						addedAny = true;
					}
				}
			}
			if (!addedAny)
			{
				break;
			}
		}
	}
}
