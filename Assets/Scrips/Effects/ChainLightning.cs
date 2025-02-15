using Tools;
using UnityEngine;

namespace Scrips.Effects
{
    public class ChainLightning : BulletEffectBase
    {
        public override void TriggerEffect(BulletBase bulletBase, EnemyBase enemyBase , Vector3 hitPoint)
        {
            enemyBase.TakeDamage(88,EnumTools.DamageKind.Electrical,hitPoint);
            base.TriggerEffect(bulletBase, enemyBase, hitPoint);
        }
    }
}