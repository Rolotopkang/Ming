using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightningShootTut : MonoBehaviour
{
    [SerializeField] float refreshRate = 0.01f;
    [SerializeField][Range(1, 10)] int maximumEnemiesInChain = 3;
    [SerializeField] float delayBetweenEachChain = 0.2f;
    [SerializeField] Transform playerFirePoint;
    [SerializeField] EnemyDetector playerEnemyDetector;
    [SerializeField] GameObject lineRendererPrefab;
    [SerializeField] GameObject impactPrefab;
    [SerializeField] GameObject activePrefab;

    bool shooting;
    bool shot;
    float counter = 1;
    GameObject currentClosestEnemy;
    List<GameObject> spawnedLineRenderers = new List<GameObject>();
    List<GameObject> enemiesInChain = new List<GameObject>();
    List<GameObject> activeEffects = new List<GameObject>();

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if(playerEnemyDetector.GetEnemiesInRange().Count > 0)
            {
                if (!shooting)
                {
                    StartShooting();
                }
            }
            else
            {
                StopShooting();
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopShooting();
        }
    }

    void StartShooting()
    {
        shooting = true;

        if(playerEnemyDetector != null && playerFirePoint != null && lineRendererPrefab !=null)
        {
            if(!shot)
            {
                shot = true;

                currentClosestEnemy = playerEnemyDetector.GetClosestEnemy();
                NewLineRenderer(playerFirePoint, currentClosestEnemy.transform, true);

                if(maximumEnemiesInChain > 1)
                {
                    StartCoroutine(ChainReaction(currentClosestEnemy));
                }
            }            
        }
    }

    void NewLineRenderer (Transform startPos, Transform endPos, bool fromPlayer = false)
    {
        GameObject lineR = Instantiate(lineRendererPrefab);
        spawnedLineRenderers.Add(lineR);
        StartCoroutine(UpdateLineRenderer(lineR, startPos, endPos, fromPlayer));

        GameObject impactVFX = Instantiate(impactPrefab, endPos.position, Quaternion.identity) as GameObject;
        Destroy(impactVFX, 5);

        GameObject activeVFX = Instantiate(activePrefab, endPos.position, Quaternion.identity) as GameObject;
        activeEffects.Add(activeVFX);
    }

    IEnumerator UpdateLineRenderer (GameObject lineR, Transform startPos, Transform endPos, bool fromPlayer = false)
    {
        if(shooting && shot && lineR != null)
        {
            lineR.GetComponent<LineRendererController>().SetPosition(startPos, endPos);

            yield return new WaitForSeconds(refreshRate);

            if (fromPlayer)
            {
                StartCoroutine(UpdateLineRenderer(lineR, startPos, playerEnemyDetector.GetClosestEnemy().transform, true));

                if(currentClosestEnemy != playerEnemyDetector.GetClosestEnemy())
                {
                    StopShooting();
                    StartShooting();
                }
            }
            else
            {
                StartCoroutine(UpdateLineRenderer(lineR, startPos, endPos));
            }
            
        }        
    }

    IEnumerator ChainReaction (GameObject closesetEnemy)
    {
        yield return new WaitForSeconds(delayBetweenEachChain);

        if(counter == maximumEnemiesInChain)
        {
            yield return null;
        }
        else
        {
            if (shooting)
            {
                counter++;

                enemiesInChain.Add(closesetEnemy);

                if(!enemiesInChain.Contains(closesetEnemy.GetComponent<EnemyDetector>().GetClosestEnemy()))
                {
                    NewLineRenderer(closesetEnemy.transform, closesetEnemy.GetComponent<EnemyDetector>().GetClosestEnemy().transform);
                    StartCoroutine(ChainReaction(closesetEnemy.GetComponent<EnemyDetector>().GetClosestEnemy()));
                }
                else
                {
                    Debug.Log("Nor more unique enemies in chain.");
                }
            }
        }
    }

    void StopShooting()
    {
        shooting = false;
        shot = false;
        counter = 1;

        for (int i = 0; i < spawnedLineRenderers.Count; i++)
        {
            Destroy(spawnedLineRenderers[i]);
        }

        spawnedLineRenderers.Clear();
        enemiesInChain.Clear();

        for (int i = 0; i < activeEffects.Count; i++)
        {
            Destroy(activeEffects[i]);
        }

        activeEffects.Clear();
    }
}
