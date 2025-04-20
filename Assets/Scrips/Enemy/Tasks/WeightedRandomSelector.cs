using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("A weighted random selector runs child tasks in a random order, based on a custom weight for each child. " +
                 "As soon as one child returns success, it stops and returns success. If all fail, it returns failure.")]
[TaskIcon("{SkinColor}RandomSelectorIcon.png")]
public class WeightedRandomSelector : Composite
{
    public SharedFloat[] weights;

    private List<int> weightedOrder = new List<int>();
    private Stack<int> childrenExecutionOrder = new Stack<int>();
    private TaskStatus executionStatus = TaskStatus.Inactive;

    public override void OnStart()
    {
        ShuffleByWeights();
    }

    public override bool CanExecute()
    {
        return childrenExecutionOrder.Count > 0 && executionStatus != TaskStatus.Success;
    }

    public override int CurrentChildIndex()
    {
        return childrenExecutionOrder.Peek();
    }

    public override void OnChildExecuted(TaskStatus childStatus)
    {
        if (childrenExecutionOrder.Count > 0) {
            childrenExecutionOrder.Pop();
        }
        executionStatus = childStatus;
    }

    public override void OnConditionalAbort(int childIndex)
    {
        executionStatus = TaskStatus.Inactive;
        childrenExecutionOrder.Clear();
        ShuffleByWeights();
    }

    public override void OnEnd()
    {
        executionStatus = TaskStatus.Inactive;
        childrenExecutionOrder.Clear();
    }

    public override void OnReset()
    {
        weights = null;
    }

    private void ShuffleByWeights()
    {
        childrenExecutionOrder.Clear();
        weightedOrder.Clear();

        if (weights == null || weights.Length != children.Count) {
            Debug.LogWarning("WeightedRandomSelector: weights array is null or does not match number of children.");
            // fallback to equal weights
            for (int i = 0; i < children.Count; i++) {
                weightedOrder.Add(i);
            }
        }
        else {
            List<int> candidates = new List<int>();
            List<float> currentWeights = new List<float>();

            for (int i = 0; i < children.Count; i++) {
                float weight = Mathf.Max(0.0001f, weights[i].Value); // avoid zero or negative
                candidates.Add(i);
                currentWeights.Add(weight);
            }

            // Weighted random sampling without replacement (Roulette wheel)
            while (candidates.Count > 0)
            {
                float totalWeight = 0f;
                foreach (var w in currentWeights)
                    totalWeight += w;

                float rand = Random.Range(0, totalWeight);
                float cumulative = 0f;
                for (int i = 0; i < candidates.Count; i++)
                {
                    cumulative += currentWeights[i];
                    if (rand <= cumulative)
                    {
                        weightedOrder.Add(candidates[i]);
                        candidates.RemoveAt(i);
                        currentWeights.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        // Push to stack in reverse order for top-to-bottom execution
        for (int i = weightedOrder.Count - 1; i >= 0; i--) {
            childrenExecutionOrder.Push(weightedOrder[i]);
        }
    }
}
