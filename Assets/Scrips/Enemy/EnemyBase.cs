using System;
using Tools;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBase : MonoBehaviour
{
    public EnemyData EnemyData;


    public Transform Center;
    public float CurrentHealth;
    public bool isDeath;

    private UI_EnemyUI_Base _enemyUIBase;

    private void Awake()
    {
        isDeath = false;
        _enemyUIBase = GetComponentInChildren<UI_EnemyUI_Base>();
        _enemyUIBase.EnemyUIRegister(this);
    }

    private void OnEnable()
    {
        CurrentHealth = EnemyData.MaxHealth;
        EnemyManager.GetInstance().RegisterEnemy(this);
    }

    private void OnDestroy()
    {
        EnemyManager.GetInstance().UnRegisterEnemy(this);
    }

    public void TakeDamage(float dmg ,EnumTools.DamageKind damageKind ,Vector3 position)
    {
        if (isDeath)
        {
            return;
        }
        
        
        CurrentHealth -= dmg;
        _enemyUIBase.UpdateUI();
        DamageNumberManager.GetInstance().GetDamageNumberThroughDMGKind(damageKind)
            .Spawn(position, dmg, this.transform);
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        isDeath = true;
    }

    public float GetHealthPercent()
    {
        return CurrentHealth / EnemyData.MaxHealth;
    }
}
