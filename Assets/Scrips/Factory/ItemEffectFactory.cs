using System;
using System.Collections.Generic;
using Scrips.Effects;
using Tools;
using UnityEngine;

public static class ItemEffectFactory
{
    private static readonly Dictionary<EnumTools.EffectName, Type> effectRegistry = new Dictionary<EnumTools.EffectName, Type>
    {
        { EnumTools.EffectName.ShowTrajectoryLine, typeof(ShowTrajectoryLineEffect) },
        { EnumTools.EffectName.ExplosionBullet, typeof(ExplosionBulletEffect) },
        { EnumTools.EffectName.ChainLightningBullet, typeof(ChainLightningBulletEffect)},
        { EnumTools.EffectName.Fire, typeof(FireBulletEffect)},
        { EnumTools.EffectName.Poison, typeof(PoisonBulletEffect)},
        { EnumTools.EffectName.TracingBullets , typeof(TracingBulletEffect)},
        { EnumTools.EffectName.Ice , typeof(IceBulletEffect)},
        { EnumTools.EffectName.GiantKiller , typeof(GiantKillerEffect)},
        { EnumTools.EffectName.Infection , typeof(InfectionEffect)},
        
    };
    
    public static IItemEffect CreateEffect(EnumTools.EffectName effectName)
    {
        if (effectRegistry.TryGetValue(effectName, out Type effectType))
        {
            EffectBase effectBase = (EffectBase)PlayerEffectManager.GetInstance().gameObject.AddComponent(effectType);
            effectBase.EffectName = effectName;
            return effectBase;
        }
        
        Debug.LogWarning($"未找到对应的 Effect: {effectName}");
        return null;
    }
}