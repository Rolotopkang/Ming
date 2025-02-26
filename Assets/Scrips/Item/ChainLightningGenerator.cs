using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class ChainLightningGenerator : MonoBehaviour
{
    [SerializeField] float refreshRate = 0.01f;
    [SerializeField] GameObject lineRendererPrefab;
    [SerializeField] GameObject impactPrefab;
    [SerializeField] GameObject activePrefab;
    [SerializeField] float delayBetweenEachChain = 0.2f;
    private int _chainMaxCount = 1;
    private float _maxChainDistance = 3f;
    private EnemyBase _hitEnemy;
    private bool CanStart = false;
    float counter = 1;
    GameObject currentClosestEnemy;
    List<GameObject> spawnedLineRenderers = new List<GameObject>();
    List<EnemyBase> enemiesInChain = new List<EnemyBase>();
    List<GameObject> activeEffects = new List<GameObject>();


    public void Register(int ChainMaxCount, float MaxChainDistance,EnemyBase hitEnemy)
    {
        _chainMaxCount = ChainMaxCount;
        _maxChainDistance = MaxChainDistance;
        _hitEnemy = hitEnemy;
        counter = _chainMaxCount;
        enemiesInChain.Add(_hitEnemy);
        CanStart = true;
    }

    private void Update()
    {
        if (CanStart)
        {
            var position = _hitEnemy.Center;
            GameObject impactVFX = Instantiate(impactPrefab, position.position, Quaternion.identity) as GameObject;
            // GameObject activeVFX = Instantiate(activePrefab, position, Quaternion.identity) as GameObject;
            // activeEffects.Add(activeVFX);
            
            if (EnemyManager.GetInstance().GetClosestEnemy(_hitEnemy.transform, _maxChainDistance,enemiesInChain))
            {
                StartCoroutine(ChainReaction(EnemyManager.GetInstance()
                    .GetClosestEnemy(_hitEnemy.transform, _maxChainDistance,enemiesInChain)));
            }
            else
            {
                StopChain();
            }
            CanStart = false;
        }
    }
    
    void NewLineRenderer(Transform startPos, Transform endPos)
    {        
        GameObject lineR = Instantiate(lineRendererPrefab);
        spawnedLineRenderers.Add(lineR);
        StartCoroutine(UpdateLineRenderer(lineR, startPos, endPos));

        GameObject impactVFX = Instantiate(impactPrefab, endPos.position, Quaternion.identity) as GameObject;
        GameObject activeVFX = Instantiate(activePrefab, endPos.position, Quaternion.identity) as GameObject;
        activeEffects.Add(activeVFX);
    }

    IEnumerator UpdateLineRenderer(GameObject lineR, Transform startPos, Transform endPos)
    {
        if (lineR != null)
        {
            lineR.GetComponent<LineRendererController>().SetPosition(startPos, endPos);
            yield return new WaitForSeconds(refreshRate);
            StartCoroutine(UpdateLineRenderer(lineR, startPos, endPos));
        }
    }
    
    IEnumerator ChainReaction(EnemyBase closestEnemy)
    {
        yield return new WaitForSeconds(delayBetweenEachChain);

        if (counter <=0)
        {
            StopChain();
            yield return null;
        }
        else
        {
            counter--;
            enemiesInChain.Add(closestEnemy);
            closestEnemy.TakeDamage(
                PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Attack)* 
                PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.ChainLightningDmgPercentage),
                EnumTools.DamageKind.Electrical,
                closestEnemy.Center.position);
            
            NewLineRenderer(_hitEnemy.Center, closestEnemy.Center);
            
            //判断下一次遍历
            if (EnemyManager.GetInstance().GetClosestEnemy(closestEnemy.transform, _maxChainDistance,enemiesInChain))
            {
                StartCoroutine(ChainReaction(EnemyManager.GetInstance().GetClosestEnemy(closestEnemy.transform, _maxChainDistance,enemiesInChain)));
            }
            else
            {
                StopChain();
            }
        }       
    }

    private void StopChain()
    {
        foreach (var t in spawnedLineRenderers)
        {
            Destroy(t);
        }
        spawnedLineRenderers.Clear();
        enemiesInChain.Clear();
        foreach (var t in activeEffects)
        {
            Destroy(t);
        }
        activeEffects.Clear();
        Destroy(gameObject, 2f);
    }
}
