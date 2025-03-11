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
        { EnumTools.PlayerStatType.Critical ,0.1f},
        { EnumTools.PlayerStatType.CriticalAmount, 2f},
        { EnumTools.PlayerStatType.Health, 200f },
        { EnumTools.PlayerStatType.Armor, 0f },
        { EnumTools.PlayerStatType.BulletCount, 1f },
        { EnumTools.PlayerStatType.BulletPenetrationCount, 1f},
        { EnumTools.PlayerStatType.BulletSpread ,40 },
        { EnumTools.PlayerStatType.ChainLightningDmgPercentage, 0},
        { EnumTools.PlayerStatType.ChainLightningMaxCount, 1},
        { EnumTools.PlayerStatType.ChainLightningRange, 0},
        { EnumTools.PlayerStatType.ExplosionRange, 0},
        { EnumTools.PlayerStatType.ExplosionDmgPercentage, 0},
        { EnumTools.PlayerStatType.Buff_Fire_MaxLayer , 0},
        { EnumTools.PlayerStatType.Buff_Fire_Duration, 0},
        { EnumTools.PlayerStatType.Buff_Fire_DmgPercentage, 0},
        { EnumTools.PlayerStatType.Buff_Poison_DmgPercentage, 0},
        { EnumTools.PlayerStatType.Buff_Poison_Duration, 0},
        { EnumTools.PlayerStatType.Buff_Ice_MaxLayer, 3},
        { EnumTools.PlayerStatType.Buff_Ice_SlowPercentage, 0.2f},
        { EnumTools.PlayerStatType.Buff_Ice_Duration, 5},
        { EnumTools.PlayerStatType.ItemTable_SlotNum , 4},
        { EnumTools.PlayerStatType.ShotPower , 20},
        { EnumTools.PlayerStatType.Buff_Ice_FrozenDuration, 2f},
        { EnumTools.PlayerStatType.Buff_Ice_DmgPercentage ,2.0f},
        { EnumTools.PlayerStatType.GiantKillerPercentage , 0},
        { EnumTools.PlayerStatType.HealthBottleNum ,3},
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
