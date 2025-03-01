using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Movement;
using Tools;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EliteEnemy : EnemyBase
{
    public Vector3 AttackHitboxOffset = new Vector3(0, 1, 1); // 立方体相对怪物位置的偏移
    public Vector3 AttackHitboxSize = new Vector3(2, 2, 2); // 立方体的尺寸
    private BehaviorTree _behaviorTree;
    private NavMeshAgent _agent;

    protected override void Awake()
    {
        base.Awake();
        _behaviorTree = GetComponent<BehaviorTree>();
        _agent = GetComponent<NavMeshAgent>();
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
        animator.SetTrigger("death");
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
    
    public void CheckAttackHit()
    {
        Vector3 boxCenter = transform.position + transform.TransformDirection(AttackHitboxOffset);
        
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, AttackHitboxSize * 0.5f, transform.rotation);

        if (hitColliders.Length > 0)
        {
            foreach (Collider hit in hitColliders)
            {
                if (hit.transform.GetComponentInParent<Player>())
                {
                    Debug.Log("Hit player: ");
                    Player.GetInstance().TakeDamage(EnemyData.BaseAtkCurve.Evaluate(RoguelikeManager.GetInstance().layer),EnumTools.DamageKind.None,Vector3.zero);
                    return;
                }
            }
        }
        else
        {
            Debug.Log("No player detected in attack range.");
        }
    }

    // 在Scene视图中显示检测范围
    private void OnDrawGizmosSelected()
    {
        Vector3 boxCenter = transform.position + transform.TransformDirection(AttackHitboxOffset);
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, AttackHitboxSize);
    }
}
