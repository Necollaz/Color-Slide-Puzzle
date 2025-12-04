using UnityEngine;

public class TileShaderPropertyIds
{
    private const string BASE_COLOR_NAME = "_BaseColor";
    private const string COLOR_NAME = "_Color";

    public readonly int BaseColorId;
    public readonly int ColorId;

    public TileShaderPropertyIds()
    {
        BaseColorId = Shader.PropertyToID(BASE_COLOR_NAME);
        ColorId = Shader.PropertyToID(COLOR_NAME);
    }
}