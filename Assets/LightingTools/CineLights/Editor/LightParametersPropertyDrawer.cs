using LightUtilities;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer (typeof (LightParameters))]
public class LightParametersPropertyDrawer : PropertyDrawer {

    SerializedProperty cullingMask;

	// Draw the property inside the given rect
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) 
	{
		// Using BeginProperty / EndProperty on the parent property means that
		// prefab override logic works on the entire property.
		EditorGUI.BeginProperty (position, label, property);

        EditorGUI.indentLevel--;
        EditorLightingUtilities.DrawSplitter();
        EditorLightingUtilities.DrawHeader("Light");
        EditorGUI.indentLevel++;

		EditorGUILayout.PropertyField (property.FindPropertyRelative ("intensity"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("colorFilter"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("type"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative ("mode"), new GUIContent("Light mode"));
		EditorGUILayout.PropertyField(property.FindPropertyRelative("range"));
		EditorGUILayout.PropertyField(property.FindPropertyRelative("indirectIntensity"));
		EditorGUILayout.PropertyField(property.FindPropertyRelative("lightCookie"));
		
		if (property.FindPropertyRelative("lightCookie").objectReferenceValue != null)
		{
			EditorGUILayout.PropertyField(property.FindPropertyRelative("cookieSize"));	
		}

        // Draw label
        EditorGUILayout.Space();
        EditorGUI.indentLevel--;
        EditorLightingUtilities.DrawSplitter();
        EditorLightingUtilities.DrawHeader("Shape");
        EditorGUI.indentLevel++;
        if (property.FindPropertyRelative("type").enumValueIndex == 0) //if spotlight
		{
			EditorGUILayout.PropertyField (property.FindPropertyRelative ("lightAngle"));
			EditorGUILayout.PropertyField (property.FindPropertyRelative ("innerSpotPercent"));
			EditorGUILayout.PropertyField (property.FindPropertyRelative ("maxSmoothness"));
		}
		
		// Draw label
		EditorGUILayout.Space();
        EditorGUI.indentLevel--;
        EditorLightingUtilities.DrawSplitter();
        property.FindPropertyRelative("shadows").boolValue = EditorLightingUtilities.DrawHeader("Shadows", property.FindPropertyRelative("shadows").boolValue);
        EditorGUI.indentLevel++;
        // Draw fields
        //EditorGUILayout.PropertyField (property.FindPropertyRelative ("shadows"));
		if (property.FindPropertyRelative("shadows").boolValue != false)
		{
			EditorGUILayout.PropertyField (property.FindPropertyRelative ("ShadowNearClip"));
			EditorGUILayout.PropertyField (property.FindPropertyRelative ("shadowResolution"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("shadowStrength"),new GUIContent("Shadow Dimmer"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("viewBiasMin"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("viewBiasScale"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("normalBias"));
        }
        cullingMask = property.FindPropertyRelative("cullingMask");

        // Draw label
        EditorGUILayout.Space();
        EditorGUI.indentLevel--;
        EditorLightingUtilities.DrawSplitter();
        cullingMask.isExpanded = EditorLightingUtilities.DrawHeaderFoldout("Additional settings",cullingMask.isExpanded);
        EditorGUI.indentLevel++;

        if(cullingMask.isExpanded)
        {
            EditorGUILayout.PropertyField(property.FindPropertyRelative("affectDiffuse"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("affectSpecular"));
        }
        EditorGUI.EndProperty ();
	}
}