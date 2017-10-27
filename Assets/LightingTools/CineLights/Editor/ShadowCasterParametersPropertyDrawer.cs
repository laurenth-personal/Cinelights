using UnityEditor;
using UnityEngine;
using LightUtilities;

[CustomPropertyDrawer(typeof(ShadowCasterParameters))]
public class ShadowCasterParametersPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorLightingUtilities.DrawSplitter();

        EditorGUI.indentLevel--;
        EditorLightingUtilities.DrawHeader("Shadowcaster");
        EditorGUI.indentLevel++;

        EditorGUILayout.PropertyField(property.FindPropertyRelative("useShadowCaster"));

        EditorGUILayout.PropertyField(property.FindPropertyRelative("shadowCasterSize"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("shadowCasterDistance"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("shadowCasterOffset"));

        EditorGUI.EndProperty();
    }
}