using BehaviorDesigner.Runtime;
using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Movement;
using Tools;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Scrips.Enemy
{
    public class EliteEnemy : EnemyBase
    {
    private BehaviorTree _behaviorTree;
    private NavMeshAgent _agent;
    private BoxCollider _boxCollider;

    protected override void Awake()
    {
        base.Awake();
        _behaviorTree = GetComponent<BehaviorTree>();
        _agent = GetComponent<NavMeshAgent>();
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.enabled = false;
        _behaviorTree.SetVariableValue("Speed",EnemyData.SpeedCurve.Evaluate(RoguelikeManager.GetInstance().layer));
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        EnemySpawnManager.GetInstance()?.RegisterWaveEnemy(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EnemySpawnManager.GetInstance()?.UnRegisterWaveEnemy(this);
    }

    protected override void Start()
    {
        base.Start();
        _behaviorTree.SetVariableValue("Player", Player.GetInstance().GetRoot().gameObject);
        GetComponent<NavMeshAgent>().avoidancePriority = Random.Range(30, 50);
    }

    public override void Death()
    {
        base.Death();
        EnemySpawnManager.GetInstance()?.UnRegisterWaveEnemy(this);
        animator.SetTrigger("death");
        Destroy(gameObject,2f);
    }

    private void Update()
    {
        if (_agent.velocity.sqrMagnitude < 0.01f)
        {
            animator.SetBool("isWalking",false);
        }
        else
        {
            animator.SetBool("isWalking",true);
        }
    }

    public override void Slow(float amount)
    {
        if (amount == 0)
        {
            _behaviorTree.SetVariableValue("Speed",0f);
            return;
        }
        
        if (amount<0)
        {
            _behaviorTree.SetVariableValue("Speed",EnemyData.SpeedCurve.Evaluate(RoguelikeManager.GetInstance().layer));
        }
        else
        {
            _behaviorTree.SetVariableValue("Speed",EnemyData.SpeedCurve.Evaluate(RoguelikeManager.GetInstance().layer) * (1- amount));
        }
    }

    public override void TakeDamage(float dmg, EnumTools.DamageKind damageKind, Vector3 position)
    {
        base.TakeDamage(dmg, damageKind, position);
        animator.SetTrigger("hit");
    }

    public void OpenHurtRange()
    {
        _boxCollider.enabled = true;
    }

    public void CloseHurtRange()
    {
        _boxCollider.enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.GetComponentInParent<Player>())
            {
                Debug.Log("Hit player: ");
                Player.GetInstance().TakeDamage(EnemyData.BaseAtkCurve.Evaluate(RoguelikeManager.GetInstance().layer),EnumTools.DamageKind.None,Vector3.zero);
                return;
            }
        }
    }
    }
}