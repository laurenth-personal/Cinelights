using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LightUtilities;
using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine.Experimental.Rendering;

[CustomEditor(typeof(CineLight))]
public class CineLightEditor : Editor
{
    public CineLight cineLight;
    SerializedProperty lightTargetParameters;
    SerializedProperty lightParameters;
    public List<Vector3> lightPivots;
    SerializedObject m_SerializedLight;
    SerializedObject m_SerializedLightParentPitchTransform;
    SerializedObject m_SerializedLightParentYawTransform;
    SerializedObject m_SerializedLightTransform;
    SerializedObject m_SerializedShadowCasterTransform;
	SerializedObject m_SerializedAdditionalLightData;
	SerializedObject m_SerializedAdditionalShadowData;

    public SerializedProperty lightParentYawLocalPosition;
    public SerializedProperty lightParentYawLocalRotation;
    public SerializedProperty lightParentPitchLocalRotation;
    public SerializedProperty shadowCasterLocalPosition;

    SerializedProperty displayName;
    SerializedProperty drawGizmo;
    SerializedProperty showEntities;

    public SerializedProperty type;
    public SerializedProperty range;
    public SerializedProperty spotAngle;
    public SerializedProperty cookie;
    public SerializedProperty cookieSize;
    public SerializedProperty color;
    public SerializedProperty intensity;
    public SerializedProperty bounceIntensity;
    public SerializedProperty colorTemperature;
    public SerializedProperty useColorTemperature;
    public SerializedProperty shadowsType;
    public SerializedProperty shadowsQuality;
    //public SerializedProperty shadowsBias;
    public SerializedProperty shadowsNormalBias;
    public SerializedProperty shadowsNearPlane;
    public SerializedProperty lightmapping;
    public SerializedProperty areaSizeX;
    public SerializedProperty areaSizeY;
    //public SerializedProperty bakedShadowRadius;
    //public SerializedProperty bakedShadowAngle;
    public SerializedProperty cullingMask;

	//HDAdditionalLightSettings
	public SerializedProperty innerSpotPercent;
	public SerializedProperty maxSmoothness;
	public SerializedProperty affectDiffuse;
	public SerializedProperty affectSpecular;
	public SerializedProperty fadeDistance;

    public HDAdditionalLightData additionalLightData;

	//AdditionalShadowSettings
	public SerializedProperty shadowResolution;
	public SerializedProperty shadowFadeDistance;
    public SerializedProperty shadowDimmer;
    public SerializedProperty viewBiasMin;
    public SerializedProperty viewBiasScale;
    public SerializedProperty normalBiasMin;
    public SerializedProperty normalBiasMax;

    public SerializedProperty yaw;
    public SerializedProperty pitch;
    public SerializedProperty roll;
    public SerializedProperty distance;
    public SerializedProperty lightRotation;

    public SerializedProperty useShadowCaster;
    public SerializedProperty shadowsCasterSize;
    public SerializedProperty shadowsCasterDistance;
    public SerializedProperty shadowsCasterOffset;
    public SerializedProperty shadowCasterLocalScale;

