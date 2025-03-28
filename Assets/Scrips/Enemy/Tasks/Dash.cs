using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using UnityEngine;

namespace Scrips.Enemy.Tasks
{
    public class Dash : NavMeshMovement
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The GameObject that the agent is seeking")]
        [UnityEngine.Serialization.FormerlySerializedAs("target")]
        public SharedGameObject m_Target;
        [BehaviorDesigner.Runtime.Tasks.Tooltip("If target is null then use the target position")]
        [UnityEngine.Serialization.FormerlySerializedAs("targetPosition")]
        public SharedVector3 m_TargetPosition;

        public float SpeedScale = 5;

        public override void OnStart()
        {
            base.OnStart();

            SetDestination(Target());
        }

        // Seek the destination. Return success once the agent has reached the destination.
        // Return running if the agent hasn't reached the destination yet
        public override TaskStatus OnUpdate()
        {
            m_NavMeshAgent.speed = m_Speed.Value * SpeedScale;
            if (HasArrived()) {
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }
        
        // Return targetPosition if target is null
        private Vector3 Target()
        {
            if (m_Target.Value != null) {
                return m_Target.Value.transform.position;
            }
            return m_TargetPosition.Value;
        }

        public override void OnReset()
        {
            base.OnReset();
            m_Target = null;
            m_TargetPosition = Vector3.zero;
        }
    }
}