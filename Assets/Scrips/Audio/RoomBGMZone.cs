using UnityEngine;

public class RoomBGMZone : MonoBehaviour
{
    public AudioClip bgmClip;
    [Range(0.5f, 5f)] public float fadeDuration = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BGMManager.Instance.EnterRoom(bgmClip, fadeDuration);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BGMManager.Instance.ExitRoom(fadeDuration);
        }
    }
}