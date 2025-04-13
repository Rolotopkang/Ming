using System;
using System.Collections.Generic;
using Scrips.Buffs;
using Tools;
using UnityEngine;

public class Player : Singleton<Player>,IHurtAble
{
    public float CurrentHP;
    public int CurrentHealthBottleCount;
    [SerializeField]
    private float HealthBottelHealingCount = 50;
    private void Start()
    {
        CurrentHP = PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Health);
        CurrentHealthBottleCount =(int)PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.HealthBottleNum);
    }

    public void ResetPlayer()
    {
        CurrentHP = PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Health);
        CurrentHealthBottleCount =(int)PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.HealthBottleNum);
    }

    public void toTryHit()
    {
        TakeDamage(10,EnumTools.DamageKind.Normal,Vector3.zero);
    }
    
    public void TakeDamage(float dmg, EnumTools.DamageKind damageKind, Vector3 position)
    {
        CurrentHP -= dmg;
        EventCenter.Publish(EnumTools.GameEvent.PlayerHit,new Dictionary<string, object>
        {
            {"amount",dmg}
        });
        CheckDeath();
    }

    public void UseHealthBottle()
    {
        Healing(HealthBottelHealingCount);
    }

    public void Healing(float amount)
    {
        CurrentHP += amount;
        EventCenter.Publish(EnumTools.GameEvent.PlayerHealth,new Dictionary<string, object>
        {
            {"amount",amount}
        });
        if (CurrentHP> GetMaxHealth())
        {
            CurrentHP = GetMaxHealth();
        }
    }

    public void CheckDeath()
    {
        if (CurrentHP<=0)
        {
            Death();
        }
    }

    public float GetHealthPercent()
    {
        return CurrentHP / GetMaxHealth();
    }

    public float GetMaxHealth()
    {
        return PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Health);
    }

    private void Update()
    {
        
        
    }

    public void Slow(float amount)
    {
    }

    public bool TryBuy(float amount)
    {
        if (PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Money)>= amount)
        {
            PlayerStatsManager.GetInstance().ApplyStatModifier(EnumTools.PlayerStatType.Money,-amount);
            return true;
        }

        return false;
    }

    public void Death()
    {
        Debug.Log("你寄了");
        EventCenter.Publish(EnumTools.GameEvent.PlayerDeath,null);
    }

    public Vector3 GetCenter()
    {
        return Vector3.zero;
    }
    
    public Transform GetRoot()
    {
        return transform.GetChild(0);
    }
}
