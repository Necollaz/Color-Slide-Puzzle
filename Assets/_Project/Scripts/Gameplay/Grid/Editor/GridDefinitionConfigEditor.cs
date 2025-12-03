using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridDefinitionConfig))]
public class GridDefinitionConfigEditor : Editor
{
    private SerializedProperty _shapeTypeProperty;
    private SerializedProperty _widthProperty;
    private SerializedProperty _heightProperty;
    private SerializedProperty _hexRadiusProperty;
    private SerializedProperty _occupiedCellsCountProperty;

    private void OnEnable()
    {
        _shapeTypeProperty = serializedObject.FindProperty("_shapeType");
        _widthProperty = serializedObject.FindProperty("_width");
        _heightProperty = serializedObject.FindProperty("_height");
        _hexRadiusProperty = serializedObject.FindProperty("_hexRadius");
        _occupiedCellsCountProperty = serializedObject.FindProperty("_occupiedCellsCount");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_shapeTypeProperty);

        GridShapeType shapeType = (GridShapeType)_shapeTypeProperty.enumValueIndex;

        EditorGUILayout.Space();

        int totalCells = 1;
        
        switch (shapeType)
        {
            case GridShapeType.Rectangle:
                EditorGUILayout.LabelField("Rectangle settings", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(_widthProperty);
                EditorGUILayout.PropertyField(_heightProperty);

                int width = Mathf.Max(1, _widthProperty.intValue);
                int height = Mathf.Max(1, _heightProperty.intValue);
                totalCells = width * height;
                break;

            case GridShapeType.Hexagon:
                EditorGUILayout.LabelField("Hexagon settings", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(_hexRadiusProperty);

                int radius = Mathf.Max(1, _hexRadiusProperty.intValue);
                totalCells = 1 + 3 * radius * (radius + 1);
                break;
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Stacks", EditorStyles.boldLabel);

        int minOccupied = 1;
        int maxOccupied = Mathf.Max(1, totalCells - 1);

        if (totalCells <= 1)
        {
            EditorGUILayout.HelpBox("Сетка слишком маленькая, чтобы оставить хотя бы одну пустую клетку.", MessageType.Warning);
            _occupiedCellsCountProperty.intValue = 1;
        }
        else
        {
            _occupiedCellsCountProperty.intValue = Mathf.Clamp(_occupiedCellsCountProperty.intValue, 1, maxOccupied);

            EditorGUILayout.IntSlider(_occupiedCellsCountProperty, minOccupied, maxOccupied,
                new GUIContent("Occupied Cells Count"));

            EditorGUILayout.LabelField($"Total cells: {totalCells}", EditorStyles.miniLabel);
            EditorGUILayout.LabelField($"Max occupied (leaving 1 empty): {maxOccupied}", EditorStyles.miniLabel);
        }

        serializedObject.ApplyModifiedProperties();
    }
}