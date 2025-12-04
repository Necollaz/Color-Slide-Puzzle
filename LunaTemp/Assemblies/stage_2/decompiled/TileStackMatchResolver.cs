using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TileStackMatchResolver
{
	private readonly GridCellsBuilder _cellsBuilder;

	private readonly TileStackMergeAnimator _mergeAnimator;

	private readonly TileStackInCellFinder _stackFinder;

	private readonly TileMatchGroupFinder _groupFinder;

	private readonly TileGroupTransferPlanner _transferPlanner;

	public bool IsResolvingMatches { get; private set; }

	public TileStackMatchResolver(GridCellsBuilder cellsBuilder, TileStackMergeAnimator mergeAnimator, TileStackInCellFinder stackFinder, TileMatchGroupFinder groupFinder, TileGroupTransferPlanner transferPlanner)
	{
		_cellsBuilder = cellsBuilder;
		_mergeAnimator = mergeAnimator;
		_stackFinder = stackFinder;
		_groupFinder = groupFinder;
		_transferPlanner = transferPlanner;
	}

	public void ResolveMatchesFromCell()
	{
		if (!IsResolvingMatches)
		{
			IsResolvingMatches = true;
			if (!TryResolveGlobalWave())
			{
				IsResolvingMatches = false;
			}
		}
	}

	private bool TryResolveGlobalWave()
	{
		IReadOnlyDictionary<Vector2Int, HexCellView> cells = _cellsBuilder.Cells;
		if (cells == null || cells.Count == 0)
		{
			return false;
		}
		_groupFinder.FindTopColorGroups();
		IReadOnlyList<List<HexCellView>> groups = _groupFinder.FoundGroups;
		if (groups.Count == 0)
		{
			return TryPlayCompletedStacksClearAnimation(cells);
		}
		Sequence fullSequence = DOTween.Sequence();
		bool hasAnyAnimations = false;
		for (int groupIndex = 0; groupIndex < groups.Count; groupIndex++)
		{
			List<HexCellView> group = groups[groupIndex];
			if (!TryGetGroupTopColor(group, out var groupColor))
			{
				continue;
			}
			Sequence groupSequence = DOTween.Sequence();
			bool groupHasTransfers = false;
			foreach (TileTransferStep step in _transferPlanner.BuildTransferSteps(group, groupColor))
			{
				TileStackView sourceStack = step.Source;
				TileStackView targetStack = step.Target;
				int chainStepIndex = step.ChainStepIndex;
				int movableCount = sourceStack.CountTopTilesOfColor(groupColor);
				if (movableCount <= 0)
				{
					continue;
				}
				groupHasTransfers = true;
				if (_mergeAnimator != null)
				{
					TileStackView capturedSource = sourceStack;
					TileStackView capturedTarget = targetStack;
					Color capturedColor = groupColor;
					Sequence mergeSeq = _mergeAnimator.BuildMergeSequence(capturedSource, capturedTarget, capturedColor, movableCount, chainStepIndex, delegate
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
			{
				fullSequence.Append(groupSequence);
			}
		}
		_groupFinder.ReleaseGroups();
		if (hasAnyAnimations)
		{
			fullSequence.OnComplete(delegate
			{
				if (!TryResolveGlobalWave())
				{
					IsResolvingMatches = false;
				}
			});
			fullSequence.Play();
		}
		return hasAnyAnimations;
	}

	private bool TryPlayCompletedStacksClearAnimation(IReadOnlyDictionary<Vector2Int, HexCellView> cells)
	{
		if (_mergeAnimator == null || cells == null || cells.Count == 0)
		{
			return false;
		}
		Sequence fullSequence = DOTween.Sequence();
		bool hasAny = false;
		foreach (KeyValuePair<Vector2Int, HexCellView> cell2 in cells)
		{
			HexCellView cell = cell2.Value;
			if (cell == null)
			{
				continue;
			}
			TileStackView stack = _stackFinder.FindActiveStackInCell(cell);
			if (!(stack == null) && stack.TryGetCompletedColor(out var color, out var completedCount) && completedCount > 0)
			{
				Sequence clearSeq = _mergeAnimator.BuildClearCompletedStackSequence(stack, color, completedCount);
				if (clearSeq != null)
				{
					fullSequence.Join(clearSeq);
					hasAny = true;
				}
			}
		}
		if (!hasAny)
		{
			return false;
		}
		fullSequence.OnComplete(delegate
		{
			if (!TryResolveGlobalWave())
			{
				IsResolvingMatches = false;
			}
		});
		fullSequence.Play();
		return true;
	}

	private bool TryGetGroupTopColor(List<HexCellView> group, out Color color)
	{
		color = default(Color);
		if (group == null || group.Count == 0)
		{
			return false;
		}
		for (int i = 0; i < group.Count; i++)
		{
			HexCellView cell = group[i];
			TileStackView stack = _stackFinder.FindActiveStackInCell(cell);
			if (!(stack == null) && stack.TryGetTopColor(out color))
			{
				return true;
			}
		}
		return false;
	}
}
