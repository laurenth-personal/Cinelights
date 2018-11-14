using UnityEngine;

namespace LightUtilities
{
    [System.Serializable]
    public class CineLightParameters
    {
        public CineLightParameters() { }

        public CineLightParameters(bool neutral)
        {
            Yaw = 0;
            Pitch = 0;
            Roll = 0;
            offset = Vector3.zero;
            distance = 0;
        }

        public static CineLightParameters DeepCopy(CineLightParameters c)
        {
            CineLightParameters temp = new CineLightParameters();
            temp.Yaw = c.Yaw;
            temp.Pitch = c.Pitch;
            temp.Roll = c.Roll;
            temp.offset = c.offset;
            temp.distance = c.distance;
            temp.linkToCameraRotation = c.linkToCameraRotation;
            temp.displayName = c.displayName;
            temp.drawGizmo = c.drawGizmo;
            return temp;
        }

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
        public ShadowCasterParameters() { }

        public ShadowCasterParameters(bool neutral)
        {
            useShadowCaster = false;
            shadowCasterSize = Vector2.zero;
            shadowCasterDistance = 0;
            shadowCasterOffset = Vector2.zero;
        }

        public static ShadowCasterParameters DeepCopy(ShadowCasterParameters c)
        {
            ShadowCasterParameters temp = new ShadowCasterParameters();
            temp.useShadowCaster = c.useShadowCaster;
            temp.shadowCasterSize = c.shadowCasterSize;
            temp.shadowCasterDistance = c.shadowCasterDistance;
            temp.shadowCasterOffset = c.shadowCasterOffset;
            return temp;
        }

        public bool useShadowCaster = false;
        public Vector2 shadowCasterSize = new Vector2(1, 1);
        public float shadowCasterDistance = 1;
        public Vector2 shadowCasterOffset = new Vector2(0, 0);
    }

    public static class CineLightUtilities
    {

        public static void ApplyCineLightParameters(CineLight light, CineLightParameters parameters)
        {
            light.offset = parameters.offset;
            light.LightParentYaw.transform.localPosition = parameters.offset;
            light.Yaw = parameters.Yaw;
            light.LightParentYaw.transform.localRotation = Quaternion.Euler(0, parameters.Yaw, 0);
            light.Pitch = parameters.Pitch;
            light.LightParentPitch.transform.localRotation = Quaternion.Euler(-parameters.Pitch, 0, 0);
            light.Roll = parameters.Roll;
            light.lightGO.transform.localRotation = Quaternion.Euler(0, 180, parameters.Roll + 180);
            light.distance = parameters.distance;
            light.lightGO.transform.localPosition = new Vector3(0, 0, parameters.distance);
            light.timelineSelected = parameters.drawGizmo;
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

    }
}