    void OnEnable()
    {
        cineLight = (CineLight)serializedObject.targetObject;

        Light light = cineLight.GetComponentInChildren<Light>();
        Transform lightTransform = light.transform;
        Transform lightParentPitchTransform = light.transform.parent;
        Transform lightParentYawTransform = lightParentPitchTransform.transform.parent;

        m_SerializedLight = new SerializedObject(light);
        m_SerializedLightParentYawTransform = new SerializedObject(lightParentYawTransform);
        m_SerializedLightTransform = new SerializedObject(lightTransform);
        m_SerializedLightParentPitchTransform = new SerializedObject(lightParentPitchTransform);

		//HDRP
		m_SerializedAdditionalLightData = new SerializedObject(light.gameObject.GetComponent<HDAdditionalLightData>());
		m_SerializedAdditionalShadowData = new SerializedObject(light.gameObject.GetComponent<AdditionalShadowData>());

        yaw = serializedObject.FindProperty("Yaw");
        pitch = serializedObject.FindProperty("Pitch");
        roll = serializedObject.FindProperty("Roll");

        type = m_SerializedLight.FindProperty("m_Type");
        range = m_SerializedLight.FindProperty("m_Range");
        spotAngle = m_SerializedLight.FindProperty("m_SpotAngle");
        cookie = m_SerializedLight.FindProperty("m_Cookie");
        cookieSize = m_SerializedLight.FindProperty("m_CookieSize");
        color = m_SerializedLight.FindProperty("m_Color");
        //intensity = m_SerializedLight.FindProperty("m_Intensity");
        bounceIntensity = m_SerializedLight.FindProperty("m_BounceIntensity");
        colorTemperature = m_SerializedLight.FindProperty("m_ColorTemperature");
        useColorTemperature = m_SerializedLight.FindProperty("m_UseColorTemperature");
        shadowsType = m_SerializedLight.FindProperty("m_Shadows.m_Type");
        shadowsQuality = m_SerializedLight.FindProperty("m_Shadows.m_Resolution");
        shadowsNormalBias = m_SerializedLight.FindProperty("m_Shadows.m_NormalBias");
        shadowsNearPlane = m_SerializedLight.FindProperty("m_Shadows.m_NearPlane");
        lightmapping = m_SerializedLight.FindProperty("m_Lightmapping");
        areaSizeX = m_SerializedLight.FindProperty("m_AreaSize.x");
        areaSizeY = m_SerializedLight.FindProperty("m_AreaSize.y");
        //bakedShadowRadius = m_SerializedLight.FindProperty("m_ShadowRadius");
        //bakedShadowAngle = m_SerializedLight.FindProperty("m_ShadowAngle");
        cullingMask = m_SerializedLight.FindProperty("m_CullingMask");

        useShadowCaster = serializedObject.FindProperty("useShadowCaster");
        shadowsCasterSize = serializedObject.FindProperty("shadowsCasterSize");
        shadowsCasterDistance = serializedObject.FindProperty("shadowsCasterDistance");
        shadowsCasterOffset = serializedObject.FindProperty("shadowsCasterOffset");

        displayName = serializedObject.FindProperty("displayName");
        drawGizmo = serializedObject.FindProperty("drawGizmo");
        showEntities = serializedObject.FindProperty("showEntities");

        lightParentYawLocalPosition = m_SerializedLightParentYawTransform.FindProperty("m_LocalPosition");
        lightParentYawLocalRotation = m_SerializedLightParentYawTransform.FindProperty("m_LocalRotation");
        lightParentPitchLocalRotation = m_SerializedLightParentPitchTransform.FindProperty("m_LocalRotation");

        distance = m_SerializedLightTransform.FindProperty("m_LocalPosition.z");
        lightRotation = m_SerializedLightTransform.FindProperty("m_LocalRotation");

		innerSpotPercent = m_SerializedAdditionalLightData.FindProperty("m_InnerSpotPercent");
		maxSmoothness = m_SerializedAdditionalLightData.FindProperty("maxSmoothness");
		affectDiffuse = m_SerializedAdditionalLightData.FindProperty("affectDiffuse");
		affectSpecular = m_SerializedAdditionalLightData.FindProperty("affectSpecular");
		fadeDistance = m_SerializedAdditionalLightData.FindProperty("fadeDistance");
        intensity = m_SerializedAdditionalLightData.FindProperty("punctualIntensity");
        normalBiasMax = m_SerializedAdditionalShadowData.FindProperty("normalBiasMax");

        shadowResolution = m_SerializedAdditionalShadowData.FindProperty ("shadowResolution");
        shadowFadeDistance = m_SerializedAdditionalShadowData.FindProperty("shadowFadeDistance");
        viewBiasMin = m_SerializedAdditionalShadowData.FindProperty ("viewBiasMin");
        viewBiasScale = m_SerializedAdditionalShadowData.FindProperty("viewBiasScale");
        normalBiasMin = m_SerializedAdditionalShadowData.FindProperty("normalBiasMin");
        normalBiasMax = m_SerializedAdditionalShadowData.FindProperty("normalBiasMax");
        shadowDimmer = m_SerializedAdditionalShadowData.FindProperty("shadowDimmer");

        InitShadowCasterSerializedObject();

        additionalLightData = (HDAdditionalLightData)m_SerializedAdditionalLightData.targetObject;
    }

