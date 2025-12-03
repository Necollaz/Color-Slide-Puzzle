using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileConfig", menuName = "Game/Tiles/Tile Config")]
public class TileConfig : ScriptableObject
{
    [Header("Color")]
    [SerializeField] private Color[] _colors;

    [Header("Stack size limits")]
    [SerializeField, Min(1)] private int _maxStackSize = 10;
    [SerializeField, Min(1)] private int _minGeneratedStackSize = 3;
    [SerializeField, Min(1)] private int _maxGeneratedStackSize = 8;

    [Header("Cluster generation")]
    [SerializeField, Min(1)] private int _minClusterSize = 2;
    [SerializeField, Min(1)] private int _maxClusterSize = 4;

    [Header("Color blocks per stack")]
    [SerializeField, Min(1)] private int _minColorBlocksPerStack = 1;
    [SerializeField, Min(1)] private int _maxColorBlocksPerStack = 3;
    [SerializeField, Min(1)] private int _maxColorPickAttempts = 8;
    [SerializeField, Min(1)] private int _minTilesPerColorBlock = 1;

    [Header("Spawn rules per color")]
    [SerializeField, Min(1)] private int _minTilesPerColorSpawn = 3;
    [SerializeField, Min(1)] private int _maxTilesPerColorSpawn = 9;
    [SerializeField, Min(1)] private int _maxStackDistributionIterations = 64;
    [SerializeField, Min(1)] private int _maxColorsPerSpawnStack = 3;
    
    [Header("Color blocks spawn weights (%)")]
    [SerializeField, Range(0, 100)] private int _oneColorStackWeightPercent = 10;
    [SerializeField, Range(0, 100)] private int _twoColorsStackWeightPercent = 60;
    [SerializeField, Range(0, 100)] private int _threeColorsStackWeightPercent = 30;
    
    [Header("Visual size relative to HexCell (field stacks)")]
    [SerializeField, Range(0.1f, 1f)] private float _xzScaleFactor = 0.9f;
    [SerializeField, Range(0.1f, 1f)] private float _stackHeight = 0.3f;
    [SerializeField] private float _yOffset = 0.05f;

    [Header("Visual size for spawn stacks (world space)")]
    [SerializeField, Range(0.1f, 1f)] private float _spawnXzWorldSize = 0.5f;
    [SerializeField, Range(0.1f, 1f)] private float _spawnStackWorldHeight = 1.0f;
    [SerializeField] private float _spawnYOffset = 0.05f;

    [Header("Visual stack segments")]
    [SerializeField, Min(0.001f)] private float _segmentHeight = 0.04f;
    [SerializeField, Min(0f)] private float _segmentGap = 0.005f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem _tileClearEffectPrefab;
    [SerializeField] private ParticleSystem _stackClearEffectPrefab;
    [SerializeField, Min(0)] private int _tileClearEffectPrewarmCount = 20;
    [SerializeField, Min(0)] private int _stackClearEffectPrewarmCount = 5;
    
    public IReadOnlyList<Color> Colors => _colors;

    public int MaxStackSize => _maxStackSize;
    public int MinGeneratedStackSize => _minGeneratedStackSize;
    public int MaxGeneratedStackSize => _maxGeneratedStackSize;

    public int MinClusterSize => _minClusterSize;
    public int MaxClusterSize => _maxClusterSize;

    public int MinColorBlocksPerStack => _minColorBlocksPerStack;
    public int MaxColorBlocksPerStack => _maxColorBlocksPerStack;
    public int MaxColorPickAttempts => _maxColorPickAttempts;
    public int MinTilesPerColorBlock => _minTilesPerColorBlock;

    public int MinTilesPerColorSpawn => _minTilesPerColorSpawn;
    public int MaxTilesPerColorSpawn => _maxTilesPerColorSpawn;
    public int MaxStackDistributionIterations => _maxStackDistributionIterations;
    public int MaxColorsPerSpawnStack => _maxColorsPerSpawnStack;

    public int OneColorStackWeightPercent => _oneColorStackWeightPercent;
    public int TwoColorsStackWeightPercent => _twoColorsStackWeightPercent;
    public int ThreeColorsStackWeightPercent => _threeColorsStackWeightPercent;
    
    public float XzScaleFactor => _xzScaleFactor;
    public float YOffset => _yOffset;
    public float StackHeight => _stackHeight;
    
    public float SpawnXzWorldSize => _spawnXzWorldSize;
    public float SpawnYOffset => _spawnYOffset;
    public float SpawnStackWorldHeight => _spawnStackWorldHeight;

    public float SegmentHeight => _segmentHeight;
    public float SegmentGap => _segmentGap;
    
    public ParticleSystem TileClearEffectPrefab => _tileClearEffectPrefab;
    public ParticleSystem StackClearEffectPrefab => _stackClearEffectPrefab;
    public int TileClearEffectPrewarmCount => _tileClearEffectPrewarmCount;
    public int StackClearEffectPrewarmCount => _stackClearEffectPrewarmCount;
}