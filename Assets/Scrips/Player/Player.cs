using System;
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

    public void TakeDamage(float dmg, EnumTools.DamageKind damageKind, Vector3 position)
    {
        CurrentHP -= dmg;
        CheckDeath();
    }

    public void UseHealthBottle()
    {
        if (CurrentHealthBottleCount > 0)
        {
            //TODO 音效、特效
            
            Healing(HealthBottelHealingCount);
            CurrentHealthBottleCount--;
        }
    }

    public void Healing(float amount)
    {
        CurrentHP += amount;
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

    public void Death()
    {
        Debug.Log("你寄了");
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
