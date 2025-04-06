using Tools;
using UnityEngine;

namespace Scrips.Buffs
{
    public class Buff_Frozen : BuffBase
    {
        private IHurtAble _hurtAble;
        private int currentDuration;
        private IBuffAble _buffAble;

        public override void OnBuffApplied(IHurtAble hurtAble, IBuffAble buffAble)
        {
            _hurtAble = hurtAble;
            _buffAble = buffAble;
            currentDuration = (int)PlayerStatsManager.GetInstance()
                .GetStatValue(EnumTools.PlayerStatType.Buff_Ice_FrozenDuration);
            hurtAble.Slow(0);
            _hurtAble.TakeDamage(
                PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Buff_Ice_DmgPercentage) * 
                PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Attack),
                EnumTools.DamageKind.Ice,
                _hurtAble.GetCenter());
            buffAble.AddBuffShader(EnumTools.BuffName.Frozen,true);
        }
        
        public override void UpdateBuff()
        {
            base.UpdateBuff();
            currentDuration--;
            if (currentDuration <=0)
            {
                _buffAble.RemoveBuff(BuffName);
            }
        }
        
        public override void OnBuffEnd(IHurtAble hurtAble, IBuffAble buffAble)
        {
            base.OnBuffEnd(hurtAble, buffAble);
            _hurtAble.Slow(-1);
            Debug.Log("remove");
            buffAble.AddBuffShader(EnumTools.BuffName.Frozen,false);
        }
    }
}