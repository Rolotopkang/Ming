using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tools;
using Unity.Mathematics;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float AttackDmg;
    public float lifetime = 5f;
    public GameObject OnHitEffectPrefab;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Instantiate(OnHitEffectPrefab, transform.position, quaternion.identity);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemyBase = other.transform.GetComponentInParent<EnemyBase>();
            EventCenter.Publish(EnumTools.GameEvent.BulletHit,new Dictionary<string, object>(
                
                ));
            enemyBase.TakeDamage(AttackDmg,other.GetContact(0).point);
            Destroy(gameObject);
        }
    }
}
