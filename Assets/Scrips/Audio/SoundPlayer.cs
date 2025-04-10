using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip ambientClip;

    void Start()
    {
        if (ambientClip != null)
        {
            AudioManager.GetInstance().PlaySound(ambientClip, transform.position, true, 5f);
        }
    }
}