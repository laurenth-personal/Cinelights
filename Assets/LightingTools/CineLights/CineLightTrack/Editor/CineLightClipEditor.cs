using LightUtilities;
using UnityEditor;
using UnityEngine;

//Disabled : doesn't work well with animation so far
/*
[CustomEditor(typeof(CineLightClip))]
public class CineLightClipEditor : Editor
{
    SerializedProperty yaw;
    SerializedProperty pitch;
    SerializedProperty roll;
    SerializedProperty distance;
    SerializedProperty offset;
    SerializedProperty displayName;
    SerializedProperty linkToCameraRotation;

    SerializedProperty type;
    SerializedProperty range;
    SerializedProperty spotAngle;
    SerializedProperty cookie;
    SerializedProperty cookieSize;
    SerializedProperty color;
    SerializedProperty intensity;
    SerializedProperty bounceIntensity;
    SerializedProperty colorTemperature;
    SerializedProperty useColorTemperature;
    SerializedProperty shadowsType;
    SerializedProperty shadowsBias;
    SerializedProperty shadowsNormalBias;
    SerializedProperty shadowsNearPlane;
    SerializedProperty lightmapping;
    SerializedProperty areaSizeX;
    SerializedProperty areaSizeY;
    SerializedProperty bakedShadowRadius;
    SerializedProperty bakedShadowAngle;
    SerializedProperty shadowStrength;

    SerializedProperty drawGizmo;

    SerializedProperty useShadowCaster;
    SerializedProperty shadowCasterSize;
    SerializedProperty shadowCasterDistance;
    SerializedProperty shadowCasterOffset;


    void OnEnable()
    {
        yaw = serializedObject.FindProperty("lightTargetClip.lightTargetParameters.Yaw");
        pitch = serializedObject.FindProperty("lightTargetClip.lightTargetParameters.Pitch");
        roll = serializedObject.FindProperty("lightTargetClip.lightTargetParameters.Roll");
        distance = serializedObject.FindProperty("lightTargetClip.lightTargetParameters.distance");
        offset = serializedObject.FindProperty("lightTargetClip.lightTargetParameters.offset");
        displayName = serializedObject.FindProperty("lightTargetClip.lightTargetParameters.displayName");

        type = serializedObject.FindProperty("lightTargetClip.lightParameters.type");
        range = serializedObject.FindProperty("lightTargetClip.lightParameters.range");
        spotAngle = serializedObject.FindProperty("lightTargetClip.lightParameters.lightAngle");
        cookie = serializedObject.FindProperty("lightTargetClip.lightParameters.lightCookie");
        cookieSize = serializedObject.FindProperty("lightTargetClip.lightParameters.cookieSize");
        color = serializedObject.FindProperty("lightTargetClip.lightParameters.colorFilter");
        intensity = serializedObject.FindProperty("lightTargetClip.lightParameters.intensity");
        bounceIntensity = serializedObject.FindProperty("lightTargetClip.lightParameters.indirectIntensity");
        colorTemperature = serializedObject.FindProperty("lightTargetClip.lightParameters.colorTemperature");
        useColorTemperature = serializedObject.FindProperty("lightTargetClip.lightParameters.useColorTemperature");
        shadowsType = serializedObject.FindProperty("lightTargetClip.lightParameters.shadows");
        shadowsBias = serializedObject.FindProperty("lightTargetClip.lightParameters.shadowBias");
        shadowsNormalBias = serializedObject.FindProperty("lightTargetClip.lightParameters.shadowNormalBias");
        shadowsNearPlane = serializedObject.FindProperty("lightTargetClip.lightParameters.ShadowNearClip");
        shadowStrength = serializedObject.FindProperty("lightTargetClip.lightParameters.shadowStrength");

        drawGizmo = serializedObject.FindProperty("lightTargetClip.lightTargetParameters.drawGizmo");

        useShadowCaster = serializedObject.FindProperty("lightTargetClip.shadowCasterParameters.useShadowCaster");
        shadowCasterSize = serializedObject.FindProperty("lightTargetClip.shadowCasterParameters.shadowCasterSize");
        shadowCasterDistance = serializedObject.FindProperty("lightTargetClip.shadowCasterParameters.shadowCasterDistance");
        shadowCasterOffset = serializedObject.FindProperty("lightTargetClip.shadowCasterParameters.shadowCasterOffset");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(displayName);

        EditorLightingUtilities.DrawSplitter();
        //EditorGUI.indentLevel--;
        yaw.isExpanded = EditorLightingUtilities.DrawHeaderFoldout("Rig", yaw.isExpanded);
        EditorGUI.indentLevel++;

        if (yaw.isExpanded)
        {
            EditorGUILayout.PropertyField(yaw);
            EditorGUILayout.PropertyField(pitch);
            EditorGUILayout.PropertyField(roll);
            EditorGUILayout.PropertyField(distance);
            EditorGUILayout.PropertyField(offset);
        }


        EditorLightingUtilities.DrawSplitter();
        EditorGUI.indentLevel--;
        EditorLightingUtilities.DrawHeader("Light");
        EditorGUI.indentLevel++;

        EditorGUILayout.PropertyField(intensity);
        EditorGUILayout.PropertyField(color);
        EditorGUILayout.PropertyField(range);
        EditorGUILayout.PropertyField(bounceIntensity);
        EditorGUILayout.PropertyField(cookie);

        if (cookie.objectReferenceValue != null)
        {
            EditorGUILayout.PropertyField(cookieSize);
        }

        EditorLightingUtilities.DrawSplitter();
        EditorGUI.indentLevel--;
        EditorLightingUtilities.DrawHeader("Spot");
        EditorGUI.indentLevel++;

        EditorGUILayout.PropertyField(spotAngle);

        EditorLightingUtilities.DrawSplitter();
        EditorGUI.indentLevel--;
        EditorLightingUtilities.DrawHeader("Shadows");
        EditorGUI.indentLevel++;

        EditorGUILayout.PropertyField(shadowsType);
        if (shadowsType.enumValueIndex != 0)
        {
            EditorGUILayout.PropertyField(shadowsNearPlane);
            EditorGUILayout.PropertyField(shadowsBias);
            EditorGUILayout.PropertyField(shadowsNormalBias);
        }

        EditorLightingUtilities.DrawSplitter();
        EditorGUI.indentLevel--;
        drawGizmo.isExpanded = EditorLightingUtilities.DrawHeaderFoldout("Additional Settings", drawGizmo.isExpanded);
        EditorGUI.indentLevel++;

        if (drawGizmo.isExpanded)
        {
            EditorGUILayout.PropertyField(drawGizmo);
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(useShadowCaster);

            if (useShadowCaster.boolValue)
            {

                EditorGUILayout.LabelField("Shadow Caster", EditorStyles.boldLabel);

                EditorGUILayout.PropertyField(shadowCasterSize);
                EditorGUILayout.PropertyField(shadowCasterDistance);
                EditorGUILayout.PropertyField(shadowCasterOffset);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
*/