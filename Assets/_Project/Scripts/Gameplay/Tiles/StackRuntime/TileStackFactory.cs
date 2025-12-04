using System.Collections.Generic;
using UnityEngine;

public class TileStackFactory
{
    private const float MIN_SCALE_EPSILON = 0.0001f;
    private const float DEFAULT_SCALE_VALUE = 1.0f;
    
    private readonly TileConfig _config;
    private readonly TileStackView _cellStackPrefab;
    private readonly TileStackRoot _spawnStackRootPrefab;
    private readonly TileStackColorsGenerator _colorsGenerator;
    private readonly TileSpawnMatchPlanner _spawnMatchPlanner;
    private readonly TileSpawnTopColorSelector _spawnTopColorSelector;
    private readonly TileColorStatistics _colorStatistics;
    private readonly GridCellsBuilder _cellsBuilder;

    private readonly List<TileStackRoot> _spawnStackPool = new List<TileStackRoot>();
    private readonly List<Color> _spawnStackColorsBuffer = new List<Color>();
    
    private Vector3 _gridWorldScale;

    private bool _isGridScaleCached;
    
    public TileStackFactory(TileStackView cellStackPrefab, TileStackRoot spawnStackRootPrefab, 
        TileStackColorsGenerator colorsGenerator, TileConfig config, GridCellsBuilder cellsBuilder,
        TileSpawnMatchPlanner spawnMatchPlanner, TileSpawnTopColorSelector spawnTopColorSelector,
        TileColorStatistics colorStatistics)
    {
        _cellStackPrefab = cellStackPrefab;
        _spawnStackRootPrefab = spawnStackRootPrefab;
        _colorsGenerator = colorsGenerator;
        _config = config;
        _cellsBuilder = cellsBuilder;
        _spawnMatchPlanner = spawnMatchPlanner;
        _spawnTopColorSelector = spawnTopColorSelector;
        _colorStatistics = colorStatistics;
    }

    public void CreateOrReuseRandomStackInCell(HexCellView cellView)
    {
        if (cellView == null)
            return;

        TileStackView stackView = cellView.GetComponentInChildren<TileStackView>(true);

        if (stackView == null)
            stackView = Object.Instantiate(_cellStackPrefab, cellView.transform);

        stackView.gameObject.SetActive(true);
        stackView.transform.localPosition = Vector3.zero;

        InitializeFieldStack(stackView);
    }

    public bool HasActiveStack(HexCellView cellView)
    {
        if (cellView == null)
            return false;
        
        TileStackView existingStack = cellView.GetComponentInChildren<TileStackView>(true);

        if (existingStack == null)
            return false;

        bool isActive = existingStack.gameObject.activeInHierarchy;
        bool isEmpty = existingStack.IsEmpty;
        
        return isActive && !isEmpty;
    }
    
    public TileStackRoot CreateOrReuseSpawnStack(Transform parentTransform)
    {
        if (parentTransform == null)
            return null;

        TileStackRoot stackRoot = GetOrCreateSpawnRootFromPool();
        stackRoot.transform.SetParent(parentTransform, worldPositionStays: false);
        stackRoot.transform.localPosition = Vector3.zero;
        
        TryMatchRootScaleWithGrid(stackRoot.transform);
        
        stackRoot.gameObject.SetActive(true);
        
        InitializeSpawnStack(stackRoot.TileStackView);

        return stackRoot;
    }

    private TileStackRoot GetOrCreateSpawnRootFromPool()
    {
        for (int i = 0; i < _spawnStackPool.Count; i++)
        {
            TileStackRoot root = _spawnStackPool[i];

            if (root != null && !root.gameObject.activeSelf)
                return root;
        }

        TileStackRoot newRoot = Object.Instantiate(_spawnStackRootPrefab);
        _spawnStackPool.Add(newRoot);

        return newRoot;
    }

    private void InitializeFieldStack(TileStackView stackView)
    {
        if (stackView == null)
            return;

        int minimumTilesPerColor = Mathf.Max(_config.MinTilesPerColorSpawn, _config.MinTilesPerColorBlock);
        int minimalStackSize = Mathf.Max(minimumTilesPerColor, _config.MinGeneratedStackSize);
        int maximalStackSize = Mathf.Min(_config.MaxGeneratedStackSize, _config.MaxStackSize - 1);

        int randomStackSizeInclusiveMax = maximalStackSize + 1;
        int generatedStackSize = Random.Range(minimalStackSize, randomStackSizeInclusiveMax);
        int safeStackSize = Mathf.Clamp(generatedStackSize, minimalStackSize, _config.MaxStackSize - 1);

        IReadOnlyList<Color> segmentColors = _colorsGenerator.BuildFieldStackColors(safeStackSize);

        stackView.Initialize(segmentColors);
    }
    
