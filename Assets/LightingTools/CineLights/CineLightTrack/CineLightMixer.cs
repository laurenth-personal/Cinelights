using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using System.Collections.Generic;
using LightUtilities;

public class CineLightMixer : PlayableBehaviour {

    // Called each frame the mixer is active, after inputs are processed

    [HideInInspector][SerializeField]
    public GameObject lightTargetGO;
    public Transform lightTransform;
    public Transform lightPitchTransform;
    public Transform lightYawTransform;
    [HideInInspector]
    [SerializeField]
    public Light light;
    private bool isFading;
    private bool globalUseShadowCaster = false;
    public Vector3 attachmentPosition;

    public override void OnGraphStart(Playable playable)
    {
        if (lightTargetGO == null)
        {
            lightTargetGO = new GameObject("LightTargetTimeline", typeof(CineLight));
            var cineLight = lightTargetGO.GetComponent<CineLight>();
        }

        else
            lightTargetGO.SetActive(true);

        base.OnGraphStart(playable);
    }

    public override void OnGraphStop(Playable playable)
    {
        if (lightTargetGO != null)
            GameObject.DestroyImmediate(lightTargetGO);
        base.OnGraphStop(playable);
    }

    public override void ProcessFrame(Playable handle, FrameData info, object playerData) {
        var count = handle.GetInputCount();

        var cineLight = lightTargetGO.GetComponent<CineLight>();

        GameObject attachmentTransform = playerData as GameObject;

        if (attachmentTransform != null)
        {
            attachmentPosition = attachmentTransform.transform.position;
            cineLight.SetAttachmentTransform(attachmentTransform, true);
        }

        lightTargetGO.transform.position = attachmentPosition;

        light = cineLight.GetComponentInChildren<Light>();

        var neutralLightParameters = new LightParameters();
        var mixedLightParameters = new LightParameters();
        neutralLightParameters.type = mixedLightParameters.type = LightType.Spot;
        neutralLightParameters.intensity = mixedLightParameters.intensity = 0;
        neutralLightParameters.indirectIntensity = mixedLightParameters.indirectIntensity = 0;
        neutralLightParameters.colorFilter = mixedLightParameters.colorFilter = Color.black;
        neutralLightParameters.range = mixedLightParameters.range = 0;
        neutralLightParameters.lightAngle = mixedLightParameters.lightAngle = 0;
        neutralLightParameters.shadowBias = mixedLightParameters.shadowBias = 0;
        neutralLightParameters.shadowNormalBias = mixedLightParameters.shadowNormalBias = 0;
        neutralLightParameters.ShadowNearClip = mixedLightParameters.ShadowNearClip = 0;

        CineLightParameters neutralCineLightParameters = new CineLightParameters();
        neutralCineLightParameters.Yaw = 0;
        neutralCineLightParameters.Pitch = 0;
        neutralCineLightParameters.Roll = 0;
        neutralCineLightParameters.offset = Vector3.zero;
        neutralCineLightParameters.distance = 0;
        CineLightParameters mixedCineLightParameters = new CineLightParameters();
        mixedCineLightParameters.Yaw = 0;
        mixedCineLightParameters.Pitch = 0;
        mixedCineLightParameters.Roll = 0;
        mixedCineLightParameters.offset = Vector3.zero;
        mixedCineLightParameters.distance = 0;

        ShadowCasterParameters mixedShadowCasterParameters = new ShadowCasterParameters();
        mixedShadowCasterParameters.shadowCasterDistance = 0;
        mixedShadowCasterParameters.shadowCasterOffset = Vector2.zero;
        mixedShadowCasterParameters.shadowCasterSize = Vector2.zero;
        mixedShadowCasterParameters.useShadowCaster = false;

        globalUseShadowCaster = false;

        List<float> inputWeights = new List<float>();

        for (var i = 0; i < count; i++)
        {
            float weight = handle.GetInputWeight(i);
            if (weight > 0)
                inputWeights.Add(weight);
            if (inputWeights.Count > 2)
                break;
        }

        if (inputWeights.Count == 1)
            isFading = true;
        else
            isFading = false;

        for (var i = 0; i < count; i++)
        {

            var inputHandle = handle.GetInput(i);
            var weight = handle.GetInputWeight(i);

            if (inputHandle.IsValid() &&
                inputHandle.GetPlayState() == PlayState.Playing &&
                weight > 0)
            {
                var data = ((ScriptPlayable<CineLightClipPlayable>)inputHandle).GetBehaviour();
                if (data != null)
                {
                    mixedCineLightParameters.Yaw += Mathf.Lerp(neutralCineLightParameters.Yaw, data.cinelightParameters.Yaw, isFading ? 1 : weight);
                    mixedCineLightParameters.Pitch += Mathf.Lerp(neutralCineLightParameters.Pitch, data.cinelightParameters.Pitch, isFading ? 1 : weight);
                    mixedCineLightParameters.Roll += Mathf.Lerp(neutralCineLightParameters.Roll, data.cinelightParameters.Roll, isFading ? 1 : weight);
                    mixedCineLightParameters.distance += Mathf.Lerp(neutralCineLightParameters.distance, data.cinelightParameters.distance, isFading ? 1 : weight);
                    mixedCineLightParameters.offset += Vector3.Lerp(neutralCineLightParameters.offset, data.cinelightParameters.offset, isFading ? 1 : weight);
                    mixedCineLightParameters.linkToCameraRotation = data.cinelightParameters.linkToCameraRotation;
                    if(weight>0.5f)
                    {
                        cineLight.drawGizmo = data.cinelightParameters.drawGizmo;
                    }

                    mixedLightParameters.intensity += LightEditorUtilities.LerpLightParameters(neutralLightParameters, data.lightParameters, weight).intensity;
                    mixedLightParameters.range += LightEditorUtilities.LerpLightParameters(neutralLightParameters, data.lightParameters, isFading ? 1 : weight).range;
                    mixedLightParameters.colorFilter += LightEditorUtilities.LerpLightParameters(neutralLightParameters, data.lightParameters, isFading ? 1 : weight).colorFilter;
                    mixedLightParameters.lightAngle += LightEditorUtilities.LerpLightParameters(neutralLightParameters, data.lightParameters, isFading ? 1 : weight).lightAngle;
                    if(weight>0.5f)
                    {
                        mixedLightParameters.shadows = data.lightParameters.shadows;
                        mixedLightParameters.cullingMask = data.lightParameters.cullingMask;
                        mixedLightParameters.shadowQuality = data.lightParameters.shadowQuality;
                        mixedLightParameters.shadows = data.lightParameters.shadows;
                    }
                    mixedLightParameters.shadowNormalBias += LightEditorUtilities.LerpLightParameters(neutralLightParameters, data.lightParameters, isFading ? 1 : weight).shadowNormalBias;
                    mixedLightParameters.ShadowNearClip += LightEditorUtilities.LerpLightParameters(neutralLightParameters, data.lightParameters, isFading ? 1 : weight).ShadowNearClip;
                    mixedLightParameters.shadowBias += LightEditorUtilities.LerpLightParameters(neutralLightParameters, data.lightParameters, isFading ? 1 : weight).shadowBias;

                    mixedShadowCasterParameters.shadowCasterDistance += Mathf.Lerp(0, data.shadowCasterParameters.shadowCasterDistance, isFading ? 1 : weight);
                    mixedShadowCasterParameters.shadowCasterOffset += Vector2.Lerp(Vector2.zero, data.shadowCasterParameters.shadowCasterOffset, isFading ? 1 : weight);
                    mixedShadowCasterParameters.shadowCasterSize += Vector2.Lerp(Vector2.zero, data.shadowCasterParameters.shadowCasterSize, isFading ? 1 : weight);
                    if (data.shadowCasterParameters.useShadowCaster == true)
                        globalUseShadowCaster = true;
                    if ( weight > 0.5 || isFading)
                        mixedShadowCasterParameters.useShadowCaster = data.shadowCasterParameters.useShadowCaster;
                }
            }
        }
        LightEditorUtilities.ApplyLightParameters(light, mixedLightParameters);
        LightEditorUtilities.ApplyCineLightParameters(cineLight, mixedCineLightParameters);

        if (globalUseShadowCaster && cineLight.shadowCasterGO == null)
        {
            cineLight.useShadowCaster = true;
            cineLight.ApplyShadowCaster();
        }

        if (cineLight.shadowCasterGO != null)
        {
            cineLight.shadowCasterGO.GetComponent<MeshRenderer>().enabled = mixedShadowCasterParameters.useShadowCaster;
            cineLight.shadowCasterGO.transform.localScale = new Vector3(mixedShadowCasterParameters.shadowCasterSize.x, mixedShadowCasterParameters.shadowCasterSize.y, 1);
            cineLight.shadowCasterGO.transform.localPosition = new Vector3(mixedShadowCasterParameters.shadowCasterOffset.x, mixedShadowCasterParameters.shadowCasterOffset.y, -mixedShadowCasterParameters.shadowCasterDistance);
        }

        lightTargetGO.SetActive(mixedLightParameters.intensity == 0 ? false : true);
    }
}