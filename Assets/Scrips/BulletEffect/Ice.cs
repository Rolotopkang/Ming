using Scrips.Buffs;
using Tools;
using UnityEngine;

namespace Scrips.Effects
{
    public class Ice : BulletEffectBase
    {
        public override void TriggerEffect(BulletBase bulletBase, EnemyBase enemyBase, Vector3 hitPoint)
        {
            base.TriggerEffect(bulletBase, enemyBase, hitPoint);
            
            foreach (BuffBase buffBase in enemyBase.GetBuffList())
            {
                if (buffBase.BuffName == EnumTools.BuffName.Frozen)
                {
                    return;
                }
            }
            enemyBase.AddNewBuff(EnumTools.BuffName.Ice);
            
        }
    }
}