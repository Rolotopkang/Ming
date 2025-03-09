using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrips.Effects
{
    public class BulletEffectBase : MonoBehaviour
    {
        protected BulletBase fatherBullet;
        protected Rigidbody rb;

        protected virtual void Awake()
        {
            fatherBullet = GetComponent<BulletBase>();
            rb = GetComponent<Rigidbody>();
        }

        public virtual void TriggerEffect(BulletBase bulletBase, EnemyBase enemyBase, Vector3 hitPoint)
        {
            Debug.Log("击中"+enemyBase.name+"触发"+this);
        }
        
    }
}