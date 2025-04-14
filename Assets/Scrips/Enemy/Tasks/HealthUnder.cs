using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using UnityEngine;

namespace Scrips.Enemy.Tasks
{
    public class HealthUnder : Conditional
    {
        public SharedFloat hpCondition;
        public EnemyBase EnemyBase;

        public override void OnStart()
        {
            base.OnStart();
            EnemyBase = GetComponent<EnemyBase>();
        }

        public override TaskStatus OnUpdate()
        {
            return EnemyBase.GetHealthPercent() < hpCondition.Value ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}