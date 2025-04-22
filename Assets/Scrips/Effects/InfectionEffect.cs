using System;
using System.Collections.Generic;
using NUnit.Framework;
using Scrips.Buffs;
using Tools;

namespace Scrips.Effects
{
    public class InfectionEffect : EffectBase
    {
        public override void ApplyEffect()
        {
            EventCenter.Subscribe(EnumTools.GameEvent.EnemyKilled,OnEnemyDeath);
            base.ApplyEffect();
        }

        public override void RemoveEffect()
        {
            EventCenter.Unsubscribe(EnumTools.GameEvent.EnemyKilled,OnEnemyDeath);
            base.RemoveEffect();
        }

        private void OnEnemyDeath(Dictionary<string, object> arg)
        {
            EnemyBase deathEnemy =(EnemyBase)arg["Enemy"];

            List<EnemyBase> tmplist = new List<EnemyBase>();
            tmplist.Add(deathEnemy);
            if (EnemyManager.GetInstance().GetClosestEnemy(deathEnemy.transform, 9999, tmplist))
            {
                EnemyBase target = EnemyManager.GetInstance().GetClosestEnemy(deathEnemy.transform, 9999, tmplist);
                List<BuffBase> tmp_bufflist = deathEnemy.GetBuffList();

                foreach (BuffBase buffBase in tmp_bufflist)
                {
                    target.AddNewBuff(buffBase.BuffName);
                }
            }

            

        }
    }
}