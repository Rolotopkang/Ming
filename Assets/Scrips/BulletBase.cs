using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            ///TODO 碰撞特效
            Destroy(gameObject);
        }
    }
    
    
}
