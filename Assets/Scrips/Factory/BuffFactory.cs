using System;
using System.Collections.Generic;
using Scrips.Buffs;
using Tools;
using UnityEngine;

namespace Scrips.Factory
{
    public static class BuffFactory
    {
        private static readonly Dictionary<EnumTools.BuffName, Type> buffRegistry = new Dictionary<EnumTools.BuffName, Type>
        {
            { EnumTools.BuffName.Fire, typeof(Buff_Fire) },
            { EnumTools.BuffName.Poison, typeof(Buff_Poison) },
            { EnumTools.BuffName.Ice , typeof(Buff_Ice)},
            { EnumTools.BuffName.Frozen , typeof(Buff_Frozen)},
        };

        public static BuffBase CreateBuff(EnumTools.BuffName buffName, GameObject root)
        {
            if (buffRegistry.TryGetValue(buffName, out Type buffType))
            {
                BuffBase buffBase =(BuffBase)root.AddComponent(buffType);
                buffBase.BuffName = buffName;
                return buffBase;
            }
            Debug.LogWarning($"未找到对应的 buff: {buffName}");
            return null;
        }
    }
}