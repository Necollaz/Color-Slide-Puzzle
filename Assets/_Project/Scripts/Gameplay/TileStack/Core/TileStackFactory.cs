using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileStackFactory
{
    private readonly TileConfig _config;
    private readonly TileStackView _stackPrefab;
    private readonly TileStackColorsGenerator _colorsGenerator;
    private readonly DiContainer _container;

    [Inject]
    public TileStackFactory(TileStackView stackPrefab, DiContainer container, TileStackColorsGenerator colorsGenerator,
        TileConfig config)
    {
        _stackPrefab = stackPrefab;
        _container = container;
        _colorsGenerator = colorsGenerator;
        _config = config;
    }

    public void CreateOrReuseRandomStackInCell(HexCellView cellView)
    {
        if (cellView == null)
            return;

        TileStackView stackView = cellView.GetComponentInChildren<TileStackView>(true);

        if (stackView == null)
            stackView = _container.InstantiatePrefabForComponent<TileStackView>(_stackPrefab, cellView.transform);

        stackView.gameObject.SetActive(true);
        stackView.transform.localPosition = Vector3.zero;

        int minSize = Mathf.Max(1, _config.MinGeneratedStackSize);
        int maxSize = Mathf.Min(_config.MaxGeneratedStackSize, _config.MaxStackSize - 1);
        int randomStackSizeInclusiveMax = maxSize + 1;

        int stackSize = Random.Range(minSize, randomStackSizeInclusiveMax);
        stackSize = Mathf.Clamp(stackSize, 1, _config.MaxStackSize - 1);

        IReadOnlyList<Color> segmentColors = _colorsGenerator.BuildStackColors(stackSize);
        stackView.Initialize(segmentColors);
    }

    public bool HasActiveStack(HexCellView cellView)
    {
        if (cellView == null)
            return false;

        TileStackView existingActiveStack = cellView.GetComponentInChildren<TileStackView>();
        
        return existingActiveStack != null;
    }
}