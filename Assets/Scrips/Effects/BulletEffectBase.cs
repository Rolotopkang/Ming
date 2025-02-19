using UnityEngine;

namespace Scrips.Effects
{
    public class BulletEffectBase : MonoBehaviour
    {
        public virtual void TriggerEffect(BulletBase bulletBase, EnemyBase enemyBase, Vector3 hitPoint)
        {
            Debug.Log("击中"+enemyBase.name+"触发"+this);
        }
    }
}