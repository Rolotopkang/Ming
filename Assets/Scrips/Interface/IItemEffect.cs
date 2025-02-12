
using Tools;

public interface IItemEffect
{
    void ApplyEffect();   // 添加效果
    void RemoveEffect();  // 移除效果
    EnumTools.EffectName GetEffectName(); // 获取效果名称
}