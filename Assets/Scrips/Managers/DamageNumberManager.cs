using System;
using System.Collections.Generic;
using DamageNumbersPro;
using Tools;
using UnityEngine;

[Serializable]
public class DamageNumberEntry
{
    public EnumTools.DamageKind damageKind;
    public DamageNumber damageNumberPrefab;
    
}
public class DamageNumberManager : Singleton<DamageNumberManager>
{
    [SerializeField]
    private List<DamageNumberEntry> damageNumberList = new List<DamageNumberEntry>();

    public DamageNumber GetDamageNumberThroughDMGKind(EnumTools.DamageKind damageKind)
    {
        foreach (DamageNumberEntry damageNumberEntry in damageNumberList)
        {
            if (damageNumberEntry.damageKind == damageKind)
            {
                return damageNumberEntry.damageNumberPrefab;
            }
        }

        Debug.LogError("找不到对应的伤害数字");
        return null;
    }
}
