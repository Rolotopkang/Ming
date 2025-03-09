using Tools;

namespace Scrips.Buffs
{
    public class Buff_Ice : BuffBase
    {
        private IHurtAble _hurtAble;
        private int currentDuration;
        private IBuffAble _buffAble;
        public override void OnBuffApplied(IHurtAble hurtAble, IBuffAble buffAble)
        {
            _hurtAble = hurtAble;
            _buffAble = buffAble;
            
            maxLayer = (int)PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Buff_Ice_MaxLayer);
            currentDuration = (int) PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Buff_Ice_Duration);
            currentLayer++;
            if (currentLayer >= maxLayer)
            {
                _buffAble.RemoveBuff(BuffName);
                buffAble.AddNewBuff(EnumTools.BuffName.Frozen);
                return;
            }
            hurtAble.Slow(PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Buff_Ice_SlowPercentage)* currentLayer);
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

        public override void OnBuffEnd()
        {
            base.OnBuffEnd();
            _hurtAble.Slow(-1);
        }
    }
}