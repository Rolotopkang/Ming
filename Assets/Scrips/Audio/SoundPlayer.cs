using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip ambientClip;
    [Range(0f, 1f)] public float volume = 1f;

    void Start()
    {
        if (ambientClip != null)
        {
            AudioManager.GetInstance().PlaySound(ambientClip, transform.position, true, 1f, volume);
        }
    }
}