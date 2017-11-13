﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.HDPipeline;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Object = UnityEngine.Object;

namespace LightUtilities
{
    [System.Serializable]
    public enum LightmapPresetBakeType
    {
        //Simplify serialization
        Baked = 0,
        Mixed = 1,
        Realtime = 2
    }

    public enum ShadowQuality
    {
        FromQualitySettings = 0,
        Low = 1,
        Medium = 2,
        High = 3,
        VeryHigh = 4
    }

    [System.Serializable]
    public enum LightShape
    {
        Point = 0,
        Spot = 1,
        Directional = 2,
        Rectangle = 3,
        Sphere = 4,
        Line = 5,
        Disc = 6,
        Frustum = 7
    }

    [System.Serializable]
    public class CascadeParameters
    {
        [Range(1, 4)]
        public int count = 4;
        [Range(0, 1)]
        public float split0 = 0.05f;
        [Range(0, 1)]
        public float split1 = 0.1f;
        [Range(0, 1)]
        public float split2 = 0.2f;
    }

    [System.Serializable]
	public class LightParameters
	{
        public LightParameters() { }

        public LightParameters(LightType specificType, LightmapPresetBakeType specificBakeMode)
        {
            type = specificType;
            mode = specificBakeMode;
        }

        public LightType type = LightType.Point;
        public LightmapPresetBakeType mode = LightmapPresetBakeType.Mixed;
		public float range = 8;
		public bool useColorTemperature;
		public float colorTemperature = 6500;
		public Color colorFilter = Color.white;
		public float intensity = 1;
		public float indirectIntensity = 1;
        [Range(0,180)]
        public float lightAngle = 45;
		public LightShadows shadows = LightShadows.Soft ;
        public ShadowQuality shadowQuality = ShadowQuality.Medium;
		[Range(0.01f,10f)]
		public float ShadowNearClip = 0.1f;
        public float shadowBias = 0.01f;
        public float shadowNormalBias = 0.1f;
        public Texture2D lightCookie;
		public float cookieSize = 5 ;
        public LightShape shape = LightShape.Point;
		[Range(0f,100f)]
        public float innerSpotPercent = 40;
        public float length;
        public float width;
        public float fadeDistance = 50;
        public float shadowFadeDistance = 10 ;
        public bool affectDiffuse = true;
        public bool affectSpecular = true;
        public float shadowStrength = 1;
        public float shadowMaxDistance = 150;
        public LayerMask cullingMask = -1 ;
		[Range(0,1)]
		public float maxSmoothness = 1;
		public int shadowResolution = 128;
    }

    [System.Serializable]
    public class CineLightParameters
    {
        public string displayName = "displayName";
        public bool linkToCameraRotation = false;
        [Range(-180f, 180f)]
        public float Yaw = 0f;
        [Range(-90f, 90f)]
        public float Pitch = 0f;
        [Range(-180f, 180f)]
        public float Roll = 0f;
        public float distance = 2f;
        public Vector3 offset;
        public bool drawGizmo = false;
    }

    [System.Serializable]
    public class ShadowCasterParameters
    {
        public bool useShadowCaster = false;
        public Vector2 shadowCasterSize = new Vector2(1, 1);
        public float shadowCasterDistance = 1;
        public Vector2 shadowCasterOffset = new Vector2(0, 0);
    }

    [System.Serializable]
    public class SunlightOrientationParameters
    {
        [Range(-180f, 180f)]
        public float yAxis = 0f;
        [Range(0f, 24f)]
        public float timeOfDay = 10f;
        [Range(0f, 90f)]
        public float lattitude = 35f;
        [Range(-180f, 180f)]
        public float Roll = 0.1f;
    }

        [System.Serializable]
    public class SunlightParameters
    {
        public SunlightAnimationParameters animationParameters = new SunlightAnimationParameters();
        public SunlightOrientationParameters orientationParameters = new SunlightOrientationParameters();
        public LightParameters lightParameters = new LightParameters();
        public ProceduralSkyboxParameters proceduralSkyParameters = new ProceduralSkyboxParameters();
    }

    [System.Serializable]
    public enum SunlightAnimationMode
    {
        Custom = 0,
        DayCycle = 1
    }

