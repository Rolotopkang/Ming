using Tools;

namespace Scrips.Buffs
{
    public interface IBuff
    {
        void OnBuffApplied(IHurtAble hurtAble, IBuffAble buffAble);
        void UpdateBuff();
        void OnBuffEnd();
        EnumTools.BuffName  GetBuffName(); // 获取Buff名称
    }
}