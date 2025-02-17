using System.Collections.Generic;
using Tools;
using UnityEngine;

public class PlayerStatsManager : Singleton<PlayerStatsManager>
{
    public Dictionary<EnumTools.PlayerStatType, float> baseStats = new Dictionary<EnumTools.PlayerStatType, float>
    {
        { EnumTools.PlayerStatType.Attack, 10f },
        { EnumTools.PlayerStatType.AttackPercentage, 1f},
        { EnumTools.PlayerStatType.Speed, 5f },
        { EnumTools.PlayerStatType.Critical ,0.5f},
        { EnumTools.PlayerStatType.CriticalAmount, 2f},
        { EnumTools.PlayerStatType.Health, 100f },
        { EnumTools.PlayerStatType.Armor, 0f },
        { EnumTools.PlayerStatType.BulletCount, 1f },
        { EnumTools.PlayerStatType.BulletPenetrationCount, 1f},
        { EnumTools.PlayerStatType.BulletSpread ,40 },
        { EnumTools.PlayerStatType.ChainLightningDmgPercentage, 0.2f},
        { EnumTools.PlayerStatType.ChainLightningMaxCount, 1},
        { EnumTools.PlayerStatType.ChainLightningRange, 5f},
    };

    private Dictionary<EnumTools.PlayerStatType, float> statModifiers = new Dictionary<EnumTools.PlayerStatType, float>();

    void Start()
    {
        foreach (var key in baseStats.Keys)
        {
            statModifiers[key] = 0f;
        }
    }

    // **应用道具加成**
    public void ApplyStatModifier(EnumTools.PlayerStatType statType, float value)
    {
        if (statModifiers.ContainsKey(statType))
        {
            statModifiers[statType] += value;
        }
    }

    // **移除道具加成**
    public void RemoveStatModifier(EnumTools.PlayerStatType statType, float value)
    {
        if (statModifiers.ContainsKey(statType))
        {
            statModifiers[statType] -= value;
        }
    }

    // **获取最终属性值**
    public float GetStatValue(EnumTools.PlayerStatType statType)
    {
        return baseStats[statType] + statModifiers[statType];
    }
}
