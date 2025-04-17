using BehaviorDesigner.Runtime;
using Tools;
using UnityEngine;
using UnityEngine.AI;

public class boss : EnemyBase
{
    private BehaviorTree _behaviorTree;
    private NavMeshAgent _agent;
    public float Speed;
    public GameObject FireballPrefab;
    public Transform FireBallPoint;
    public float Height = 5;
    public Vector2 spawnAreaSize = new Vector2(10f, 10f);
    public Transform SpwanPoint;
    [Header("Gizmo Settings")]
    public Color spawnAreaColor = new Color(0f, 0f, 1f, 0.3f); // 正方形边框颜色
    public Color validPointColor = Color.green; // 合法点颜色
    public Color invalidPointColor = Color.red; // 非法点颜色
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

    public void ShotFireBall()
    {
        GameObject target = Player.GetInstance().GetRoot().gameObject;
        GameObject go = Instantiate(FireballPrefab, FireBallPoint.position,Quaternion.identity);
        Debug.Log("FireBall");
        Rigidbody rb = go.GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            Vector3 velocity = CalculateLaunchVelocity(FireBallPoint.position,target.transform.position, Height);
            rb.linearVelocity = velocity;
        }

    }
    
    public void SpawnEnemy()
    {
        EnemySpawnManager.GetInstance().SpawnBossEnemy(spawnAreaSize,SpwanPoint.position);
    }
    
    Vector3 CalculateLaunchVelocity(Vector3 start, Vector3 end, float height)
    {
        Vector3 dir = end - start;
        Vector3 dirXZ = new Vector3(dir.x, 0, dir.z);
        float distance = dirXZ.magnitude;

        float verticalOffset = end.y - start.y;
        float gravity = Mathf.Abs(Physics.gravity.y);

        // clamp height to avoid sqrt of negative number
        float h = Mathf.Max(height, verticalOffset + 0.1f); // 保证 height 至少比 verticalOffset 高一点

        float timeUp = Mathf.Sqrt(2 * h / gravity);
        float timeDown = Mathf.Sqrt(2 * Mathf.Max(0.1f, h - verticalOffset) / gravity); // 防止负数
        float totalTime = timeUp + timeDown;

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(2 * gravity * h);
        Vector3 velocityXZ = dirXZ / totalTime;

        return velocityXZ + velocityY;
    }
    
    private void OnDrawGizmos() {
        // 绘制刷怪区域边框
        Gizmos.color = spawnAreaColor;
        Vector3 center = transform.position;
        Vector3 size = new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.y);
        Gizmos.DrawWireCube(center, size);
    }
}
