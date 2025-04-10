using Tools;

namespace Scrips.Buffs
{
    public interface IBuff
    {
        void OnBuffApplied(IHurtAble hurtAble, IBuffAble buffAble);
        void UpdateBuff();
        void OnBuffEnd(IHurtAble hurtAble, IBuffAble buffAble);
        EnumTools.BuffName  GetBuffName(); // 获取Buff名称
    }
}