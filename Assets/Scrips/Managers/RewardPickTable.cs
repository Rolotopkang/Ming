using System;
using System.Collections.Generic;
using System.Linq;
using Autohand;
using Tools;
using UnityEngine;

public class RewardPickTable : Singleton<RewardPickTable>
{
    public int RewardNum = 3;
    public AnimationCurve level1ProbabilityCurve;
    public AnimationCurve level2ProbabilityCurve;
    public AnimationCurve level3ProbabilityCurve;
    public AnimationCurve level4ProbabilityCurve;
    private List<PlacePoint> rewardPoints;
    
    private void Start()
    {
        rewardPoints = GetComponentsInChildren<PlacePoint>().ToList();
    }

    public void updateNewPosition(Transform _transform)
    {
        transform.position = _transform.position;
    }
    
    public void ShowReward()
    {
        //Animation
        
        
        ClearTable();
        int i = 0;
        List<GameObject> resault = ItemDatabaseManager.GetInstance().GetRandomNoneIsOnlyItems(RewardNum);
        foreach (GameObject gameObject in resault)
        {
            Grabbable clone = Instantiate(gameObject, Vector3.zero, Quaternion.identity ,transform).GetComponent<Grabbable>();
            rewardPoints[i++].Place(clone);
            clone.GetComponent<ItemBase>().ItemCount = SelectItemByDifficulty(RoguelikeManager.GetInstance().layer);
        }
        
        foreach (PlacePoint placePoint in rewardPoints)
        {
            placePoint.OnRemove.AddListener(DestroyRestReward);
        }
    }

    public void DestroyRestReward(PlacePoint pp, Grabbable grabbable)
    {
        foreach (PlacePoint placePoint in rewardPoints)
        {
            placePoint.OnRemove.RemoveListener(DestroyRestReward);
        }
        foreach (PlacePoint placePoint in rewardPoints)
        {
            if (placePoint != pp)
            {
                if (placePoint.placedObject!= null)
                {
                    Grabbable tmp = placePoint.placedObject;
                    placePoint.Remove();
                    Destroy(tmp.gameObject);
                }
            }
        }
    }

    private void ClearTable()
    {
        foreach (PlacePoint placePoint in rewardPoints)
        {
            if (placePoint.placedObject!= null)
            {
                Grabbable tmp = placePoint.placedObject;
                placePoint.Remove(); 
                Destroy(tmp.gameObject);
            }
        }
    }
    
    public int SelectItemByDifficulty(float difficulty)
    {
        float level1Probability = level1ProbabilityCurve.Evaluate(difficulty);
        float level2Probability = level2ProbabilityCurve.Evaluate(difficulty);
        float level3Probability = level3ProbabilityCurve.Evaluate(difficulty);
        float level4Probability = level4ProbabilityCurve.Evaluate(difficulty);
        float totalProbability = level1Probability + level2Probability + level3Probability + level4Probability;
        level1Probability /= totalProbability;
        level2Probability /= totalProbability;
        level3Probability /= totalProbability;
        level4Probability /= totalProbability;
        
        float randomValue = UnityEngine.Random.value;
        if (randomValue < level1Probability)
        {
            return 1;  // 1级物品
        }
        else if (randomValue < level1Probability + level2Probability)
        {
            return 2;  // 2级物品
        }
        else if (randomValue < level1Probability + level2Probability + level3Probability)
        {
            return 3;  // 3级物品
        }
        else
        {
            return 4;  // 4级物品
        }
    }
}
