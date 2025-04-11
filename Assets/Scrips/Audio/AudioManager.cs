using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : Singleton<AudioManager>
{
    public GameObject audioSourcePrefab;

    public void PlaySound(AudioClip clip, Vector3 position, bool loop, float MaxDistance, UnityAction endAction)
    {
        if (clip == null || audioSourcePrefab == null)
        {
            Debug.LogError("缺少音效剪辑或 AudioSourcePrefab！");
            return;
        }

        AudioInstance audioObject = Instantiate(audioSourcePrefab, position, Quaternion.identity).GetComponent<AudioInstance>();
        audioObject.Init(clip, loop, MaxDistance, endAction);
    }

    public void PlaySound(AudioClip clip, Vector3 position, bool loop, float MaxDistance)
    {
        if (clip == null || audioSourcePrefab == null)
        {
            Debug.LogError("缺少音效剪辑或 AudioSourcePrefab！");
            return;
        }

        AudioInstance audioObject = Instantiate(audioSourcePrefab, position, Quaternion.identity).GetComponent<AudioInstance>();
        audioObject.Init(clip, loop, MaxDistance);
    }

    public void PlayBGSound(AudioClip clip)
    {
        if (clip == null || audioSourcePrefab == null)
        {
            Debug.LogError("缺少音效剪辑或 AudioSourcePrefab！");
            return;
        }

        AudioInstance audioObject = Instantiate(audioSourcePrefab, Vector3.zero, Quaternion.identity).GetComponent<AudioInstance>();
        audioObject.Init(clip);
    }

    public GameObject PlayBGSoundAndReturn(AudioClip clip)
    {
        if (clip == null || audioSourcePrefab == null)
        {
            Debug.LogError("缺少音效剪辑或 AudioSourcePrefab！");
            return null;
        }

        GameObject audioGO = Instantiate(audioSourcePrefab, Vector3.zero, Quaternion.identity);
        AudioInstance audioObject = audioGO.GetComponent<AudioInstance>();
        audioObject.Init(clip); // 你原有的 Init(clip) 方法播放 BGM
        return audioGO;
    }
}
