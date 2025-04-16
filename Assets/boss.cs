using BehaviorDesigner.Runtime;
using Tools;
using UnityEngine;
using UnityEngine.AI;

public class boss : EnemyBase
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
        _behaviorTree.SetVariableValue("Player", Player.GetInstance().GetRoot().gameObject);
    }

    public override void TakeDamage(float dmg, EnumTools.DamageKind damageKind, Vector3 position)
    {
        base.TakeDamage(dmg, damageKind, position);
        animator.SetTrigger("hit");
    }
}
