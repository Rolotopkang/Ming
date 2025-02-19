using Tools;
using UnityEngine;

public class EffectBase : MonoBehaviour, IItemEffect
{
    public EnumTools.EffectName EffectName;
    public virtual void ApplyEffect()
    {
        
    }

    public virtual void RemoveEffect()
    {
        Destroy(this);
    }

    public EnumTools.EffectName GetEffectName()
    {
        return EffectName;
    }
}