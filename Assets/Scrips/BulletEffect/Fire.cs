using Tools;
using UnityEngine;

namespace Scrips.Effects
{
    public class Fire : BulletEffectBase
    {
        public override void TriggerEffect(BulletBase bulletBase, EnemyBase enemyBase, Vector3 hitPoint)
        {
            enemyBase.AddNewBuff(EnumTools.BuffName.Fire);
            base.TriggerEffect(bulletBase, enemyBase, hitPoint);
        }
    }
}