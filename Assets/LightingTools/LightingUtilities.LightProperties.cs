using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.HDPipeline;
#if UNITY_EDITOR
using UnityEditor;
#endif

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

        public LightParameters(LightType specificType, LightmapPresetBakeType specificBakeMode, bool isNeutral)
        {
            if (isNeutral)
            {
                range = 0;
                intensity = 0;
                colorFilter = Color.black;
                indirectIntensity = 0;
                lightAngle = 0;
                innerSpotPercent = 0;
                cookieSize = 0;
                ShadowNearClip = 0;
                shadowStrength = 0;
                viewBiasMin = 0;
                viewBiasScale = 0;
                normalBias = 0;
                maxSmoothness = 0;
                fadeDistance = 0;
                shadowFadeDistance = 0;
                shadowResolution = 0;
            }
            type = specificType;
            mode = specificBakeMode;
        }

        public static LightParameters DeepCopy(LightParameters c)
        {
            LightParameters temp = new LightParameters();
            temp.type = c.type;
            temp.mode = c.mode;
            temp.range = c.range;
            temp.intensity = c.intensity;
            temp.colorFilter = c.colorFilter;
            temp.indirectIntensity = c.indirectIntensity;
            temp.lightAngle = c.lightAngle;
            temp.shadows = c.shadows;
            temp.shadowQuality = c.shadowQuality;
            temp.ShadowNearClip = c.ShadowNearClip;
            temp.viewBiasMin = c.viewBiasMin;
            temp.viewBiasScale = c.viewBiasScale;
            temp.normalBias = c.normalBias;
            temp.lightCookie = c.lightCookie;
            temp.cookieSize = c.cookieSize;
            temp.innerSpotPercent = c.innerSpotPercent;
            temp.length = c.length;
            temp.width = c.width;
            temp.fadeDistance = c.fadeDistance;
            temp.shadowFadeDistance = c.shadowFadeDistance;
            temp.affectDiffuse = c.affectDiffuse;
            temp.affectSpecular = c.affectSpecular;
            temp.shadowStrength = c.shadowStrength;
            temp.cullingMask = c.cullingMask;
            temp.maxSmoothness = c.maxSmoothness;
            temp.shadowResolution = c.shadowResolution;
            temp.contactShadows = c.contactShadows;
            return temp;
        }

        public static LightParameters operator +(LightParameters x, LightParameters y)
        {
            var addition = new LightParameters
            {
                intensity = x.intensity + y.intensity,
                range = x.range + y.range,
                colorFilter = x.colorFilter + y.colorFilter,
                colorTemperature = x.colorTemperature + y.colorTemperature,
                indirectIntensity = x.indirectIntensity + y.indirectIntensity,
                lightAngle = x.lightAngle + y.lightAngle,
                innerSpotPercent = x.innerSpotPercent + y.innerSpotPercent,
                maxSmoothness = x.maxSmoothness + y.maxSmoothness,
                shadowStrength = x.shadowStrength + y.shadowStrength,
                shadowResolution = x.shadowResolution + y.shadowResolution,
                useColorTemperature = x.useColorTemperature || y.useColorTemperature ? true : false,
                shadows = x.shadows || y.shadows ? true : false,
                affectDiffuse = x.affectDiffuse || y.affectDiffuse ? true : false,
                affectSpecular = x.affectSpecular || y.affectSpecular ? true : false,
                contactShadows = x.contactShadows || y.contactShadows ? true : false,
                ShadowNearClip = x.ShadowNearClip + y.ShadowNearClip,
                viewBiasMin = x.viewBiasMin + y.viewBiasMin,
                viewBiasScale = x.viewBiasScale + y.viewBiasScale,
                normalBias = x.normalBias + y.normalBias,
                cookieSize = x.cookieSize + y.cookieSize,
                length = x.length + y.length,
                width = x.width + y.width,
                fadeDistance = x.fadeDistance + y.fadeDistance,
                shadowFadeDistance = x.shadowFadeDistance + y.shadowFadeDistance,
                shadowMaxDistance = x.shadowMaxDistance + y.shadowMaxDistance
            };
            return addition;
        }

        public LightType type = LightType.Point;
        public LightmapPresetBakeType mode = LightmapPresetBakeType.Mixed;
        public float range = 8;
        public bool useColorTemperature;
        public float colorTemperature = 6500;
        public Color colorFilter = Color.white;
        public float intensity = 1;
        public float indirectIntensity = 1;
        [Range(0, 180)]
        public float lightAngle = 45;
        public bool shadows = true;
        public ShadowQuality shadowQuality = ShadowQuality.Medium;
        [Range(0.01f, 10f)]
        public float ShadowNearClip = 0.1f;
        public float viewBiasMin = 0.2f;
        public float viewBiasScale = 1.0f;
        public float normalBias = 0.2f;
        public Texture lightCookie;
        public float cookieSize = 5;
        [Range(0f, 100f)]
        public float innerSpotPercent = 40;
        public float length;
        public float width;
        public float fadeDistance = 50;
        public float shadowFadeDistance = 10;
        public bool affectDiffuse = true;
        public bool affectSpecular = true;
        [Range(0, 1)]
        public float shadowStrength = 1;
        public float shadowMaxDistance = 150;
        public LayerMask cullingMask = -1;
        [Range(0, 1)]
        public float maxSmoothness = 1;
        public int shadowResolution = 128;
        public bool contactShadows = false;
    }

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
            if (lightParameters.shadows)
                light.shadows = LightShadows.Soft;
            else
                light.shadows = LightShadows.None;
            light.shadowStrength = 1;
            light.shadowNearPlane = lightParameters.ShadowNearClip;
            light.color = lightParameters.colorFilter;
            light.range = lightParameters.range;
            light.spotAngle = lightParameters.lightAngle;
            light.cookie = lightParameters.lightCookie;
            light.cullingMask = lightParameters.cullingMask;

            additionalLightData.intensity = lightParameters.intensity;
            additionalLightData.affectDiffuse = lightParameters.affectDiffuse;
            additionalLightData.affectSpecular = lightParameters.affectSpecular;
            additionalLightData.maxSmoothness = lightParameters.maxSmoothness;
            additionalLightData.fadeDistance = lightParameters.fadeDistance;
            additionalLightData.m_InnerSpotPercent = lightParameters.innerSpotPercent;

            additionalShadowData.shadowFadeDistance = lightParameters.shadowMaxDistance;
            additionalShadowData.shadowResolution = lightParameters.shadowResolution;
            additionalShadowData.shadowDimmer = lightParameters.shadowStrength;
            additionalShadowData.viewBiasMin = lightParameters.viewBiasMin;
            additionalShadowData.viewBiasScale = lightParameters.viewBiasScale;
            additionalShadowData.normalBiasMin = lightParameters.normalBias;
            additionalShadowData.normalBiasMax = lightParameters.normalBias;
            additionalShadowData.shadowDimmer = lightParameters.shadowStrength;
            additionalShadowData.contactShadows = lightParameters.contactShadows;
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
            lerpLightParameters.maxSmoothness = Mathf.Lerp(from.maxSmoothness, to.maxSmoothness, weight);
            lerpLightParameters.innerSpotPercent = Mathf.Lerp(from.innerSpotPercent, to.innerSpotPercent, weight);

            if (from.shadows == false && to.shadows == false)
            {
                lerpLightParameters.shadows = false;
            }
            else
            {
                lerpLightParameters.shadows = true;
            }

            lerpLightParameters.lightCookie = weight > 0.5f ? to.lightCookie : from.lightCookie;
            lerpLightParameters.shadowStrength = Mathf.Lerp(from.shadowStrength, to.shadowStrength, weight);
            lerpLightParameters.viewBiasMin = Mathf.Lerp(from.viewBiasMin, to.viewBiasMin, weight);
            lerpLightParameters.viewBiasScale = Mathf.Lerp(from.viewBiasScale, to.viewBiasScale, weight);
            lerpLightParameters.normalBias = Mathf.Lerp(from.normalBias, to.normalBias, weight);
            lerpLightParameters.ShadowNearClip = Mathf.Lerp(from.ShadowNearClip, to.ShadowNearClip, weight);
            lerpLightParameters.shadowResolution = (int)Mathf.Lerp(from.shadowResolution, to.shadowResolution, weight);

            lerpLightParameters.affectDiffuse = weight > 0.5f ? to.affectDiffuse : from.affectDiffuse;
            lerpLightParameters.affectSpecular = weight > 0.5f ? to.affectSpecular : from.affectSpecular;

            lerpLightParameters.cullingMask = weight > 0.5f ? to.cullingMask : from.cullingMask;
            lerpLightParameters.shadowQuality = weight > 0.5f ? to.shadowQuality : from.shadowQuality;
            lerpLightParameters.contactShadows = weight > 0.5f ? to.contactShadows : from.contactShadows;

            return lerpLightParameters;
        }
    }
}