    [System.Serializable]
    public class SunlightAnimationParameters
    {
        public bool animate;
        public SunlightAnimationMode animationMode;
        public float dayLength = 1;
        public Gradient colorGradient;
    }

    [System.Serializable]
    public class ProceduralSkyboxParameters
    {
        [Range(0.01f,1)]
        public float sunSize = 0.05f;
        [Range(0.1f,4.5f)]
        public float atmosphereThickness = 1;
        public Color skyTint = Color.gray;
        public Color Ground = Color.gray;
        [Range(0,8)]
        public float exposure = 1.5f;
    }

    [System.Serializable]
	public class LightSourceMeshParameters
	{
		public GameObject lightSourceObject;
		public ShadowCastingMode meshShadowMode;
		public bool showObjectInHierarchy;
	}
	
	[System.Serializable]
	public class LightSourceMaterialsParameters
	{
		public bool linkEmissiveIntensityWithLight = true ;
		public bool linkEmissiveColorWithLight = true ;
		public float emissiveMultiplier = 1f;
	}

    [System.Serializable]
    public class LightSourceAnimationParameters
    {
	    public bool enabledFromStart = true ;
	    public bool enableFunctionalAnimation = false ;
	    public bool enableSwithOnAnimation = false ;
	    public bool enableSwithOffAnimation = false ;
	    public bool enableBreakAnimation = false ;

        public LightAnimationParameters functionalAnimationParameters;
	    public LightAnimationParameters switchOnAnimationParameters;
	    public LightAnimationParameters switchOffAnimationParameters;
	    public LightAnimationParameters breakAnimationParameters;
    }

    [System.Serializable]
    public class LightAnimationParameters
    {
        public AnimationMode animationMode;
        public AnimationCurve animationCurve;
        public AnimationClip animationClip;
        public NoiseAnimationParameters noiseParameters;
	    public float animationLength = 1;
    }

    [System.Serializable]
    public class NoiseAnimationParameters
    {
        public float frequency = 5;
        public float minimumValue = 0;
        public float maximumValue = 1;
        [Range(0.0f, 1.0f)]
        public float jumpFrequency = 0;
    }

    public enum AnimationMode {Curve, Noise, AnimationClip }

    public static class LightingUtilities
    {

        public static void ApplyLightParameters(Light light, LightParameters lightParameters)
        {
			//HD
			var additionalLightData = light.gameObject.GetComponent<HDAdditionalLightData>();
			var additionalShadowData = light.gameObject.GetComponent<AdditionalShadowData>();

            light.type = lightParameters.type;

#if UNITY_EDITOR
            switch (lightParameters.mode)
            {
                case LightmapPresetBakeType.Realtime: light.lightmapBakeType = LightmapBakeType.Realtime; break;
                case LightmapPresetBakeType.Baked: light.lightmapBakeType = LightmapBakeType.Baked; break;
                case LightmapPresetBakeType.Mixed: light.lightmapBakeType = LightmapBakeType.Mixed; break;
            }
#endif
            light.shadows = lightParameters.shadows;
            light.shadowStrength = lightParameters.shadowStrength;
            light.shadowNearPlane = lightParameters.ShadowNearClip;
            light.shadowResolution = (LightShadowResolution)lightParameters.shadowQuality;
            light.shadowNormalBias = lightParameters.shadowNormalBias;
            light.shadowBias = lightParameters.shadowBias;
            light.intensity = lightParameters.intensity;
            light.color = lightParameters.colorFilter;
            light.range = lightParameters.range;
            light.spotAngle = lightParameters.lightAngle;
            light.cookie = lightParameters.lightCookie;
            light.cullingMask = lightParameters.cullingMask;

			additionalLightData.affectDiffuse = lightParameters.affectDiffuse;
			additionalLightData.affectSpecular = lightParameters.affectSpecular;
			additionalLightData.maxSmoothness = lightParameters.maxSmoothness;
			additionalLightData.fadeDistance = lightParameters.fadeDistance;
			additionalLightData.m_InnerSpotPercent = lightParameters.innerSpotPercent;

			additionalShadowData.shadowFadeDistance = lightParameters.shadowMaxDistance;
			additionalShadowData.shadowResolution = lightParameters.shadowResolution;
			additionalShadowData.shadowDimmer = lightParameters.shadowStrength;
        }

