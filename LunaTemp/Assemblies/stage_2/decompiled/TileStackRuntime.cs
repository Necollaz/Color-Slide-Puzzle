using System.Collections.Generic;
using UnityEngine;

public class TileStackRuntime
{
	private readonly TileStackSegmentsModel _model;

	private readonly TileStackSegmentsVisual _visual;

	private readonly Transform _stackTransform;

	public bool IsEmpty => _model.IsEmpty;

	public int TilesCount => _model.Count;

	public TileStackRuntime(TileConfig config, MeshRenderer renderer, Transform stackTransform)
	{
		_model = new TileStackSegmentsModel(config);
		_visual = new TileStackSegmentsVisual(config, renderer);
		_stackTransform = stackTransform;
	}

	public int CountTopTilesOfColor(Color color)
	{
		return _model.CountTopTilesOfColor(color);
	}

	public int RemoveTopTiles(int count)
	{
		if (count <= 0)
		{
			return 0;
		}
		int removed = _model.RemoveTopTiles(count);
		if (removed > 0)
		{
			ApplyModelToView();
		}
		return removed;
	}

	public int RemoveTilesOfColorFromBottom(Color color, int count)
	{
		if (count <= 0)
		{
			return 0;
		}
		int removed = _model.RemoveColorFromBottom(color, count);
		if (removed > 0)
		{
			ApplyModelToView();
		}
		return removed;
	}

	public bool TryGetBottomColorIndex(Color color, out int indexFromBottom)
	{
		return _model.TryGetBottomColorIndex(color, out indexFromBottom);
	}

	public bool TryGetTopColor(out Color color)
	{
		return _model.TryGetTopColor(out color);
	}

	public bool TryGetCompletedColor(out Color color, out int completedCount)
	{
		return _model.TryGetCompletedColor(out color, out completedCount);
	}

	public void Initialize(IReadOnlyList<Color> segmentColors)
	{
		_model.Initialize(segmentColors);
		ApplyModelToView(true);
	}

	public void AddTilesOnTop(Color color, int count)
	{
		if (count > 0)
		{
			_model.AddTilesOnTop(color, count);
			ApplyModelToView();
		}
	}

	public void ForceTopColor(Color color)
	{
		_model.ForceTopColor(color);
		ApplyModelToView();
	}

	public void AppendColorsToDictionary(Dictionary<Color, int> colorCounts)
	{
		_model.AppendColorsToDictionary(colorCounts);
	}

	private void ApplyModelToView(bool fullRebuildStackSize = false)
	{
		if (_stackTransform == null)
		{
			return;
		}
		if (_model.IsEmpty)
		{
			_stackTransform.gameObject.SetActive(false);
			return;
		}
		_stackTransform.gameObject.SetActive(true);
		if (fullRebuildStackSize)
		{
			_visual.ApplyStackSize(_stackTransform);
		}
		_visual.RebuildVisualStack(_model.SegmentColors, _stackTransform);
	}
}