    private void InitializeSpawnStack(TileStackView stackView)
    {
        if (stackView == null)
            return;

        int minimumTilesPerColor = Mathf.Max(_config.MinTilesPerColorSpawn, _config.MinTilesPerColorBlock);
        int minimalStackSize = Mathf.Max(minimumTilesPerColor, _config.MinGeneratedStackSize);
        int maximalStackSize = Mathf.Min(_config.MaxGeneratedStackSize, _config.MaxStackSize - 1);

        int randomStackSizeInclusiveMax = maximalStackSize + 1;
        int generatedStackSize = Random.Range(minimalStackSize, randomStackSizeInclusiveMax);
        int safeStackSize = Mathf.Clamp(generatedStackSize, minimalStackSize, _config.MaxStackSize - 1);

        IReadOnlyList<Color> segmentColors;

        _spawnStackColorsBuffer.Clear();

        bool isPlannedSuccessfully = _spawnMatchPlanner != null &&
                                     _spawnMatchPlanner.TryBuildSpawnStackColors(safeStackSize, _spawnStackColorsBuffer);

        if (isPlannedSuccessfully && _spawnStackColorsBuffer.Count > 0)
        {
            segmentColors = _spawnStackColorsBuffer;
        }
        else
        {
            if (_spawnMatchPlanner != null && _spawnTopColorSelector.TryGetHelpfulSpawnTopColor(out Color topColor))
            {
                _colorStatistics.Rebuild();

                int maxStackThreshold = _config.MaxStackSize;
                int currentColorCount = _colorStatistics.GetColorCount(topColor);
                int remainder = maxStackThreshold > 0 ? currentColorCount % maxStackThreshold : 0;

                if (remainder == 0 && currentColorCount > 0)
                    remainder = maxStackThreshold;

                int maxAdditionalTilesWithoutOverflow = maxStackThreshold - remainder;

                if (maxAdditionalTilesWithoutOverflow < minimumTilesPerColor)
                {
                    segmentColors = _colorsGenerator.BuildFieldStackColors(safeStackSize);
                }
                else
                {
                    int safeMonocolorSize = Mathf.Clamp(safeStackSize, minimumTilesPerColor, 
                        maxAdditionalTilesWithoutOverflow);

                    segmentColors = _colorsGenerator.BuildMonocolorStack(safeMonocolorSize, topColor);
                }
            }
            else
            {
                segmentColors = _colorsGenerator.BuildFieldStackColors(safeStackSize);
            }
        }

        stackView.Initialize(segmentColors);
    }
    
    private void CacheGridWorldScaleIfNeeded()
    {
        if (_isGridScaleCached)
            return;

        IReadOnlyDictionary<Vector2Int, HexCellView> cells = _cellsBuilder.Cells;

        foreach (HexCellView cellView in cells.Values)
        {
            if (cellView == null)
                continue;

            _gridWorldScale = cellView.transform.lossyScale;
            _isGridScaleCached = true;
            
            break;
        }
    }
    
    private void TryMatchRootScaleWithGrid(Transform rootTransform)
    {
        if (rootTransform == null)
            return;

        CacheGridWorldScaleIfNeeded();

        if (!_isGridScaleCached)
            return;

        Transform parentTransform = rootTransform.parent;
        Vector3 parentWorldScale = parentTransform != null ? parentTransform.lossyScale : Vector3.one;

        float parentX = Mathf.Abs(parentWorldScale.x) < MIN_SCALE_EPSILON ? DEFAULT_SCALE_VALUE : parentWorldScale.x;
        float parentY = Mathf.Abs(parentWorldScale.y) < MIN_SCALE_EPSILON ? DEFAULT_SCALE_VALUE : parentWorldScale.y;
        float parentZ = Mathf.Abs(parentWorldScale.z) < MIN_SCALE_EPSILON ? DEFAULT_SCALE_VALUE : parentWorldScale.z;

        Vector3 localScale = rootTransform.localScale;
        localScale.x = _gridWorldScale.x / parentX;
        localScale.y = _gridWorldScale.y / parentY;
        localScale.z = _gridWorldScale.z / parentZ;

        rootTransform.localScale = localScale;
    }
}