        public static LightParameters LerpLightParameters(LightParameters from, LightParameters to, float weight)
        {
            var lerpLightParameters = new LightParameters();

            lerpLightParameters.intensity = Mathf.Lerp(from.intensity, to.intensity, weight);
            lerpLightParameters.indirectIntensity = Mathf.Lerp(from.indirectIntensity, to.indirectIntensity, weight);
            lerpLightParameters.range = Mathf.Lerp(from.range, to.range, weight);
            lerpLightParameters.lightAngle = Mathf.Lerp(from.lightAngle, to.lightAngle, weight);
            lerpLightParameters.type = from.type;
            lerpLightParameters.colorFilter = Color.Lerp(from.colorFilter, to.colorFilter, weight);
			lerpLightParameters.maxSmoothness = Mathf.Lerp (from.maxSmoothness, to.maxSmoothness, weight);
			lerpLightParameters.innerSpotPercent = Mathf.Lerp (from.innerSpotPercent, to.innerSpotPercent, weight);
            
            if (from.shadows == LightShadows.None && to.shadows != LightShadows.None)
            {
                lerpLightParameters.shadows = to.shadows;
            }
            if (from.shadows != LightShadows.None && to.shadows == LightShadows.None)
            {
                lerpLightParameters.shadows = from.shadows;
            }
            if (from.shadows == LightShadows.None && to.shadows == LightShadows.None)
            {
                lerpLightParameters.shadows = LightShadows.None;
            }

			lerpLightParameters.lightCookie = weight > 0.5f ? to.lightCookie : from.lightCookie;
            lerpLightParameters.shadowStrength = Mathf.Lerp(from.shadowStrength, to.shadowStrength, weight);
            lerpLightParameters.shadowBias = Mathf.Lerp(from.shadowBias, to.shadowBias, weight);
            lerpLightParameters.shadowNormalBias = Mathf.Lerp(from.shadowNormalBias, to.shadowNormalBias, weight);
            lerpLightParameters.ShadowNearClip = Mathf.Lerp(from.ShadowNearClip, to.ShadowNearClip, weight);
			lerpLightParameters.shadowResolution = (int)Mathf.Lerp(from.shadowResolution, to.shadowResolution, weight);

			lerpLightParameters.affectDiffuse = weight > 0.5f ? to.affectDiffuse : from.affectDiffuse;
			lerpLightParameters.affectSpecular = weight > 0.5f ? to.affectSpecular : from.affectSpecular ;

			lerpLightParameters.cullingMask = weight > 0.5f ? to.cullingMask : from.cullingMask ;
			lerpLightParameters.shadowQuality = weight > 0.5f ? to.shadowQuality : from.shadowQuality ;

            return lerpLightParameters;
        }

        public static void ApplyCineLightParameters(CineLight light, CineLightParameters parameters)
        {
            light.offset = parameters.offset;
            light.LightParentYaw.transform.localPosition = parameters.offset;
            light.Yaw = parameters.Yaw;
            light.LightParentYaw.transform.localRotation = Quaternion.Euler(0, parameters.Yaw, 0);
            light.Pitch = parameters.Pitch;
            light.LightParentPitch.transform.localRotation = Quaternion.Euler(-parameters.Pitch, 0, 0);
            light.Roll = parameters.Roll;
            light.light.transform.localRotation = Quaternion.Euler(0, 180, parameters.Roll + 180);
            light.distance = parameters.distance;
            light.light.transform.localPosition = new Vector3(0, 0, parameters.distance);
        }

        public static CineLightParameters LerpLightTargetParameters(CineLightParameters from, CineLightParameters to, float weight)
        {
            var lerpLightTargetParameters = new CineLightParameters();

            lerpLightTargetParameters.Yaw = Mathf.Lerp(from.Yaw, to.Yaw, weight);
            lerpLightTargetParameters.Pitch = Mathf.Lerp(from.Pitch, to.Pitch, weight);
            lerpLightTargetParameters.Roll = Mathf.Lerp(from.Roll, to.Roll, weight);
            lerpLightTargetParameters.distance = Mathf.Lerp(from.distance, to.distance, weight);
            lerpLightTargetParameters.offset = Vector3.Lerp(from.offset, to.offset, weight);
            lerpLightTargetParameters.linkToCameraRotation = to.linkToCameraRotation;
            lerpLightTargetParameters.drawGizmo = to.drawGizmo;

            return lerpLightTargetParameters;
        }

