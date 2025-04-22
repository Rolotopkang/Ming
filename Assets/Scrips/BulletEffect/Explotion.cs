using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Scrips.Effects
{
    public class Explotion : BulletEffectBase
    {
        public override void TriggerEffect(BulletBase bulletBase, EnemyBase enemyBase, Vector3 hitPoint)
        {
            GameObject explotion = Resources.Load<GameObject>("ExplotionBullet");
            GameObject tmpExp = Instantiate(explotion, bulletBase.transform.position, Quaternion.identity);
            float scale =1.4f * (float)PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.ExplosionRange);
            tmpExp.transform.localScale = new Vector3(scale, scale, scale); 
            HashSet<EnemyBase> damagedEnemies = new HashSet<EnemyBase>();

            Collider[] hits = Physics.OverlapSphere(
                bulletBase.transform.position, 
                (float)PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.ExplosionRange)
            );

            Debug.Log(hits);

            foreach (Collider hit in hits)
            {
                if (hit.gameObject.CompareTag("Enemy"))
                {
                    EnemyBase enemy = hit.GetComponentInParent<EnemyBase>();
                    if (enemy != null && !damagedEnemies.Contains(enemy))
                    {
                        Debug.Log(hit.gameObject.name);

                        // 造成伤害
                        float damage = 
                            PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.ExplosionDmgPercentage) *
                            PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Attack);

                        enemy.TakeDamage(damage, EnumTools.DamageKind.Normal, enemy.GetCenter());

                        // 添加到已受伤集合
                        damagedEnemies.Add(enemy);
                    }
                }
            }
            base.TriggerEffect(bulletBase, enemyBase, hitPoint);
        }
    }
}