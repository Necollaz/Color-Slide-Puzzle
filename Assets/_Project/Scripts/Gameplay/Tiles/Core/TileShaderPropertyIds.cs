using UnityEngine;

public class TileShaderPropertyIds
{
    public const string BaseColorName = "_BaseColor";
    public const string ColorName = "_Color";

    public readonly int BaseColorId;
    public readonly int ColorId;

    public TileShaderPropertyIds()
    {
        BaseColorId = Shader.PropertyToID(BaseColorName);
        ColorId = Shader.PropertyToID(ColorName);
    }
}