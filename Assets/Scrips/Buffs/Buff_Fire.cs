using Tools;

namespace Scrips.Buffs
{
    public class Buff_Fire : BuffBase
    {
        private IHurtAble _hurtAble;
        private int currentDuration;
        private IBuffAble _buffAble;
        public override void OnBuffApplied(IHurtAble hurtAble, IBuffAble buffAble)
        {
            _hurtAble = hurtAble;
            _buffAble = buffAble;
            maxLayer = (int)PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Buff_Fire_MaxLayer);
            currentDuration = (int) PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Buff_Fire_Duration);
            currentLayer++;
            if (currentLayer > maxLayer)
            {
                currentLayer = maxLayer;
            }
        }

        public override void UpdateBuff()
        {
            _hurtAble.TakeDamage(
                PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Buff_Fire_DmgPercentage) * 
                PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Attack) * currentLayer,
                EnumTools.DamageKind.Fire,
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