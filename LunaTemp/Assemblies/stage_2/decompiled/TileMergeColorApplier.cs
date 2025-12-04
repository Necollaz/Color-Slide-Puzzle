using UnityEngine;

public class TileMergeColorApplier
{
	private readonly TileShaderPropertyIds _shaderPropertyIds;

	private readonly MaterialPropertyBlock _propertyBlock = new MaterialPropertyBlock();

	public TileMergeColorApplier(TileShaderPropertyIds shaderPropertyIds)
	{
		_shaderPropertyIds = shaderPropertyIds;
	}

	public void ApplyColorToRenderer(MeshRenderer renderer, Color color)
	{
		if (renderer == null)
		{
			return;
		}
		renderer.GetPropertyBlock(_propertyBlock);
		Material material = renderer.sharedMaterial;
		if (material != null)
		{
			if (material.HasProperty(_shaderPropertyIds.BaseColorId))
			{
				_propertyBlock.SetColor(_shaderPropertyIds.BaseColorId, color);
			}
			else if (material.HasProperty(_shaderPropertyIds.ColorId))
			{
				_propertyBlock.SetColor(_shaderPropertyIds.ColorId, color);
			}
		}
		renderer.SetPropertyBlock(_propertyBlock);
	}
}
