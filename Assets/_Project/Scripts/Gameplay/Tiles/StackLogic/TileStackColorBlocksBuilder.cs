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
    private const int MIN_EFFECTIVE_COLORS_FOR_THREE = TRIPLE_DISTINCT_COLORS;

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
        int minDistinctColors = Mathf.Clamp(_config.MinColorBlocksPerStack, MIN_DISTINCT_COLORS, maxDistinctColors);

        if (maxDistinctColors <= minDistinctColors)
            return maxDistinctColors;

        if (maxDistinctColors <= MIN_DISTINCT_COLORS)
            return MIN_DISTINCT_COLORS;

        int maxColorsBySize = Mathf.Max(MIN_DISTINCT_COLORS, stackSize / minimumTilesPerColor);
        int effectiveMaxColors = Mathf.Min(maxDistinctColors, maxColorsBySize);

        if (effectiveMaxColors <= MIN_DISTINCT_COLORS)
            return MIN_DISTINCT_COLORS;

        (int oneColorWeight, int twoColorsWeight, int threeColorsWeight) = CalculateColorWeights(effectiveMaxColors);

        int totalWeight = oneColorWeight + twoColorsWeight + threeColorsWeight;

        if (totalWeight <= MIN_TOTAL_WEIGHT)
            return Mathf.Clamp(FALLBACK_DISTINCT_COLORS, minDistinctColors, effectiveMaxColors);

        int roll = Random.Range(MIN_RANDOM_INCLUSIVE, totalWeight);
        int desiredColorsCount = ResolveDesiredColorsCountFromRoll(roll, oneColorWeight, twoColorsWeight);

        return Mathf.Clamp(desiredColorsCount, minDistinctColors, effectiveMaxColors);
    }

    public void BuildStackFromDistinctColors(IReadOnlyList<Color> distinctColors, int stackSize, int minimumTilesPerColor,
        List<Color> output)
    {
        output.Clear();

        if (distinctColors == null || distinctColors.Count == 0)
            return;

        int distinctCount = distinctColors.Count;
        int remainingTiles = stackSize;
        int remainingColors = distinctCount;

        int maxTilesPerColor = _config.MaxTilesPerColorSpawn;

        _colorBlocksBuffer.Clear();
        _colorBlocksBuffer.Capacity = Mathf.Max(_colorBlocksBuffer.Capacity, distinctCount);

        for (int index = 0; index < distinctCount; index++)
        {
            Color color = distinctColors[index];

            (int minTilesForThisColor, int maxTilesForThisColor) = CalculateTilesRangeForColor(remainingTiles,
                remainingColors, minimumTilesPerColor, maxTilesPerColor);

            int tilesForThisColor = AllocateTilesForColor(remainingTiles, remainingColors, minTilesForThisColor,
                maxTilesForThisColor, maxTilesPerColor);

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
                output.Add(block.Color);
        }
    }

    private (int oneColorWeight, int twoColorsWeight, int threeColorsWeight) CalculateColorWeights(int effectiveMaxColors)
    {
        int oneColorWeight = Mathf.Max(MIN_TOTAL_WEIGHT, _config.OneColorStackWeightPercent);
        int twoColorsWeight = Mathf.Max(MIN_TOTAL_WEIGHT, _config.TwoColorsStackWeightPercent);
        int threeColorsWeight = effectiveMaxColors >= MIN_EFFECTIVE_COLORS_FOR_THREE
            ? Mathf.Max(MIN_TOTAL_WEIGHT, _config.ThreeColorsStackWeightPercent) : MIN_TOTAL_WEIGHT;
        
        if (effectiveMaxColors == FALLBACK_DISTINCT_COLORS)
            threeColorsWeight = MIN_TOTAL_WEIGHT;

        return (oneColorWeight, twoColorsWeight, threeColorsWeight);
    }

    private (int minTilesForThisColor, int maxTilesForThisColor) CalculateTilesRangeForColor(int remainingTiles, 
        int remainingColors, int minimumTilesPerColor, int maxTilesPerColor)
    {
        int minTilesForThisColor = minimumTilesPerColor;

        int minimalSumForOtherColors = (remainingColors - OTHER_COLORS_OFFSET) * minimumTilesPerColor;
        int maxTilesForThisColor = Mathf.Min(maxTilesPerColor, remainingTiles - minimalSumForOtherColors);

        if (maxTilesForThisColor < minTilesForThisColor)
        {
            int safeMaxTilesForThisColor = remainingTiles - (remainingColors - OTHER_COLORS_OFFSET) * minimumTilesPerColor;
            maxTilesForThisColor = Mathf.Min(maxTilesPerColor, Mathf.Max(minTilesForThisColor, safeMaxTilesForThisColor));
        }

        return (minTilesForThisColor, maxTilesForThisColor);
    }
    
    private int ResolveDesiredColorsCountFromRoll(int roll, int oneColorWeight, int twoColorsWeight)
    {
        int twoColorsThreshold = oneColorWeight + twoColorsWeight;

        if (roll < oneColorWeight)
            return MIN_DISTINCT_COLORS;

        if (roll < twoColorsThreshold)
            return FALLBACK_DISTINCT_COLORS;
        
        return TRIPLE_DISTINCT_COLORS;
    }

    private int AllocateTilesForColor(int remainingTiles, int remainingColors, int minTilesForThisColor, 
        int maxTilesForThisColor, int maxTilesPerColor)
    {
        int tilesForThisColor;

        if (remainingColors == SINGLE_REMAINING_COLOR)
        {
            tilesForThisColor = Mathf.Clamp(remainingTiles, minTilesForThisColor, maxTilesPerColor);
        }
        else
        {
            int maxExclusive = maxTilesForThisColor + RANDOM_RANGE_INCLUSIVE_OFFSET;
            tilesForThisColor = Random.Range(minTilesForThisColor, maxExclusive);
        }

        tilesForThisColor = Mathf.Clamp(tilesForThisColor, minTilesForThisColor, maxTilesPerColor);

        return tilesForThisColor;
    }
    
    private void DistributeRemainingTiles(int remainingTiles, int maxTilesPerColor, List<TileColorBlock> colorBlocks)
    {
        int iterationsLeft = _config.MaxStackDistributionIterations;

        while (remainingTiles > MIN_TOTAL_WEIGHT && iterationsLeft > MIN_TOTAL_WEIGHT)
        {
            iterationsLeft--;

            bool addedAny = false;

            for (int index = 0; index < colorBlocks.Count && remainingTiles > MIN_TOTAL_WEIGHT; index++)
            {
                TileColorBlock block = colorBlocks[index];

                int canTake = maxTilesPerColor - block.Count;
                
                if (canTake <= MIN_TOTAL_WEIGHT)
                    continue;

                int add = Mathf.Min(canTake, remainingTiles);
                
                if (add <= MIN_TOTAL_WEIGHT)
                    continue;

                block.Count += add;
                remainingTiles -= add;
                colorBlocks[index] = block;
                addedAny = true;
            }

            if (!addedAny)
                break;
        }
    }
}