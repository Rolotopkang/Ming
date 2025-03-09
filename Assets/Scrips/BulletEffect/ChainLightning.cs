using Tools;
using UnityEngine;

namespace Scrips.Effects
{
    public class ChainLightning : BulletEffectBase
    {
        public override void TriggerEffect(BulletBase bulletBase, EnemyBase enemyBase , Vector3 hitPoint)
        {
            enemyBase.TakeDamage(
                PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Attack)* 
                PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.ChainLightningDmgPercentage)
                ,EnumTools.DamageKind.Electrical,enemyBase.Center.position);
            ChainLightningGenerator tmpGenerator = Instantiate(Resources.Load<GameObject>("Prefab/ChainLightningGenerator"), Vector3.zero,
                Quaternion.identity).GetComponent<ChainLightningGenerator>();
            tmpGenerator.Register(
                (int)PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.ChainLightningMaxCount),
                PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.ChainLightningRange),
                enemyBase);
            base.TriggerEffect(bulletBase, enemyBase, hitPoint);
            
            //TODO 决定是否只能触发一次
            Destroy(this);
        }
    }
}