using UnityEngine;

public class TileShaderPropertyIds
{
	private const string BASE_COLOR_NAME = "_BaseColor";

	private const string COLOR_NAME = "_Color";

	public readonly int BaseColorId;

	public readonly int ColorId;

	public TileShaderPropertyIds()
	{
		BaseColorId = Shader.PropertyToID("_BaseColor");
		ColorId = Shader.PropertyToID("_Color");
	}
}
