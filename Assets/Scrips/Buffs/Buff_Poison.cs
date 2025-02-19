using Tools;

namespace Scrips.Buffs
{
    public class Buff_Poison: BuffBase
    {
        private IHurtAble _hurtAble;
        private int currentDuration;
        private IBuffAble _buffAble;
        
        
        public override void OnBuffApplied(IHurtAble hurtAble, IBuffAble buffAble)
        {
            _hurtAble = hurtAble;
            _buffAble = buffAble;
            currentDuration = (int) PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Buff_Poison_Duration);
            currentLayer = 1;
        }

        public override void UpdateBuff()
        {
            _hurtAble.TakeDamage(
                PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Buff_Poison_DmgPercentage) * 
                _hurtAble.GetMaxHealth() * currentLayer,
                EnumTools.DamageKind.Poison,
                _hurtAble.GetCenter());
            currentDuration--;
            if (currentDuration <=0)
            {
                _buffAble.RemoveBuff(BuffName);
            }
        }

        public override void OnBuffEnd()
        {
            
        }
    }
}