    void InitShadowCasterSerializedObject()
    {
        if (cineLight.shadowCasterGO != null)
        {
            Transform shadowCaster = cineLight.shadowCasterGO.transform;
            m_SerializedShadowCasterTransform = new SerializedObject(shadowCaster);
            shadowCasterLocalPosition = m_SerializedShadowCasterTransform.FindProperty("m_LocalPosition");
            shadowCasterLocalScale = m_SerializedShadowCasterTransform.FindProperty("m_LocalScale");
        }
    }

    public override void OnInspectorGUI()
    {
        if (m_SerializedShadowCasterTransform != null && cineLight.shadowCasterGO == null)
            m_SerializedShadowCasterTransform = null;

        serializedObject.Update();
        m_SerializedLight.Update();
        m_SerializedLightTransform.Update();
        m_SerializedLightParentPitchTransform.Update();
        m_SerializedLightParentYawTransform.Update();
		m_SerializedAdditionalLightData.Update ();
		m_SerializedAdditionalShadowData.Update ();
        if(cineLight.shadowCasterGO != null)
            m_SerializedShadowCasterTransform.Update();

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(displayName);



        EditorLightingUtilities.DrawSplitter();
        //EditorGUI.indentLevel--;
        yaw.isExpanded = EditorLightingUtilities.DrawHeaderFoldout("Rig",yaw.isExpanded);
        EditorGUI.indentLevel++;

        if(yaw.isExpanded)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(yaw);
            if (EditorGUI.EndChangeCheck())
            {
                lightParentYawLocalRotation.quaternionValue = Quaternion.Euler(0, yaw.floatValue,0);
            }

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(pitch);
            if (EditorGUI.EndChangeCheck())
            {
                lightParentPitchLocalRotation.quaternionValue = Quaternion.Euler(-pitch.floatValue, 0,0 );
            }

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(roll);
            if(EditorGUI.EndChangeCheck())
            {
                lightRotation.quaternionValue = Quaternion.Euler(0, 180, roll.floatValue+180);
            }

            EditorGUILayout.PropertyField(distance, new GUIContent("Distance"));
            EditorGUILayout.PropertyField(lightParentYawLocalPosition, new GUIContent("Offset"));
        }


        EditorLightingUtilities.DrawSplitter();
        EditorGUI.indentLevel--;
        color.isExpanded = EditorLightingUtilities.DrawHeaderFoldout("Light",color.isExpanded);
        EditorGUI.indentLevel++;

        if(color.isExpanded)
        {
			EditorGUILayout.PropertyField(useColorTemperature);
            if(useColorTemperature.boolValue)
                EditorGUILayout.PropertyField(colorTemperature);
			EditorGUILayout.PropertyField(color);
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(intensity);
            if (EditorGUI.EndChangeCheck())
            {
                m_SerializedAdditionalLightData.ApplyModifiedProperties();
                additionalLightData.ConvertPhysicalLightIntensityToLightIntensity();
            }
            EditorGUILayout.PropertyField(bounceIntensity);
            EditorGUILayout.PropertyField(range);
            EditorGUILayout.PropertyField(lightmapping);
            EditorGUILayout.PropertyField(cookie);
            if(cookie.objectReferenceValue != null)
                EditorGUILayout.PropertyField(cookieSize);
        }

        EditorLightingUtilities.DrawSplitter();
        EditorGUI.indentLevel--;
        spotAngle.isExpanded = EditorLightingUtilities.DrawHeaderFoldout("Shape",spotAngle.isExpanded);
        EditorGUI.indentLevel++;
        if(spotAngle.isExpanded)
		{
            EditorGUILayout.Slider(spotAngle,0,180);
			EditorGUILayout.Slider (innerSpotPercent, 0, 100);
			EditorGUILayout.Slider (maxSmoothness, 0, 1);
		}

