using System.Collections;
using System.Collections.Generic;
using Tools;
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

    public GameObject PlaySoundReturn(AudioClip clip, Vector3 position, bool loop, float MaxDistance)
    {
        if (clip == null || audioSourcePrefab == null)
        {
            Debug.LogError("缺少音效剪辑或 AudioSourcePrefab！");
            return null;
        }

        GameObject obj = Instantiate(audioSourcePrefab, position, Quaternion.identity);
        AudioInstance audioObject = obj.GetComponent<AudioInstance>();
        audioObject.Init(clip, loop, MaxDistance);
        return obj;
    }

    public GameObject PlayBGSoundReturn(AudioClip clip)
    {
        if (clip == null || audioSourcePrefab == null)
        {
            Debug.LogError("缺少音效剪辑或 AudioSourcePrefab！");
            return null;
        }

        GameObject obj = Instantiate(audioSourcePrefab, Vector3.zero, Quaternion.identity);
        AudioInstance audioObject = obj.GetComponent<AudioInstance>();
        audioObject.Init(clip);
        return obj;
    }
}
