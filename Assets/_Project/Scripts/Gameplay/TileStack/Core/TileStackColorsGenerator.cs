using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileStackColorsGenerator
{
    private readonly TileConfig _config;
    private readonly List<Color> _stackColorsBuffer = new();

    [Inject]
    public TileStackColorsGenerator(TileConfig config)
    {
        _config = config;
    }
    
    public IReadOnlyList<Color> BuildStackColors(int stackSize)
    {
        _stackColorsBuffer.Clear();

        IReadOnlyList<Color> paletteColors = _config.Colors;
        int paletteCount = paletteColors.Count;

        if (paletteCount == 0)
        {
            _stackColorsBuffer.Add(Color.white);
            
            return _stackColorsBuffer;
        }

        int maxBlocks = Mathf.Min(_config.MaxColorBlocksPerStack, stackSize);
        int blocksCount = Random.Range(_config.MinColorBlocksPerStack, maxBlocks + 1);

        int remainingTiles = stackSize;
        int remainingBlocks = blocksCount;

        Color previousBlockColor = Color.white;
        bool hasPreviousColor = false;

        for (int blockIndex = 0; blockIndex < blocksCount; blockIndex++)
        {
            int minTilesForBlock = _config.MinTilesPerColorBlock;
            int maxTilesForBlock = remainingTiles - (remainingBlocks - 1) * _config.MinTilesPerColorBlock;
            int blockSize = Random.Range(minTilesForBlock, maxTilesForBlock + 1);

            remainingTiles -= blockSize;
            remainingBlocks--;

            Color blockColor = GetRandomPaletteColor(paletteColors, previousBlockColor, hasPreviousColor);

            previousBlockColor = blockColor;
            hasPreviousColor = true;

            for (int i = 0; i < blockSize; i++)
                _stackColorsBuffer.Add(blockColor);
        }

        return _stackColorsBuffer;
    }

    private Color GetRandomPaletteColor(IReadOnlyList<Color> paletteColors, Color previousColor, bool hasPreviousColor)
    {
        int paletteCount = paletteColors.Count;

        if (!hasPreviousColor || paletteCount <= 1)
            return paletteColors[Random.Range(0, paletteCount)];

        Color result = previousColor;

        for (int attempt = 0; attempt < _config.MaxColorPickAttempts; attempt++)
        {
            Color candidate = paletteColors[Random.Range(0, paletteCount)];
            
            if (candidate != previousColor)
            {
                result = candidate;
                
                break;
            }
        }

        return result;
    }
}