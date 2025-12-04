using UnityEngine;

public class TileStackStackSizeCalculator
{
	private const float DEFAULT_SCALE_VALUE = 1f;

	private const float MIN_SCALE_EPSILON = 0.0001f;

	private const float HALF_TILE_HEIGHT_FACTOR = 0.5f;

	private readonly TileConfig _config;

	private readonly MeshRenderer _rootRenderer;

	public TileStackStackSizeCalculator(TileConfig config, MeshRenderer rootRenderer)
	{
		_config = config;
		_rootRenderer = rootRenderer;
	}

	public void ApplyRegularStackSize(Transform stackTransform, out float segmentStepLocal, out float segmentBaseLocalY, out float worldScaleY)
	{
		Vector3 stackScale = stackTransform.localScale;
		stackScale.x = _config.XzScaleFactor;
		stackScale.z = _config.XzScaleFactor;
		stackScale.y = _config.StackHeight;
		stackTransform.localScale = stackScale;
		float tileWorldHeight = _rootRenderer.bounds.size.y;
		if (tileWorldHeight <= 0.0001f)
		{
			tileWorldHeight = _config.StackHeight;
		}
		worldScaleY = SafeScaleComponent(stackTransform.lossyScale.y);
		segmentStepLocal = tileWorldHeight / worldScaleY;
		float yOffsetLocal = _config.YOffset / worldScaleY;
		segmentBaseLocalY = yOffsetLocal + segmentStepLocal * 0.5f;
	}

	public void ApplySpawnStackSize(Transform stackTransform, out float segmentStepLocal, out float segmentBaseLocalY, out float worldScaleY)
	{
		Transform parent = stackTransform.parent;
		Vector3 parentScale = ((parent != null) ? parent.lossyScale : Vector3.one);
		float parentX = SafeScaleComponent(parentScale.x);
		float parentY = SafeScaleComponent(parentScale.y);
		float parentZ = SafeScaleComponent(parentScale.z);
		Vector3 stackScale = stackTransform.localScale;
		stackScale.x = _config.SpawnXzWorldSize / parentX;
		stackScale.y = _config.SpawnStackWorldHeight / parentY;
		stackScale.z = _config.SpawnXzWorldSize / parentZ;
		stackTransform.localScale = stackScale;
		worldScaleY = SafeScaleComponent(stackTransform.lossyScale.y);
		float tileWorldHeight = _rootRenderer.bounds.size.y;
		if (tileWorldHeight <= 0.0001f)
		{
			tileWorldHeight = _config.SpawnStackWorldHeight;
		}
		segmentStepLocal = tileWorldHeight / worldScaleY;
		float yOffsetLocal = _config.SpawnYOffset / worldScaleY;
		segmentBaseLocalY = yOffsetLocal + segmentStepLocal * 0.5f;
	}

	private float SafeScaleComponent(float value)
	{
		return (Mathf.Abs(value) < 0.0001f) ? 1f : value;
	}
}
