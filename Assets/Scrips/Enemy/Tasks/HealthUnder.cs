using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using System.Collections.Generic;

namespace Scrips.Enemy.Tasks
{
    public class HealthUnder : Conditional
    {
        // 存储多个血量百分比触发条件
        public List<SharedFloat> hpConditions;
        public EnemyBase EnemyBase;
        private int currentConditionIndex = 0; // 当前已经触发的血量条件索引
        private bool hasTriggered = false; // 确保每个条件只触发一次

        public override void OnStart()
        {
            base.OnStart();
            EnemyBase = GetComponent<EnemyBase>();
            hasTriggered = false; // 每次开始时重置
        }

        public override TaskStatus OnUpdate()
        {
            // 如果当前触发的血量条件索引超出范围，返回失败
            if (currentConditionIndex >= hpConditions.Count)
            {
                return TaskStatus.Failure;
            }

            // 获取当前血量百分比
            float healthPercent = EnemyBase.GetHealthPercent();

            // 检查当前血量是否低于条件并且还没有触发过
            if (!hasTriggered && healthPercent < hpConditions[currentConditionIndex].Value)
            {
                // 一旦触发，更新索引，确保下一个条件触发
                currentConditionIndex++;
                hasTriggered = true; // 标记已经触发过
                return TaskStatus.Success;
            }

            // 如果条件没有满足，返回失败，不影响整个 Sequence
            return TaskStatus.Failure;
        }
    }
}