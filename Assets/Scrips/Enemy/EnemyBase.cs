using System;
using System.Collections.Generic;
using Scrips.Buffs;
using Scrips.Factory;
using Tools;
using Unity.XR.CoreUtils;
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
    public Renderer SkinnedMeshRenderer;
    public List<Material> DebuffShader;
    public UnityEvent OnHit;

    private List<BuffBase> _buffBaseList = new List<BuffBase>();
    protected UI_EnemyUI_Base _enemyUIBase;
    
    private Rigidbody[] ragdollRigidbodies;
    private Joint[] joints;
    protected Animator animator;
    private CapsuleCollider[] _colliders;

    protected virtual void Awake()
    {
        isDeath = false;
        _enemyUIBase = GetComponentInChildren<UI_EnemyUI_Base>();
        _enemyUIBase.EnemyUIRegister(this);
        InitRagdoll();
    }

    private void InitRagdoll()
    {
        animator = GetComponent<Animator>();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        joints = GetComponentsInChildren<Joint>();
        _colliders = GetComponentsInChildren<CapsuleCollider>();
    }

    protected virtual void Start()
    {
        // SetRagdollActive(false);
        InvokeRepeating(nameof(UpdateSec), 0f,1f);
    }

    protected virtual void OnEnable()
    {
        CurrentHealth = EnemyData.MaxHPCurve.Evaluate(RoguelikeManager.GetInstance().layer);
        EnemyManager.GetInstance()?.RegisterEnemy(this);
    }

    private void OnDisable()
    {
        EnemyManager.GetInstance()?.UnRegisterEnemy(this);
    }

    protected virtual void OnDestroy()
    {
        
    }

    private void UpdateSec()
    {
        TriggerBuffs();
    }

    public virtual void TakeDamage(float dmg ,EnumTools.DamageKind damageKind ,Vector3 position)
    {
        if (isDeath)
        {
            return;
        }

        if (damageKind == EnumTools.DamageKind.Poison && EnemyData.isBoss)
        {
            dmg = EnemyData.MaxHPCurve.Evaluate(RoguelikeManager.GetInstance().layer) * 0.002f;
        }
        
        OnHit.Invoke();
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

        tmp_remove.OnBuffEnd(this,this);
        _buffBaseList.Remove(tmp_remove);
        Destroy(tmp_remove);
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

    public virtual void Slow(float amount)
    {
        
    }

    public virtual void Death()
    {
        isDeath = true;
        OnDeath?.Invoke();

        EnemyManager.GetInstance()?.UnRegisterEnemy(this);
    }

    // public void AddBuffShader(EnumTools.BuffName buffName,bool isAdd)
    // {
    //     Material tmp_m = GetDebuffMaterial(buffName);
    //     if (tmp_m == null)
    //     {
    //         return;
    //     }
    //     
    //     List<Material> mats = new List<Material>(SkinnedMeshRenderer.materials);
    //     if (isAdd)
    //     {
    //         if (!mats.Contains(tmp_m))
    //         {
    //             mats.Add(tmp_m);
    //             SkinnedMeshRenderer.materials = mats.ToArray();
    //         }
    //     }
    //     else
    //     {
    //         if (mats.Contains(tmp_m))
    //         {
    //             mats.Remove(tmp_m);
    //             SkinnedMeshRenderer.materials = mats.ToArray();
    //         }
    //     }
    //
    // }
    
    public void AddBuffShader(EnumTools.BuffName buffName, bool isAdd)
    {
        Material tmp_m = GetDebuffMaterial(buffName);
        if (tmp_m == null) return;

        // 使用 sharedMaterials，避免 Unity 复制 materials
        List<Material> mats = new List<Material>(SkinnedMeshRenderer.sharedMaterials);

        if (isAdd)
        {
            if (!mats.Contains(tmp_m))
            {
                mats.Add(tmp_m);
                SkinnedMeshRenderer.sharedMaterials = mats.ToArray(); // 直接修改 sharedMaterials
            }
        }
        else
        {
            if (mats.Contains(tmp_m))
            {
                mats.Remove(tmp_m);
                SkinnedMeshRenderer.sharedMaterials = mats.ToArray(); // 直接修改 sharedMaterials
            }
        }
    }


    
    private Material GetDebuffMaterial(EnumTools.BuffName buffName)
    {
        switch (buffName)
        {
            case EnumTools.BuffName.Fire: return DebuffShader[0];
            case EnumTools.BuffName.Poison: return DebuffShader[1];
            case EnumTools.BuffName.Ice: return DebuffShader[2];
            case EnumTools.BuffName.Frozen: return DebuffShader[3];
            default: return null;
        }
    }

    public float GetHealthPercent()
    {
        return CurrentHealth / EnemyData.MaxHPCurve.Evaluate(RoguelikeManager.GetInstance().layer);
    }

    public float GetMaxHealth()
    {
        return EnemyData.MaxHPCurve.Evaluate(RoguelikeManager.GetInstance().layer);
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
