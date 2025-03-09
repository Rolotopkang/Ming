using Tools;
using UnityEngine;

namespace Scrips.Effects
{
    public class Poison: BulletEffectBase
    {
        public override void TriggerEffect(BulletBase bulletBase, EnemyBase enemyBase, Vector3 hitPoint)
        {
            enemyBase.AddNewBuff(EnumTools.BuffName.Poison);
            base.TriggerEffect(bulletBase, enemyBase, hitPoint);
        }
    }
}