using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileStackColorsGenerator
{
    private const int MIN_TILES_PER_COLOR_DEFAULT = 3;
    
    private readonly TileConfig _config;
    private readonly TileStackColorBlocksBuilder _colorBlocksBuilder;
    
    private readonly List<Color> _stackColorsBuffer = new();
    private readonly List<Color> _distinctColorsBuffer = new();

    [Inject]
    public TileStackColorsGenerator(TileConfig config, TileStackColorBlocksBuilder colorBlocksBuilder)
    {
        _config = config;
        _colorBlocksBuilder = colorBlocksBuilder;
    }
    
    public IReadOnlyList<Color> BuildFieldStackColors(int stackSize)
    {
        _stackColorsBuffer.Clear();

        IReadOnlyList<Color> paletteColors = _config.Colors;
        int paletteCount = paletteColors.Count;

        if (paletteCount == 0)
        {
            _stackColorsBuffer.Add(Color.white);
            
            return _stackColorsBuffer;
        }

        int minimumTilesPerColor = Mathf.Max(MIN_TILES_PER_COLOR_DEFAULT, _config.MinTilesPerColorBlock);
        int minGeneratedSize = Mathf.Max(minimumTilesPerColor, _config.MinGeneratedStackSize);
        int maxGeneratedSize = Mathf.Max(minGeneratedSize, _config.MaxGeneratedStackSize);

        int safeStackSize = Mathf.Clamp(stackSize, minGeneratedSize, maxGeneratedSize);

        if (safeStackSize < minimumTilesPerColor)
            safeStackSize = minimumTilesPerColor;

        int maxDistinctColors = Mathf.Min(_config.MaxColorBlocksPerStack, paletteCount);

        if (maxDistinctColors <= 0)
        {
            _stackColorsBuffer.Add(paletteColors[0]);
            
            return _stackColorsBuffer;
        }

        int maxColorsBySize = Mathf.Max(1, safeStackSize / minimumTilesPerColor);
        maxDistinctColors = Mathf.Min(maxDistinctColors, maxColorsBySize);

        int distinctColorsCount = _colorBlocksBuilder.ChooseDistinctColorsCount(maxDistinctColors, safeStackSize,
            minimumTilesPerColor);

        distinctColorsCount = Mathf.Clamp(distinctColorsCount, 1, maxDistinctColors);

        int minColorsPerStack = Mathf.Clamp(_config.MinColorBlocksPerStack, 1, maxDistinctColors);
        distinctColorsCount = Mathf.Clamp(distinctColorsCount, minColorsPerStack, maxDistinctColors);

        _distinctColorsBuffer.Clear();

        int maxPickAttempts = Mathf.Max(1, _config.MaxColorPickAttempts);
        int attemptsLeft = maxPickAttempts;
        
        while (_distinctColorsBuffer.Count < distinctColorsCount && attemptsLeft > 0)
        {
            attemptsLeft--;

            Color candidate = paletteColors[Random.Range(0, paletteCount)];

            if (!_distinctColorsBuffer.Contains(candidate))
                _distinctColorsBuffer.Add(candidate);
        }
        
        if (_distinctColorsBuffer.Count < distinctColorsCount)
        {
            for (int i = 0; i < paletteCount && _distinctColorsBuffer.Count < distinctColorsCount; i++)
            {
                Color candidate = paletteColors[i];

                if (_distinctColorsBuffer.Contains(candidate))
                    continue;

                _distinctColorsBuffer.Add(candidate);
            }
        }

        _colorBlocksBuilder.BuildStackFromDistinctColors(_distinctColorsBuffer, safeStackSize, minimumTilesPerColor,
            _stackColorsBuffer);

        return _stackColorsBuffer;
    }

    public IReadOnlyList<Color> BuildMonocolorStack(int stackSize, Color color)
    {
        _stackColorsBuffer.Clear();

        int minimumTilesPerColor = Mathf.Max(MIN_TILES_PER_COLOR_DEFAULT, _config.MinTilesPerColorBlock);
        int safeSize = Mathf.Max(minimumTilesPerColor, stackSize);

        for (int i = 0; i < safeSize; i++)
            _stackColorsBuffer.Add(color);

        return _stackColorsBuffer;
    }
}