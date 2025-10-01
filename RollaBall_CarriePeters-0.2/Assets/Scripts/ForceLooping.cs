using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class InspectorVideoControls : MonoBehaviour {
    public VideoPlayer vp;

    void Reset() {
        if (vp == null) vp = GetComponent<VideoPlayer>();
    }

    [ContextMenu("Play Video")]
    public void PlayVideo() {
        if (vp == null) vp = GetComponent<VideoPlayer>();
        vp.Prepare();
        vp.prepareCompleted += (s) => s.Play();
    }

    [ContextMenu("Stop Video")]
    public void StopVideo() {
        if (vp == null) vp = GetComponent<VideoPlayer>();
        vp.Stop();
    }
}

