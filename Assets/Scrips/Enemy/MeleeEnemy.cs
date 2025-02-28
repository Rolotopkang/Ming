using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Movement;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    private BehaviorTree _behaviorTree;

    protected override void Awake()
    {
        base.Awake();
        _behaviorTree = GetComponent<BehaviorTree>();
    }

    protected override void Start()
    {
        base.Start();
        _behaviorTree.FindTask<Seek>().m_Target = Player.GetInstance().GetRoot().gameObject;

    }
}
