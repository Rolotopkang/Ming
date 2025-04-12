using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Scrips.Managers;
using Tools;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnManager : Singleton<EnemySpawnManager>
{
    [Header("Spawn Settings")] 
    public List<BigWave> Waves1;
    public List<BigWave> Waves2;
    public List<BigWave> Waves3;
    public Transform SpawnRoot;
    public Vector2 spawnAreaSize = new Vector2(10f, 10f);
    public float minDistanceBetweenEnemies = 1.5f; // 怪物之间的最小距离

    [Header("NavMesh Settings")]
    public float checkRadius = 1f; // 碰撞检测半径
    public LayerMask obstacleLayer; // 障碍物检测层
    
    [Header("Test Settings")]
    public int spawnCount = 5;
    public int maxAttempts = 50; // 检测多少个点用于可视化
    private Vector3[] testPoints; // 存储检测的点
    private bool[] validPoints; // 对应点是否合法
    public List<EnemyBase> CurrentWaveList;
    private bool working = false;
    private Vector3 spawnRoot;

    public void RegisterWaveEnemy(EnemyBase enemyBase)
    {
        if (CurrentWaveList.Contains(enemyBase))
        {
            return;
        }
        CurrentWaveList.Add(enemyBase);
    }

    public void UnRegisterWaveEnemy(EnemyBase enemyBase)
    {
        if (CurrentWaveList.Contains(enemyBase))
        {
            CurrentWaveList?.Remove(enemyBase);
        }
    }

    public void RemoveAllEnemy()
    {
        for (int i = CurrentWaveList.Count - 1; i >= 0; i--)
        {
            CurrentWaveList[i].Death();
        }

        working = false;
    }
    
    public async void SpawnEnemy(Vector2 _spawnAreaSize, Vector3 root)
    {
        spawnAreaSize = _spawnAreaSize;
        spawnRoot = root;
        List<BigWave> currentBigWaveList = RoguelikeManager.GetInstance().BigLayer switch
        {
            1 => Waves1,
            2 => Waves2,
            3 => Waves3,
            _ => null,
        };
        if (currentBigWaveList== null || currentBigWaveList.Count == 0)
        {
            Debug.LogError("缺少刷怪配置！！");
            return;
        }

        Debug.Log("刷怪起开始工作");
        working = true;
        BigWave currentWave = currentBigWaveList[Random.Range(0, currentBigWaveList.Count)];
        int currentLayer = RoguelikeManager.GetInstance().layer;
        
        foreach (Wave wave in currentWave.bigWaveList)
        {
            if (!working)
            {
                return;
            }
            
            foreach (EnemySpawnInfo enemySpawnInfo in wave.enemySpawnInfos)
            {
                for (int i = 0; i < (int)enemySpawnInfo.countCurve.Evaluate(currentLayer); i++)
                {
                    Vector3 spawnPoint = GetValidSpawnPoint();
                    Instantiate(enemySpawnInfo.enemyPrefab, spawnPoint, Quaternion.identity,SpawnRoot);
                }
            }
            if (currentWave.waitDeath)
            {
                Debug.Log("开始等待杀光");
                await UniTask.WaitUntil(() => CurrentWaveList.Count <= 0);
                Debug.Log("杀光结束");
            }

            if (!wave.Equals(currentWave.bigWaveList.Last()))
            {
                Debug.Log("等待回合结束");
                await UniTask.WaitForSeconds((int)currentWave.spawnInterval);
                Debug.Log("结束");
                
            }
        }
        working = false;
        Debug.Log("结束工作");
    }

    public bool isWaveEnd()
    {
        return !working && CurrentWaveList.Count <= 0;
    }
    
    private Vector3 GetValidSpawnPoint() {
        for (int i = 0; i < maxAttempts; i++) {
            float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
            float randomZ = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
            Vector3 randomPoint = spawnRoot + new Vector3(randomX, 0, randomZ);

            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, checkRadius, NavMesh.AllAreas)) {
                // 检测是否与其他带有"Enemy"标签的物体重叠
                if (!Physics.CheckSphere(hit.position, checkRadius, obstacleLayer) && 
                    !IsOverlappingWithEnemies(hit.position)) {
                    return hit.position; // 合法位置且没有与敌人重叠
                }
            }
        }
        return Vector3.zero;
    }

    private bool IsOverlappingWithEnemies(Vector3 position) {
        Collider[] colliders = Physics.OverlapSphere(position, minDistanceBetweenEnemies);
        foreach (var collider in colliders) {
            if (collider.CompareTag("Enemy")) {
                return true; // 有Enemy在附近
            }
        }
        return false; // 没有重叠
    }
}
