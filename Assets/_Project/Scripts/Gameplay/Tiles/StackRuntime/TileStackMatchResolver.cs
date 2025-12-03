using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class TileStackMatchResolver
{
    private readonly GridCellsBuilder _cellsBuilder;
    private readonly TileStackMergeAnimator _mergeAnimator;
    private readonly TileStackInCellFinder _stackFinder;
    private readonly TileMatchGroupFinder _groupFinder;
    private readonly TileGroupTransferPlanner _transferPlanner;

    [Inject]
    public TileStackMatchResolver(GridCellsBuilder cellsBuilder, TileStackMergeAnimator mergeAnimator,
        TileStackInCellFinder stackFinder, TileMatchGroupFinder groupFinder, TileGroupTransferPlanner transferPlanner)
    {
        _cellsBuilder = cellsBuilder;
        _mergeAnimator = mergeAnimator;
        _stackFinder = stackFinder;
        _groupFinder = groupFinder;
        _transferPlanner = transferPlanner;
    }
    
    public bool IsResolvingMatches { get; private set; }

    public void ResolveMatchesFromCell()
    {
        if (IsResolvingMatches)
            return;

        IsResolvingMatches = true;

        bool hasAnimations = TryResolveGlobalWave();
        
        if (!hasAnimations)
            IsResolvingMatches = false;
    }

    private bool TryResolveGlobalWave()
    {
        IReadOnlyDictionary<Vector2Int, HexCellView> cells = _cellsBuilder.Cells;

        if (cells == null || cells.Count == 0)
            return false;

        _groupFinder.FindTopColorGroups();
        
        IReadOnlyList<List<HexCellView>> groups = _groupFinder.FoundGroups;

        if (groups.Count == 0)
        {
            bool hasClearAnimations = TryPlayCompletedStacksClearAnimation(cells);
            
            return hasClearAnimations;
        }

        Sequence fullSequence = DOTween.Sequence();
        bool hasAnyAnimations = false;

        for (int groupIndex = 0; groupIndex < groups.Count; groupIndex++)
        {
            List<HexCellView> group = groups[groupIndex];

            if (!TryGetGroupTopColor(group, out Color groupColor))
                continue;

            Sequence groupSequence = DOTween.Sequence();
            bool groupHasTransfers = false;

            foreach ((TileStackView sourceStack, TileStackView targetStack, int chainStepIndex) 
                     in _transferPlanner.BuildTransferSteps(group, groupColor))
            {
                int movableCount = sourceStack.CountTopTilesOfColor(groupColor);

                if (movableCount <= 0)
                    continue;

                groupHasTransfers = true;

                if (_mergeAnimator != null)
                {
                    TileStackView capturedSource = sourceStack;
                    TileStackView capturedTarget = targetStack;
                    Color capturedColor = groupColor;

                    Sequence mergeSeq = _mergeAnimator.BuildMergeSequence(capturedSource, capturedTarget, capturedColor,
                        movableCount, chainStepIndex, () =>
                        {
                            capturedSource.RemoveTopTiles(1);
                            capturedTarget.AddTilesOnTop(capturedColor, 1);
                        });

                    if (mergeSeq != null)
                    {
                        groupSequence.Append(mergeSeq);
                        
                        hasAnyAnimations = true;
                    }
                }
                else
                {
                    sourceStack.RemoveTopTiles(movableCount);
                    targetStack.AddTilesOnTop(groupColor, movableCount);
                }
            }

            if (groupHasTransfers && _mergeAnimator != null)
                fullSequence.Append(groupSequence);
        }

        _groupFinder.ReleaseGroups();

        if (hasAnyAnimations)
        {
            fullSequence.OnComplete(() =>
            {
                bool hasMore = TryResolveGlobalWave();

                if (!hasMore)
                    IsResolvingMatches = false;
            });

            fullSequence.Play();
        }

        return hasAnyAnimations;
    }

    private bool TryPlayCompletedStacksClearAnimation(IReadOnlyDictionary<Vector2Int, HexCellView> cells)
    {
        if (_mergeAnimator == null || cells == null || cells.Count == 0)
            return false;

        Sequence fullSequence = DOTween.Sequence();
        bool hasAny = false;

        foreach (KeyValuePair<Vector2Int, HexCellView> pair in cells)
        {
            HexCellView cell = pair.Value;

            if (cell == null)
                continue;

            TileStackView stack = _stackFinder.FindActiveStackInCell(cell);

            if (stack == null)
                continue;

            if (!stack.TryGetCompletedColor(out Color color, out int completedCount))
                continue;

            if (completedCount <= 0)
                continue;

            Sequence clearSeq = _mergeAnimator.BuildClearCompletedStackSequence(stack, color, completedCount);

            if (clearSeq == null)
                continue;

            fullSequence.Join(clearSeq);
            hasAny = true;
        }

        if (!hasAny)
            return false;

        fullSequence.OnComplete(() =>
        {
            bool hasMore = TryResolveGlobalWave();
            
            if (!hasMore)
                IsResolvingMatches = false;
        });
        
        fullSequence.Play();

        return true;
    }

    private bool TryGetGroupTopColor(List<HexCellView> group, out Color color)
    {
        color = default;

        if (group == null || group.Count == 0)
            return false;

        for (int i = 0; i < group.Count; i++)
        {
            HexCellView cell = group[i];

            TileStackView stack = _stackFinder.FindActiveStackInCell(cell);

            if (stack == null)
                continue;

            if (stack.TryGetTopColor(out color))
                return true;
        }

        return false;
    }
}