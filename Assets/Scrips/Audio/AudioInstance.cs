using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioInstance : MonoBehaviour
{
    private AudioSource _source;
    private UnityAction PlayEnd;
    private bool isStart;

    public void Init(AudioClip clip)
    {
        _source = GetComponent<AudioSource>();
        _source.clip = clip;
        _source.loop = true;
        _source.spatialBlend = 0;
        _source.Play();
        isStart = true;
    }

    public void Init(AudioClip clip, bool isLoop, float maxDistance)
    {
        _source = GetComponent<AudioSource>();
        _source.clip = clip;
        _source.loop = isLoop;
        _source.maxDistance = maxDistance;
        _source.Play();
        isStart = true;
    }

    public void Init(AudioClip clip, bool isLoop, float maxDistance, UnityAction OnPlayEndTrigger)
    {
        _source = GetComponent<AudioSource>();
        _source.clip = clip;
        _source.loop = isLoop;
        _source.maxDistance = maxDistance;
        if (!isLoop)
        {
            PlayEnd = OnPlayEndTrigger;
        }

        _source.Play();
        isStart = true;
    }

    private void Update()
    {
        if (isStart)
        {
            if (!_source.isPlaying)
            {
                if (PlayEnd != null)
                {
                    PlayEnd.Invoke();
                }

                Destroy(gameObject);
            }
        }
    }
}
