using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

[TrackColor(1.0f, 0.96f, 0.85f)]
[TrackClipType(typeof(CineLightClip))]
[TrackBindingType(typeof(GameObject))]
public class CineLightTrack : TrackAsset {

    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        foreach (var c in GetClips())
        {
            CineLightClip shot = (CineLightClip)c.asset;
            c.displayName = shot.lightTargetClip.cinelightParameters.displayName;
        }

        return ScriptPlayable<CineLightMixer>.Create(graph, inputCount);
    }
}