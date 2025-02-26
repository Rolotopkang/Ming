using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Movement;
using Tools;
using UnityEngine;
using UnityEngine.AI;

public class EliteEnemy : EnemyBase
{
    private BehaviorTree _behaviorTree;
    private NavMeshAgent _agent;

    protected override void Awake()
    {
        base.Awake();
        _behaviorTree = GetComponent<BehaviorTree>();
        _agent = GetComponent<NavMeshAgent>();
    }

    protected override void Start()
    {
        base.Start();
        // _behaviorTree.FindTask<Seek>().m_Target = Player.GetInstance().GetRoot().gameObject;

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

    public override void TakeDamage(float dmg, EnumTools.DamageKind damageKind, Vector3 position)
    {
        base.TakeDamage(dmg, damageKind, position);
        animator.SetTrigger("hit");
    }
}
