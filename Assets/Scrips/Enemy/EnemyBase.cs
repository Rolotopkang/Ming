using System;
using System.Collections.Generic;
using Scrips.Buffs;
using Scrips.Factory;
using Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EnemyBase : MonoBehaviour, IHurtAble , IBuffAble
{
    public EnemyData EnemyData;

    
    public Transform Center;
    public float CurrentHealth = 100;
    public bool isDeath;
    public UnityEvent OnDeath;

    private List<BuffBase> _buffBaseList = new List<BuffBase>();
    private UI_EnemyUI_Base _enemyUIBase;
    
    private Rigidbody[] ragdollRigidbodies;
    private Joint[] joints;
    private Animator animator;
    private CapsuleCollider[] _colliders;

    private void Awake()
    {
        isDeath = false;
        _enemyUIBase = GetComponentInChildren<UI_EnemyUI_Base>();
        _enemyUIBase.EnemyUIRegister(this);
        InitRagdoll();
    }

    private void InitRagdoll()
    {
        // 获取角色的 Animator
        animator = GetComponent<Animator>();

        // 获取所有子节点的 Rigidbody 和 Joint
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        joints = GetComponentsInChildren<Joint>();
        _colliders = GetComponentsInChildren<CapsuleCollider>();
    }

    private void Start()
    {
        SetRagdollActive(false);
        InvokeRepeating(nameof(UpdateSec), 0f,1f);
    }

    private void OnEnable()
    {
        CurrentHealth = EnemyData.MaxHealth;
        EnemyManager.GetInstance()?.RegisterEnemy(this);
    }

    private void OnDestroy()
    {
        EnemyManager.GetInstance()?.UnRegisterEnemy(this);
    }

    private void UpdateSec()
    {
        TriggerBuffs();
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

    public void TriggerBuffs()
    {
        for (int i = _buffBaseList.Count - 1; i >= 0; i--)
        {
            _buffBaseList[i].UpdateBuff();
        }
    }

    public void AddNewBuff(EnumTools.BuffName buffName)
    {
        foreach (BuffBase buffBase in _buffBaseList)
        {
            if (buffBase.GetBuffName() == buffName)
            {
                buffBase.OnBuffApplied(this,this);
                _enemyUIBase.UpdateBuffUI();
                return;
            }
        }
        BuffBase tmp_buff = BuffFactory.CreateBuff(buffName, gameObject);
        _buffBaseList.Add(tmp_buff); 
        tmp_buff.OnBuffApplied(this,this);
        _enemyUIBase.UpdateBuffUI();
    }

    public void RemoveBuff(EnumTools.BuffName buffName)
    {
        BuffBase tmp_remove = null;
        foreach (BuffBase buffBase in _buffBaseList)
        {
            if (buffBase.GetBuffName() == buffName)
            {
                tmp_remove = buffBase;
                break;
            }
        }
        if(tmp_remove== null)
            return;

        tmp_remove.OnBuffEnd();
        _buffBaseList.Remove(tmp_remove);
        _enemyUIBase.UpdateBuffUI();
    }
    
    private void SetRagdollActive(bool isActive)
    {
        animator.enabled = !isActive;
        // 控制每个关节的 Rigidbody 是否由物理引擎控制
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = !isActive; // 活着时 isKinematic = true，死亡时 isKinematic = false
        }

        foreach (CapsuleCollider capsuleCollider in _colliders)
        {
            capsuleCollider.enabled = isActive;
        }
        
        // 如果不想影响 Joint 位置，也可以禁用关节组件
        foreach (Joint joint in joints)
        {
            joint.enableCollision = isActive; // 死亡时 Joint 生效
        }
    }
    
    public void CheckDeath()
    {
        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        isDeath = true;
        OnDeath?.Invoke();
        SetRagdollActive(true);
        EnemyManager.GetInstance()?.UnRegisterEnemy(this);
    }

    public float GetHealthPercent()
    {
        return CurrentHealth / EnemyData.MaxHealth;
    }

    public float GetMaxHealth()
    {
        return EnemyData.MaxHealth;
    }

    public Vector3 GetCenter()
    {
        return Center.position;
    }

    public List<BuffBase> GetBuffList()
    {
        return _buffBaseList;
    }
}