        public static SunlightParameters LerpSunlightParameters(SunlightParameters from, SunlightParameters to, float weight)
        {
            var lerpSunlightParameters = new SunlightParameters();
            //Orientation
            lerpSunlightParameters.orientationParameters.lattitude = Mathf.Lerp(from.orientationParameters.lattitude, to.orientationParameters.lattitude, weight);
            lerpSunlightParameters.orientationParameters.yAxis = Mathf.Lerp(from.orientationParameters.yAxis, to.orientationParameters.yAxis, weight);
            lerpSunlightParameters.orientationParameters.timeOfDay = Mathf.Lerp(from.orientationParameters.timeOfDay, to.orientationParameters.timeOfDay, weight);

            lerpSunlightParameters.lightParameters = LerpLightParameters(from.lightParameters, to.lightParameters, weight);

            return lerpSunlightParameters;
        }

        public static void SetProceduralSkyboxParameters(ProceduralSkyboxParameters skyParameters)
        {
            var skyboxMaterial = RenderSettings.skybox;
            if(skyboxMaterial.shader.name == "Skybox/Procedural")
            {
                skyboxMaterial.SetFloat("_SunSize", skyParameters.sunSize);
                skyboxMaterial.SetFloat("_AtmosphereThickness", skyParameters.atmosphereThickness);
                skyboxMaterial.SetColor("_SkyTint", skyParameters.skyTint);
                skyboxMaterial.SetColor("_GroundColor", skyParameters.Ground);
                skyboxMaterial.SetFloat("_Exposure", skyParameters.exposure);
            }
            else
            {
                Debug.Log("Skybox material not using Procedural Skybox shader");
            }
        }

    }

#if UNITY_EDITOR
    public static class EditorLightingUtilities
    {
        public static void AssignSerializedProperty(SerializedProperty sp, object source)
        {
            var valueType = source.GetType();
            if (valueType.IsEnum)
            {
                sp.enumValueIndex = (int)source;
            }
            else if (valueType == typeof(Color))
            {
                sp.colorValue = (Color)source;
            }
            else if (valueType == typeof(float))
            {
                sp.floatValue = (float)source;
            }
            else if (valueType == typeof(Vector3))
            {
                sp.vector3Value = (Vector3)source;
            }
            else if (valueType == typeof(bool))
            {
                sp.boolValue = (bool)source;
            }
            else if (valueType == typeof(string))
            {
                sp.stringValue = (string)source;
            }
            else if (typeof(int).IsAssignableFrom(valueType))
            {
                sp.intValue = (int)source;
            }
            else if (typeof(Object).IsAssignableFrom(valueType))
            {
                sp.objectReferenceValue = (Object)source;
            }
            else
            {
                Debug.LogError("Missing type : " + valueType);
            }
        }

