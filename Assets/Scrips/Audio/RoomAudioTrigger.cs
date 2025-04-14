using UnityEngine;

public class RoomAudioTrigger : MonoBehaviour
{
    public AudioClip ambientClip1;
    public AudioClip ambientClip2;
    public AudioClip bgmClip;

    public Transform ambient1Position;  // 环境音1的位置
    public Transform ambient2Position;  // 环境音2的位置

    private GameObject ambientInstance1;
    private GameObject ambientInstance2;
    private GameObject bgmInstance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("进入房间的是：" + other.name);
            // 播放环境音1（3D音效）
            ambientInstance1 = AudioManager.GetInstance().PlaySoundReturn(ambientClip1, ambient1Position.position, true, 15f);
            // 播放环境音2（3D音效）
            ambientInstance2 = AudioManager.GetInstance().PlaySoundReturn(ambientClip2, ambient2Position.position, true, 15f);
            // 播放背景音乐（2D音效）
            bgmInstance = AudioManager.GetInstance().PlayBGSoundReturn(bgmClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ambientInstance1) Destroy(ambientInstance1);
            if (ambientInstance2) Destroy(ambientInstance2);
            if (bgmInstance) Destroy(bgmInstance);
        }
    }
}