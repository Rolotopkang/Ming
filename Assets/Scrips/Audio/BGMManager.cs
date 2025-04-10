using System.Collections;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance { get; private set; }

    private GameObject currentBGMObject;
    private AudioSource currentSource;
    private AudioClip currentClip;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public void EnterRoom(AudioClip newClip, float fadeDuration)
    {
        if (newClip == null || newClip == currentClip) return;

        // 先淡出旧音乐
        if (currentSource != null)
            StartCoroutine(FadeOutAndDestroy(currentBGMObject, currentSource, fadeDuration));

        // 播放新音乐
        currentBGMObject = AudioManager.GetInstance().PlayBGSoundAndReturn(newClip);
        currentSource = currentBGMObject.GetComponent<AudioSource>();
        currentClip = newClip;
        currentSource.volume = 0f;
        StartCoroutine(FadeIn(currentSource, fadeDuration));
    }

    public void ExitRoom(float fadeDuration)
    {
        if (currentSource != null)
        {
            StartCoroutine(FadeOutAndDestroy(currentBGMObject, currentSource, fadeDuration));
            currentBGMObject = null;
            currentSource = null;
            currentClip = null;
        }
    }

    IEnumerator FadeIn(AudioSource source, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            source.volume = Mathf.Lerp(0f, 1f, timer / duration);
            yield return null;
        }
    }

    IEnumerator FadeOutAndDestroy(GameObject obj, AudioSource source, float duration)
    {
        float startVol = source.volume;
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            source.volume = Mathf.Lerp(startVol, 0f, timer / duration);
            yield return null;
        }
        Destroy(obj);
    }
}