        public static void DrawSpotlightGizmo(Light spotlight)
        {
            var flatRadiusAtRange = spotlight.range * Mathf.Tan(spotlight.spotAngle * Mathf.Deg2Rad * 0.5f);

            var vectorLineUp = Vector3.Normalize(spotlight.gameObject.transform.position + spotlight.gameObject.transform.forward * spotlight.range + spotlight.gameObject.transform.up * flatRadiusAtRange - spotlight.gameObject.transform.position);
            var vectorLineDown = Vector3.Normalize(spotlight.gameObject.transform.position + spotlight.gameObject.transform.forward * spotlight.range + spotlight.gameObject.transform.up * -flatRadiusAtRange - spotlight.gameObject.transform.position);
            var vectorLineRight = Vector3.Normalize(spotlight.gameObject.transform.position + spotlight.gameObject.transform.forward * spotlight.range + spotlight.gameObject.transform.right * flatRadiusAtRange - spotlight.gameObject.transform.position);
            var vectorLineLeft = Vector3.Normalize(spotlight.gameObject.transform.position + spotlight.gameObject.transform.forward * spotlight.range + spotlight.gameObject.transform.right * -flatRadiusAtRange - spotlight.gameObject.transform.position);

            var rangeDiscDistance = Mathf.Cos(Mathf.Deg2Rad * spotlight.spotAngle / 2) * spotlight.range;
            var rangeDiscRadius = spotlight.range * Mathf.Sin(spotlight.spotAngle * Mathf.Deg2Rad * 0.5f);
            var nearDiscDistance = Mathf.Cos(Mathf.Deg2Rad * spotlight.spotAngle / 2) * spotlight.shadowNearPlane;
            var nearDiscRadius = spotlight.shadowNearPlane * Mathf.Sin(spotlight.spotAngle * Mathf.Deg2Rad * 0.5f);

            //Draw Near Plane Disc
            if (spotlight.shadows != LightShadows.None) Handles.Disc(spotlight.gameObject.transform.rotation, spotlight.gameObject.transform.position + spotlight.gameObject.transform.forward * nearDiscDistance, spotlight.gameObject.transform.forward, nearDiscRadius, false, 1);
            //Draw Range disc
            Handles.Disc(spotlight.gameObject.transform.rotation, spotlight.gameObject.transform.position + spotlight.gameObject.transform.forward * rangeDiscDistance, spotlight.gameObject.transform.forward, rangeDiscRadius, false, 1);
            //Draw Lines

            Gizmos.DrawLine(spotlight.gameObject.transform.position, spotlight.gameObject.transform.position + vectorLineUp * spotlight.range);
            Gizmos.DrawLine(spotlight.gameObject.transform.position, spotlight.gameObject.transform.position + vectorLineDown * spotlight.range);
            Gizmos.DrawLine(spotlight.gameObject.transform.position, spotlight.gameObject.transform.position + vectorLineRight * spotlight.range);
            Gizmos.DrawLine(spotlight.gameObject.transform.position, spotlight.gameObject.transform.position + vectorLineLeft * spotlight.range);

            //Draw Range Arcs
            Handles.DrawWireArc(spotlight.gameObject.transform.position, spotlight.gameObject.transform.right, vectorLineUp, spotlight.spotAngle, spotlight.range);
            Handles.DrawWireArc(spotlight.gameObject.transform.position, spotlight.gameObject.transform.up, vectorLineLeft, spotlight.spotAngle, spotlight.range);
        }

        public static void DrawDirectionalLightGizmo(Transform directionalLightTransform)
        {
            var gizmoSize = 0.2f;
            Handles.Disc(directionalLightTransform.rotation, directionalLightTransform.position, directionalLightTransform.gameObject.transform.forward, gizmoSize, false, 1);
            Gizmos.DrawLine(directionalLightTransform.position, directionalLightTransform.position + directionalLightTransform.forward);
            Gizmos.DrawLine(directionalLightTransform.position + directionalLightTransform.up * gizmoSize, directionalLightTransform.position + directionalLightTransform.up * gizmoSize + directionalLightTransform.forward);
            Gizmos.DrawLine(directionalLightTransform.position + directionalLightTransform.up * -gizmoSize, directionalLightTransform.position + directionalLightTransform.up * -gizmoSize + directionalLightTransform.forward);
            Gizmos.DrawLine(directionalLightTransform.position + directionalLightTransform.right * gizmoSize, directionalLightTransform.position + directionalLightTransform.right * gizmoSize + directionalLightTransform.forward);
            Gizmos.DrawLine(directionalLightTransform.position + directionalLightTransform.right * -gizmoSize, directionalLightTransform.position + directionalLightTransform.right * -gizmoSize + directionalLightTransform.forward);
        }

        public static void DrawCross(Transform m_transform)
        {
            var gizmoSize = 0.25f;
            Gizmos.DrawLine(m_transform.position, m_transform.position + m_transform.TransformVector(m_transform.root.forward * gizmoSize / m_transform.localScale.z));
            Gizmos.DrawLine(m_transform.position, m_transform.position + m_transform.TransformVector(m_transform.root.forward * -gizmoSize / m_transform.localScale.z));
            Gizmos.DrawLine(m_transform.position, m_transform.position + m_transform.TransformVector(m_transform.root.up * gizmoSize / m_transform.localScale.y));
            Gizmos.DrawLine(m_transform.position, m_transform.position + m_transform.TransformVector(m_transform.root.up * -gizmoSize / m_transform.localScale.y));
            Gizmos.DrawLine(m_transform.position, m_transform.position + m_transform.TransformVector(m_transform.root.right * gizmoSize / m_transform.localScale.x));
            Gizmos.DrawLine(m_transform.position, m_transform.position + m_transform.TransformVector(m_transform.root.right * -gizmoSize / m_transform.localScale.x));
        }

