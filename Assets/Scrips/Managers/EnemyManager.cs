using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<EnemyBase> CurrentEnemyBaseList;

    public void RegisterEnemy(EnemyBase enemyBase)
    {
        if (CurrentEnemyBaseList.Contains(enemyBase))
        {
            return;
        }
        CurrentEnemyBaseList.Add(enemyBase);
    }

    public void UnRegisterEnemy(EnemyBase enemyBase)
    {
        CurrentEnemyBaseList?.Remove(enemyBase);
    }

    /// <summary>
    /// 获取范围内最近的敌人
    /// </summary>
    /// <param name="currentPos">基准位置</param>
    /// <param name="rangeLimit">范围</param>
    /// <returns></returns>
    public EnemyBase GetClosestEnemy(Transform currentPos, float rangeLimit)
    {
        if (CurrentEnemyBaseList.Count>0)
        {
            EnemyBase bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            foreach (EnemyBase enemyBase in CurrentEnemyBaseList)
            {
                Vector3 directionToTarget = enemyBase.transform.position - currentPos.position;
                float dSToTarget = directionToTarget.magnitude;

                if (dSToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSToTarget;
                    if (dSToTarget > rangeLimit)
                    {
                        continue;
                    }
                    bestTarget = enemyBase;
                    
                }
            }
        
            return bestTarget;
        }
        else
        {
            return null;
        }
    }
}
