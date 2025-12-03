using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileSpawnTopColorSelector
{
    private const int MATCH_POWER_WEIGHT = 10;
    private const int MIN_MATCH_SCORE = 0;

    private readonly Grid _grid;
    private readonly GridCellsBuilder _cellsBuilder;
    private readonly TileConfig _tileConfig;
    private readonly TileColorStatistics _colorStatistics;
    private readonly GridNeighborOffsetProvider _gridNeighborOffset;

    private readonly List<Color> _paletteBuffer = new List<Color>();

    [Inject]
    public TileSpawnTopColorSelector(Grid grid, GridCellsBuilder cellsBuilder, TileConfig tileConfig,
        TileColorStatistics colorStatistics, GridNeighborOffsetProvider gridNeighborOffset)
    {
        _grid = grid;
        _cellsBuilder = cellsBuilder;
        _tileConfig = tileConfig;
        _colorStatistics = colorStatistics;
        _gridNeighborOffset = gridNeighborOffset;
    }

    public bool TryGetHelpfulSpawnTopColor(out Color topColor)
    {
        topColor = default;

        IReadOnlyDictionary<Vector2Int, HexCellView> cells = _cellsBuilder.Cells;

        if (cells == null || cells.Count == 0)
            return false;

        IReadOnlyList<Color> palette = _tileConfig.Colors;

        if (palette == null || palette.Count == 0)
            return false;

        _colorStatistics.Rebuild();

        Vector2Int[] neighborOffsets = _gridNeighborOffset.GetNeighborOffsets(_grid);
        int maxStackThreshold = _tileConfig.MaxStackSize;

        bool foundBest = false;
        Color bestColor = default;
        int bestScore = int.MinValue;
        int bestRemainder = int.MaxValue;

        foreach (KeyValuePair<Vector2Int, HexCellView> pair in cells)
        {
            Vector2Int coordinates = pair.Key;
            HexCellView cellView = pair.Value;

            if (cellView == null)
                continue;

            TileStackView stackView = cellView.GetComponentInChildren<TileStackView>();
            bool hasActiveStack = stackView != null && stackView.gameObject.activeSelf && !stackView.IsEmpty;

            if (hasActiveStack)
                continue;

            for (int index = 0; index < neighborOffsets.Length; index++)
            {
                Vector2Int neighborCoordinates = coordinates + neighborOffsets[index];

                if (!cells.TryGetValue(neighborCoordinates, out HexCellView neighborCell))
                    continue;

                if (neighborCell == null)
                    continue;

                TileStackView neighborStack = neighborCell.GetComponentInChildren<TileStackView>();

                if (neighborStack == null || neighborStack.IsEmpty || !neighborStack.gameObject.activeSelf)
                    continue;

                if (!neighborStack.TryGetTopColor(out Color neighborTopColor))
                    continue;

                int matchPower = CountSameTopNeighbors(coordinates, neighborTopColor, cells, neighborOffsets);

                if (matchPower <= MIN_MATCH_SCORE)
                    continue;

                int totalCount = _colorStatistics.GetColorCount(neighborTopColor);
                int remainder = maxStackThreshold > 0 ? totalCount % maxStackThreshold : 0;
                int score = matchPower * MATCH_POWER_WEIGHT - remainder;

                bool isBetterScore = score > bestScore;
                bool isEqualScoreButLessRemainder = score == bestScore && remainder < bestRemainder;

                if (!foundBest || isBetterScore || isEqualScoreButLessRemainder)
                {
                    foundBest = true;
                    bestScore = score;
                    bestRemainder = remainder;
                    bestColor = neighborTopColor;
                }
            }
        }

        if (foundBest && bestScore > MIN_MATCH_SCORE)
        {
            topColor = bestColor;
            
            return true;
        }

        if (TryPickColorByRemainder(out Color fallbackColor))
        {
            topColor = fallbackColor;
            
            return true;
        }

        return false;
    }

    private int CountSameTopNeighbors(Vector2Int cellCoordinates, Color topColor, 
        IReadOnlyDictionary<Vector2Int, HexCellView> cells, Vector2Int[] neighborOffsets)
    {
        int count = 0;

        for (int index = 0; index < neighborOffsets.Length; index++)
        {
            Vector2Int neighborCoordinates = cellCoordinates + neighborOffsets[index];

            if (!cells.TryGetValue(neighborCoordinates, out HexCellView neighborCell))
                continue;

            if (neighborCell == null)
                continue;

            TileStackView neighborStack = neighborCell.GetComponentInChildren<TileStackView>();

            if (neighborStack == null || neighborStack.IsEmpty)
                continue;

            if (!neighborStack.TryGetTopColor(out Color neighborTopColor))
                continue;

            if (neighborTopColor == topColor)
                count++;
        }

        return count;
    }

    private bool TryPickColorByRemainder(out Color color)
    {
        color = default;

        IReadOnlyList<Color> palette = _tileConfig.Colors;

        if (palette == null || palette.Count == 0)
            return false;

        _paletteBuffer.Clear();
        _paletteBuffer.AddRange(palette);

        if (_paletteBuffer.Count == 0)
            return false;

        int maxStackThreshold = _tileConfig.MaxStackSize;

        bool foundAny = false;
        Color bestColor = default;
        int bestRemainder = int.MaxValue;

        for (int index = 0; index < _paletteBuffer.Count; index++)
        {
            Color candidate = _paletteBuffer[index];

            int totalCount = _colorStatistics.GetColorCount(candidate);
            int remainder = maxStackThreshold > 0 ? totalCount % maxStackThreshold : 0;

            if (!foundAny || remainder < bestRemainder)
            {
                foundAny = true;
                bestRemainder = remainder;
                bestColor = candidate;
            }
        }

        if (!foundAny)
            return false;

        color = bestColor;
        
        return true;
    }
}