        EditorLightingUtilities.DrawSplitter();
        EditorGUI.indentLevel--;
        shadowsType.isExpanded = EditorLightingUtilities.DrawHeaderFoldout("Shadows", shadowsType.isExpanded);
        EditorGUI.indentLevel++;

        if(shadowsType.isExpanded)
        {
            EditorGUILayout.PropertyField(shadowsType);
            if(shadowsType.enumValueIndex > 0)
            {
                //EditorGUILayout.PropertyField(shadowsQuality);
				EditorGUILayout.PropertyField (shadowResolution);
				EditorGUILayout.PropertyField(shadowsNearPlane);
                EditorGUILayout.PropertyField(viewBiasMin);
                EditorGUILayout.PropertyField(viewBiasScale);
                EditorGUILayout.PropertyField(normalBiasMin);
                normalBiasMax = normalBiasMin;
                //EditorGUILayout.PropertyField(bakedShadowRadius);
                //EditorGUILayout.PropertyField(bakedShadowAngle);
            }
        }

        EditorLightingUtilities.DrawSplitter();
        EditorGUI.indentLevel--;
        drawGizmo.isExpanded = EditorLightingUtilities.DrawHeaderFoldout("Additional Settings",drawGizmo.isExpanded);
        EditorGUI.indentLevel++;

        if (drawGizmo.isExpanded)
        {
            EditorGUILayout.PropertyField(drawGizmo);
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(showEntities);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                cineLight.ApplyShowFlags(showEntities.boolValue);
            }

            EditorGUILayout.PropertyField(cullingMask);

			EditorGUILayout.PropertyField(affectDiffuse);
			EditorGUILayout.PropertyField(affectSpecular);
            EditorGUILayout.PropertyField(shadowDimmer);
			EditorGUILayout.PropertyField(fadeDistance);
			EditorGUILayout.PropertyField(shadowFadeDistance);

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(useShadowCaster);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                cineLight.ApplyShadowCaster();
            }

            if (useShadowCaster.boolValue)
            {
                if (m_SerializedShadowCasterTransform == null )
                    InitShadowCasterSerializedObject();

                EditorGUI.BeginChangeCheck();
                EditorGUILayout.LabelField("Shadow Caster", EditorStyles.boldLabel);
                if (EditorGUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    cineLight.ApplyShadowCaster();
                    m_SerializedShadowCasterTransform.ApplyModifiedProperties();
                }
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(shadowsCasterSize);
                EditorGUILayout.PropertyField(shadowsCasterDistance);
                EditorGUILayout.PropertyField(shadowsCasterOffset);
                if (EditorGUI.EndChangeCheck())
                {
                    if (cineLight.shadowCasterGO != null)
                    {
                        m_SerializedShadowCasterTransform.ApplyModifiedProperties();
                        shadowCasterLocalPosition.vector3Value = new Vector3(shadowsCasterOffset.vector2Value.x, shadowsCasterOffset.vector2Value.y, -shadowsCasterDistance.floatValue);
                        shadowCasterLocalScale.vector3Value = new Vector3(shadowsCasterSize.vector2Value.x, shadowsCasterSize.vector2Value.y, 1);
                    }
                }
            }
        }

        serializedObject.ApplyModifiedProperties();
        m_SerializedLight.ApplyModifiedProperties();
        m_SerializedLightTransform.ApplyModifiedProperties();
        m_SerializedLightParentPitchTransform.ApplyModifiedProperties();
        m_SerializedLightParentYawTransform.ApplyModifiedProperties();
		m_SerializedAdditionalLightData.ApplyModifiedProperties ();
		m_SerializedAdditionalShadowData.ApplyModifiedProperties ();
        if (cineLight.shadowCasterGO != null)
            m_SerializedShadowCasterTransform.ApplyModifiedProperties();
    }
}