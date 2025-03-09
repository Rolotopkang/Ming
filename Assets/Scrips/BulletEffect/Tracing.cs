using System.Collections;
using UnityEngine;

namespace Scrips.Effects
{
    public class Tracing : BulletEffectBase
    {
        private EnemyBase target;
        private EnemyBase oldTarget;
        private float homingRange = 3f;
        
        private bool isHomingCoroutine;
        private float minLength;
        private Coroutine homing;
        private float enhanceHoming;
        private float homingStrength = 60f;
        private float homingAngelStrength = 8f;


        
        private void FixedUpdate()
        {
            target = EnemyManager.GetInstance().GetClosestEnemy(transform, homingRange);
            if (target != null && !isHomingCoroutine)
            {
                isHomingCoroutine = true;
                homing=StartCoroutine(HomingCoroutine(target));
                oldTarget = target;
            }else if (target != null && isHomingCoroutine)
            {
                if (oldTarget == target)
                {
                    return;
                }
                StopCoroutine(homing);
            }else if (target == null && isHomingCoroutine)
            {
                StopCoroutine(homing);
            }
        }
        
        IEnumerator HomingCoroutine(EnemyBase target)
        {
            float currentV = rb.linearVelocity.magnitude;
            while (gameObject.activeSelf && target != null && target.gameObject.activeSelf)
            {
                Vector3 targetDirection = (target.GetCenter() - transform.position).normalized;

                // 计算理想速度方向但保持原本的速度大小
                Vector3 currentVelocity = rb.linearVelocity;
                Vector3 desiredVelocity = targetDirection * currentVelocity.magnitude;
                Vector3 steeringForce = (desiredVelocity - currentVelocity).normalized;

                // 应用方向调整力
                rb.AddForce(steeringForce * homingStrength * rb.mass * currentV/20, ForceMode.Force);

                // 限制最大速度
                if (rb.linearVelocity.magnitude > currentV)
                {
                    rb.linearVelocity = rb.linearVelocity.normalized * currentV;
                }

                yield return null;
            }
        }
    }
}