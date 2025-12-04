using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileSpawnMatchPlanner
{
    private const int MIN_STACK_SIZE = 1;

    private readonly TileConfig _tileConfig;
    private readonly GridCellsBuilder _cellsBuilder;
    private readonly TileColorStatistics _colorStatistics;
    private readonly TileSpawnUsefulTopColorsCollector _usefulTopColorsCollector;
    private readonly TileColorRemainderSorter _colorRemainderSorter;
    private readonly TileSpawnStackFromColorsBuilder _stackFromColorsBuilder;

    private readonly HashSet<Color> _usefulTopColors = new HashSet<Color>();
    private readonly List<Color> _paletteBuffer = new List<Color>();
    private readonly List<Color> _usefulTopColorsList = new List<Color>();

    [Inject]
    public TileSpawnMatchPlanner(TileConfig tileConfig, GridCellsBuilder cellsBuilder, TileColorStatistics colorStatistics,
        TileSpawnUsefulTopColorsCollector usefulTopColorsCollector, TileColorRemainderSorter colorRemainderSorter,
        TileSpawnStackFromColorsBuilder stackFromColorsBuilder)
    {
        _tileConfig = tileConfig;
        _cellsBuilder = cellsBuilder;
        _colorStatistics = colorStatistics;
        _usefulTopColorsCollector = usefulTopColorsCollector;
        _colorRemainderSorter = colorRemainderSorter;
        _stackFromColorsBuilder = stackFromColorsBuilder;
    }

    public bool TryBuildSpawnStackColors(int stackSize, List<Color> resultColors)
    {
        resultColors.Clear();

        if (stackSize <= 0)
            return false;

        IReadOnlyDictionary<Vector2Int, HexCellView> cells = _cellsBuilder.Cells;

        if (cells == null || cells.Count == 0)
            return false;

        IReadOnlyList<Color> palette = _tileConfig.Colors;

        if (palette == null || palette.Count == 0)
            return false;

        _colorStatistics.Rebuild();

        int maxStackThreshold = _tileConfig.MaxStackSize;
        int minimumTilesPerColor = Mathf.Max(_tileConfig.MinTilesPerColorSpawn, _tileConfig.MinTilesPerColorBlock);

        if (stackSize < MIN_STACK_SIZE)
            stackSize = MIN_STACK_SIZE;
        
        _usefulTopColorsCollector.CollectUsefulTopColors(_usefulTopColors);

        _usefulTopColorsList.Clear();
        _usefulTopColorsList.AddRange(_usefulTopColors);

        if (_usefulTopColorsList.Count > 0)
        {
            _colorRemainderSorter.SortByRemainder(_usefulTopColorsList, maxStackThreshold);
            
            if (_stackFromColorsBuilder.TryBuildFinishingStack(_usefulTopColorsList, stackSize, maxStackThreshold,
                    resultColors))
            {
                return true;
            }
            
            if (_stackFromColorsBuilder.TryBuildHelpfulSpawnStackFromColors(_usefulTopColorsList, stackSize,
                    minimumTilesPerColor, maxStackThreshold, resultColors))
            {
                return resultColors.Count > 0;
            }
        }
        
        _paletteBuffer.Clear();
        _paletteBuffer.AddRange(palette);

        if (_paletteBuffer.Count == 0)
            return false;

        _colorRemainderSorter.SortByRemainder(_paletteBuffer, maxStackThreshold);

        if (_stackFromColorsBuilder.TryBuildFinishingStack(_paletteBuffer, stackSize, maxStackThreshold, resultColors))
        {
            return true;
        }

        if (_stackFromColorsBuilder.TryBuildHelpfulSpawnStackFromColors(_paletteBuffer, stackSize, minimumTilesPerColor,
                maxStackThreshold, resultColors))
        {
            return resultColors.Count > 0;
        }

        return false;
    }
}