        public static bool DrawHeader(string title, bool activeField)
        {
            var backgroundRect = GUILayoutUtility.GetRect(1f, 17f);

            var labelRect = backgroundRect;
            labelRect.xMin += 16f;
            labelRect.xMax -= 20f;

            var toggleRect = backgroundRect;
            toggleRect.y += 2f;
            toggleRect.width = 13f;
            toggleRect.height = 13f;

            // Background rect should be full-width
            backgroundRect.xMin = 0f;
            backgroundRect.width += 4f;

            // Background
            float backgroundTint = EditorGUIUtility.isProSkin ? 0.1f : 1f;
            EditorGUI.DrawRect(backgroundRect, new Color(backgroundTint, backgroundTint, backgroundTint, 0.2f));

            // Title
            using (new EditorGUI.DisabledScope(!activeField))
                EditorGUI.LabelField(labelRect, title, EditorStyles.boldLabel);

            // Active checkbox
            activeField = GUI.Toggle(toggleRect, activeField, GUIContent.none, new GUIStyle("ShurikenCheckMark"));

            var e = Event.current;
            if (e.type == EventType.MouseDown && backgroundRect.Contains(e.mousePosition) && e.button == 0)
            {
                activeField = !activeField;
                e.Use();
            }

            EditorGUILayout.Space();

            return activeField;
        }

        public static void DrawHeader(string title)
        {
            var backgroundRect = GUILayoutUtility.GetRect(1f, 17f);

            var labelRect = backgroundRect;
            labelRect.xMin += 16f;
            labelRect.xMax -= 20f;

            var foldoutRect = backgroundRect;
            foldoutRect.y += 1f;
            foldoutRect.width = 13f;
            foldoutRect.height = 13f;

            // Background rect should be full-width
            backgroundRect.xMin = 0f;
            backgroundRect.width += 4f;

            // Background
            float backgroundTint = EditorGUIUtility.isProSkin ? 0.1f : 1f;
            EditorGUI.DrawRect(backgroundRect, new Color(backgroundTint, backgroundTint, backgroundTint, 0.2f));

            // Title
            EditorGUI.LabelField(labelRect, title, EditorStyles.boldLabel);
            EditorGUILayout.Space();
        }

        public static void DrawSplitter()
        {
            EditorGUILayout.Space();
            var rect = GUILayoutUtility.GetRect(1f, 1f);

            // Splitter rect should be full-width
            rect.xMin = 0f;
            rect.width += 4f;

            if (Event.current.type != EventType.Repaint)
                return;

            EditorGUI.DrawRect(rect, !EditorGUIUtility.isProSkin
                ? new Color(0.6f, 0.6f, 0.6f, 1.333f)
                : new Color(0.12f, 0.12f, 0.12f, 1.333f));
        }

        public static bool DrawHeaderFoldout(string title, bool state)
        {
            var backgroundRect = GUILayoutUtility.GetRect(1f, 17f);

            var labelRect = backgroundRect;
            labelRect.xMin += 16f;
            labelRect.xMax -= 20f;

            var foldoutRect = backgroundRect;
            foldoutRect.y += 1f;
            foldoutRect.width = 13f;
            foldoutRect.height = 13f;

            // Background rect should be full-width
            backgroundRect.xMin = 0f;
            backgroundRect.width += 4f;

            // Background
            float backgroundTint = EditorGUIUtility.isProSkin ? 0.1f : 1f;
            EditorGUI.DrawRect(backgroundRect, new Color(backgroundTint, backgroundTint, backgroundTint, 0.2f));

            // Title
            EditorGUI.LabelField(labelRect, title, EditorStyles.boldLabel);

            // Active checkbox
            state = GUI.Toggle(foldoutRect, state, GUIContent.none, EditorStyles.foldout);

            var e = Event.current;
            if (e.type == EventType.MouseDown && backgroundRect.Contains(e.mousePosition) && e.button == 0)
            {
                state = !state;
                e.Use();
            }

            return state;
        }
    }
#endif
}