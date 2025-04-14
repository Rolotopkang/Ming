using System.Collections.Generic;
using Tools;
using UnityEngine;
using static Tools.EnumTools;

public class BulletHitAndDeathSound : MonoBehaviour
{
    [Header("音效设置")]
    public AudioClip bulletHitSound;     // 子弹命中音效
    public AudioClip enemyDeathSound;    // 怪物死亡音效
    public float volume = 1f;
    public float maxDistance = 15f;

    private void OnEnable()
    {
        EventCenter.Subscribe(EnumTools.GameEvent.BulletHit, OnBulletHit);
        EventCenter.Subscribe(EnumTools.GameEvent.EnemyKilled, OnEnemyKilled);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(EnumTools.GameEvent.BulletHit, OnBulletHit);
        EventCenter.Unsubscribe(EnumTools.GameEvent.EnemyKilled, OnEnemyKilled);
    }

    private void OnBulletHit(Dictionary<string, object> dictionary)
    {
        PlaySoundAtEventPosition(bulletHitSound, dictionary);
    }

    private void OnEnemyKilled(Dictionary<string, object> dictionary)
    {
        PlaySoundAtEventPosition(enemyDeathSound, dictionary);
    }

    private void PlaySoundAtEventPosition(AudioClip clip, Dictionary<string, object> dictionary)
    {
        if (clip == null)
        {
            Debug.LogWarning("未设置音效！");
            return;
        }

        if (dictionary != null && dictionary.TryGetValue("Bulletbase", out object posObj) && posObj is Vector3 hitPos)
        {
            AudioManager.GetInstance().PlaySound(clip, hitPos, false, maxDistance, null);
        }
        else
        {
            Debug.LogWarning("找不到位置");
            AudioManager.GetInstance().PlaySound(clip, transform.position, false, maxDistance, null);
        }
    }
}