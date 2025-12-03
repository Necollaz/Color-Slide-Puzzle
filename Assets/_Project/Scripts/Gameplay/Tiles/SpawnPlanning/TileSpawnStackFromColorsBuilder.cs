using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileSpawnStackFromColorsBuilder
{
    private const int MIN_DISTINCT_COLORS = 1;
    private const int MIN_TOTAL_WEIGHT = 0;

    private readonly TileConfig _tileConfig;
    private readonly TileColorStatistics _colorStatistics;

    [Inject]
    public TileSpawnStackFromColorsBuilder(TileConfig tileConfig, TileColorStatistics colorStatistics)
    {
        _tileConfig = tileConfig;
        _colorStatistics = colorStatistics;
    }

    public bool TryBuildHelpfulSpawnStackFromColors(IReadOnlyList<Color> sourceColors, int stackSize, 
        int minimumTilesPerColor, int maxStackThreshold, List<Color> resultColors)
    {
        resultColors.Clear();

        if (sourceColors == null || sourceColors.Count == 0)
            return false;

        int maxDistinctColors = Mathf.Min(_tileConfig.MaxColorsPerSpawnStack, sourceColors.Count);
        int maxColorsBySize = Mathf.Max(MIN_DISTINCT_COLORS, stackSize / minimumTilesPerColor);
        int effectiveMaxDistinctColors = Mathf.Min(maxDistinctColors, maxColorsBySize);

        if (effectiveMaxDistinctColors <= 0)
            return false;

        List<Color> chosenColors = new List<Color>(effectiveMaxDistinctColors);

        for (int index = 0; index < effectiveMaxDistinctColors; index++)
            chosenColors.Add(sourceColors[index]);

        int remainingTiles = stackSize;
        int remainingColors = effectiveMaxDistinctColors;

        int maxTilesPerColorSpawn = _tileConfig.MaxTilesPerColorSpawn;

        List<(Color color, int count)> colorBlocks = new List<(Color color, int count)>(effectiveMaxDistinctColors);

        for (int index = 0; index < effectiveMaxDistinctColors; index++)
        {
            Color color = chosenColors[index];

            int totalColorCount = _colorStatistics.GetColorCount(color);
            int remainder = maxStackThreshold > 0 ? totalColorCount % maxStackThreshold : 0;

            if (remainder == 0 && totalColorCount > 0)
                remainder = maxStackThreshold;

            int neededToThreshold = maxStackThreshold > 0 ? maxStackThreshold - remainder : minimumTilesPerColor;
            int minimalTilesForThisColor = minimumTilesPerColor;
            int minimalSumForOtherColors = (remainingColors - 1) * minimumTilesPerColor;
            int maximalTilesForThisColor = Mathf.Min(maxTilesPerColorSpawn, remainingTiles - minimalSumForOtherColors);

            if (maximalTilesForThisColor < minimalTilesForThisColor)
            {
                minimalTilesForThisColor = minimumTilesPerColor;

                int safeMaxForThisColor =
                    remainingTiles - (remainingColors - 1) * minimumTilesPerColor;

                maximalTilesForThisColor = Mathf.Min(maxTilesPerColorSpawn, Mathf.Max(minimalTilesForThisColor,
                    safeMaxForThisColor));
            }

            int targetTilesForThisColor = Mathf.Clamp(neededToThreshold, minimalTilesForThisColor,
                maximalTilesForThisColor);

            if (remainingColors == MIN_DISTINCT_COLORS)
            {
                targetTilesForThisColor = Mathf.Clamp(remainingTiles, minimalTilesForThisColor,
                    maxTilesPerColorSpawn);
            }

            targetTilesForThisColor = Mathf.Clamp(targetTilesForThisColor, minimalTilesForThisColor, 
                maxTilesPerColorSpawn);

            remainingTiles -= targetTilesForThisColor;
            
            remainingColors--;

            colorBlocks.Add((color, targetTilesForThisColor));
        }

        int iterationsLeft = _tileConfig.MaxStackDistributionIterations;

        while (remainingTiles > 0 && iterationsLeft > MIN_TOTAL_WEIGHT)
        {
            iterationsLeft--;

            bool addedAny = false;

            for (int index = 0; index < colorBlocks.Count && remainingTiles > 0; index++)
            {
                (Color color, int count) block = colorBlocks[index];

                int canTake = _tileConfig.MaxTilesPerColorSpawn - block.count;

                if (canTake <= 0)
                    continue;

                int add = Mathf.Min(canTake, remainingTiles);

                if (add <= 0)
                    continue;

                block.count += add;
                remainingTiles -= add;
                colorBlocks[index] = block;
                addedAny = true;
            }

            if (!addedAny)
                break;
        }

        for (int index = 0; index < colorBlocks.Count; index++)
        {
            (Color color, int count) block = colorBlocks[index];

            for (int tileIndex = 0; tileIndex < block.count; tileIndex++)
                resultColors.Add(block.color);
        }

        return resultColors.Count > 0;
    }

    public bool TryBuildFinishingStack(IReadOnlyList<Color> colors, int stackSize, int maxStackThreshold,
        List<Color> resultColors)
    {
        resultColors.Clear();

        if (colors == null || colors.Count == 0)
            return false;

        if (maxStackThreshold <= 0)
            return false;

        for (int index = 0; index < colors.Count; index++)
        {
            Color color = colors[index];

            int totalCount = _colorStatistics.GetColorCount(color);

            if (totalCount <= 0)
                continue;

            int remainder = totalCount % maxStackThreshold;

            if (remainder == 0)
                continue;

            int tilesNeededToThreshold = maxStackThreshold - remainder;

            if (tilesNeededToThreshold > stackSize)
                continue;

            if (tilesNeededToThreshold > _tileConfig.MaxTilesPerColorSpawn)
                continue;

            for (int tileIndex = 0; tileIndex < tilesNeededToThreshold; tileIndex++)
                resultColors.Add(color);

            return true;
        }

        return false;
    }
}