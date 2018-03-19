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
        property.FindPropertyRelative("useShadowCaster").boolValue = EditorLightingUtilities.DrawHeader("Shadowcaster", property.FindPropertyRelative("useShadowCaster").boolValue);
        EditorGUI.indentLevel++;

        if(property.FindPropertyRelative("useShadowCaster").boolValue)
        {
			EditorGUILayout.PropertyField(property.FindPropertyRelative("shadowCasterSize"));
			EditorGUILayout.PropertyField(property.FindPropertyRelative("shadowCasterDistance"));
			EditorGUILayout.PropertyField(property.FindPropertyRelative("shadowCasterOffset"));
           
        }

        EditorGUI.EndProperty();
    }
}