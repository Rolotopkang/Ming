using System.Collections.Generic;
using Scrips.Effects;
using Tools;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletBase : MonoBehaviour
{
    public float AttackDmg;
    public float lifetime = 5f;
    public GameObject OnHitEffectPrefab;
    public GameObject OnHitEnemyEffectPrefab;
    public EnumTools.DamageKind DamageKind = EnumTools.DamageKind.Normal;
    private BulletEffectBase[] _bulletEffectBaseslist;
    
    public int PenetrationNum = 1;
    private bool _isCritical = false;
    public List<EnemyBase> hitEnemyList = new List<EnemyBase>();
    
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //ADD FORCE
        Rigidbody tmp_rb = other.GetComponent<Rigidbody>();
        if (tmp_rb != null)
        {
            Vector3 vl = GetComponent<Rigidbody>().linearVelocity;
            Vector3 forceToApply = vl * tmp_rb.mass;
            tmp_rb.AddForce(forceToApply, ForceMode.Impulse);
        }
        
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Instantiate(OnHitEffectPrefab, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
        

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("WeakPoint"))
        {
            EnemyBase enemyBase = other.transform.GetComponentInParent<EnemyBase>();
            if (hitEnemyList.Contains(enemyBase))
            {
                return;
            }
            
            hitEnemyList.Add(enemyBase);
            PenetrationNum--;
            Instantiate(OnHitEnemyEffectPrefab, transform.position, quaternion.identity);
            //EVENT
            EventCenter.Publish(EnumTools.GameEvent.BulletHit,new Dictionary<string, object>
            {
                {"BulletBase",this},
                {"EnemyBase",enemyBase}
            });
            
            
            if (CheckCritical() || other.gameObject.CompareTag("WeakPoint"))
            {
                _isCritical = true;
                DamageKind = EnumTools.DamageKind.Critical;
            }
            
            TriggerAllBulletEffect(enemyBase,other.transform.position);
            
            float withoutCriticalDmg = 
                AttackDmg + 
                AttackDmg * PlayerStatsManager.GetInstance()
                .GetStatValue(EnumTools.PlayerStatType.AttackPercentage);

            //巨人杀手逻辑
            if (enemyBase.GetHealthPercent() > 0.9f)
            {
                withoutCriticalDmg = PlayerItemSlotManager.GetInstance().CheckEffect(EnumTools.EffectName.GiantKiller)
                    ? withoutCriticalDmg * (PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.GiantKillerPercentage)+1)
                    : withoutCriticalDmg;
            }
            
            
            enemyBase.TakeDamage(
                _isCritical? 
                    withoutCriticalDmg*PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.CriticalAmount) : 
                    withoutCriticalDmg,
                DamageKind,
                transform.position);

            if (PenetrationNum <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private bool CheckCritical()
    {
        return Random.value < PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Critical);
    }

    private void TriggerAllBulletEffect(EnemyBase enemyBase, Vector3 hitPoint)
    {
        _bulletEffectBaseslist = GetComponents<BulletEffectBase>();
        foreach (BulletEffectBase bulletEffectBase in _bulletEffectBaseslist)
        {
            bulletEffectBase.TriggerEffect(this,enemyBase, hitPoint);
        }
    }
}
