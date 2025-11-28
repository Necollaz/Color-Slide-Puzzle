using UnityEditor;

[CustomEditor(typeof(GridDefinitionConfig))]
public class GridDefinitionConfigEditor : Editor
{
    private SerializedProperty _shapeTypeProperty;
    private SerializedProperty _widthProperty;
    private SerializedProperty _heightProperty;
    private SerializedProperty _hexRadiusProperty;

    private void OnEnable()
    {
        _shapeTypeProperty = serializedObject.FindProperty("_shapeType");
        _widthProperty = serializedObject.FindProperty("_width");
        _heightProperty = serializedObject.FindProperty("_height");
        _hexRadiusProperty = serializedObject.FindProperty("_hexRadius");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_shapeTypeProperty);

        GridShapeType shapeType = (GridShapeType)_shapeTypeProperty.enumValueIndex;

        EditorGUILayout.Space();

        switch (shapeType)
        {
            case GridShapeType.Rectangle:
                EditorGUILayout.LabelField("Rectangle settings", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(_widthProperty);
                EditorGUILayout.PropertyField(_heightProperty);
                break;

            case GridShapeType.Hexagon:
                EditorGUILayout.LabelField("Hexagon settings", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(_hexRadiusProperty);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}