using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{

    List<GameObject> enemiesInRange = new List<GameObject>();

    public GameObject GetClosestEnemy ()
    {
        if(enemiesInRange.Count > 0)
        {
            GameObject bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;

            foreach (GameObject closestEnemy in enemiesInRange)
            {
                Vector3 directionToTarget = closestEnemy.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;

                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = closestEnemy;
                }
            }

            return bestTarget;
        }
        else 
        { 
            return null; 
        }        
    }

    public List<GameObject> GetEnemiesInRange ()
    {
        return enemiesInRange;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Enemy"))
        {            
            if(enemiesInRange.Count == 0) 
            {
                enemiesInRange.Add(col.gameObject);
            }
            else if (!enemiesInRange.Contains(col.gameObject))
            {
                enemiesInRange.Add(col.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            if (enemiesInRange.Count > 0)
            {                
                enemiesInRange.Remove(col.gameObject);
            }
        }
    }
}
