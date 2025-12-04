using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileConfig))]
public class TileConfigEditor : Editor
{
    private const int MinPercent = 0;
    private const int MaxPercent = 100;
    private const int TotalPercent = 100;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        SerializedProperty oneColorProp = serializedObject.FindProperty("_oneColorStackWeightPercent");
        SerializedProperty twoColorsProp = serializedObject.FindProperty("_twoColorsStackWeightPercent");
        SerializedProperty threeColorsProp = serializedObject.FindProperty("_threeColorsStackWeightPercent");
        
        int oldOne = oneColorProp.intValue;
        int oldTwo = twoColorsProp.intValue;
        int oldThree = threeColorsProp.intValue;
        
        DrawPropertiesExcluding(serializedObject, "m_Script", "_oneColorStackWeightPercent",
            "_twoColorsStackWeightPercent", "_threeColorsStackWeightPercent");

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Color blocks spawn weights (%)", EditorStyles.boldLabel);
        
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.IntSlider(oneColorProp, MinPercent, MaxPercent,
            new GUIContent("One color stacks"));
        EditorGUILayout.IntSlider(twoColorsProp, MinPercent, MaxPercent,
            new GUIContent("Two colors stacks"));
        EditorGUILayout.IntSlider(threeColorsProp, MinPercent, MaxPercent,
            new GUIContent("Three colors stacks"));

        bool anyChanged = EditorGUI.EndChangeCheck();

        if (anyChanged)
        {
            int newOne = Mathf.Clamp(oneColorProp.intValue, MinPercent, MaxPercent);
            int newTwo = Mathf.Clamp(twoColorsProp.intValue, MinPercent, MaxPercent);
            int newThree = Mathf.Clamp(threeColorsProp.intValue, MinPercent, MaxPercent);

            bool oneChanged = newOne != oldOne;
            bool twoChanged = newTwo != oldTwo;
            bool threeChanged = newThree != oldThree;
            
            if (oneChanged && !twoChanged && !threeChanged)
            {
                RebalanceForSingleChange(newOne, oneColorProp, twoColorsProp, threeColorsProp, 
                    oldTwo, oldThree);
            }
            else if (twoChanged && !oneChanged && !threeChanged)
            {
                RebalanceForSingleChange(newTwo, twoColorsProp, oneColorProp, threeColorsProp, 
                    oldOne, oldThree);
            }
            else if (threeChanged && !oneChanged && !twoChanged)
            {
                RebalanceForSingleChange(newThree, threeColorsProp, oneColorProp, twoColorsProp, 
                    oldOne, oldTwo);
            }
            else
            {
                NormalizeToTotal(oneColorProp, twoColorsProp, threeColorsProp);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void RebalanceForSingleChange(int changedValue, SerializedProperty changedProp, 
        SerializedProperty otherPropA, SerializedProperty otherPropB, int oldOtherA, int oldOtherB)
    {
        changedValue = Mathf.Clamp(changedValue, MinPercent, MaxPercent);
        changedProp.intValue = changedValue;

        int remaining = Mathf.Clamp(TotalPercent - changedValue, 0, TotalPercent);

        int sumOldOthers = Mathf.Max(0, oldOtherA) + Mathf.Max(0, oldOtherB);

        int newA;
        int newB;

        if (sumOldOthers <= 0)
        {
            newA = remaining / 2;
            newB = remaining - newA;
        }
        else
        {
            float ratioA = (float)oldOtherA / sumOldOthers;
            newA = Mathf.RoundToInt(remaining * ratioA);
            newB = remaining - newA;
        }

        newA = Mathf.Clamp(newA, MinPercent, MaxPercent);
        newB = Mathf.Clamp(newB, MinPercent, MaxPercent);

        otherPropA.intValue = newA;
        otherPropB.intValue = newB;

        FixRoundingError(changedProp, otherPropA, otherPropB);
    }

    private void NormalizeToTotal(SerializedProperty oneColorProp, SerializedProperty twoColorsProp,
        SerializedProperty threeColorsProp)
    {
        int one = Mathf.Max(MinPercent, oneColorProp.intValue);
        int two = Mathf.Max(MinPercent, twoColorsProp.intValue);
        int three = Mathf.Max(MinPercent, threeColorsProp.intValue);

        int sum = one + two + three;

        if (sum <= 0)
        {
            one = TotalPercent / 3;
            two = TotalPercent / 3;
            three = TotalPercent - one - two;
        }
        else
        {
            float k = (float)TotalPercent / sum;

            one = Mathf.RoundToInt(one * k);
            two = Mathf.RoundToInt(two * k);
            three = Mathf.RoundToInt(three * k);

            oneColorProp.intValue = one;
            twoColorsProp.intValue = two;
            threeColorsProp.intValue = three;

            FixRoundingError(oneColorProp, twoColorsProp, threeColorsProp);
            
            return;
        }

        oneColorProp.intValue = one;
        twoColorsProp.intValue = two;
        threeColorsProp.intValue = three;

        FixRoundingError(oneColorProp, twoColorsProp, threeColorsProp);
    }

    private void FixRoundingError(SerializedProperty oneColorProp, SerializedProperty twoColorsProp,
        SerializedProperty threeColorsProp)
    {
        int one = oneColorProp.intValue;
        int two = twoColorsProp.intValue;
        int three = threeColorsProp.intValue;

        int sum = one + two + three;

        if (sum == TotalPercent)
            return;

        int diff = TotalPercent - sum;
        
        if (one >= two && one >= three)
        {
            one = Mathf.Clamp(one + diff, MinPercent, MaxPercent);
            oneColorProp.intValue = one;
        }
        else if (two >= one && two >= three)
        {
            two = Mathf.Clamp(two + diff, MinPercent, MaxPercent);
            twoColorsProp.intValue = two;
        }
        else
        {
            three = Mathf.Clamp(three + diff, MinPercent, MaxPercent);
            threeColorsProp.intValue = three;
        }
    }
}