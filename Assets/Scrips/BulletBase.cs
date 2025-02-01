using System;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float lifetime = 5f;
    public GameObject OnHitEffectPrefab;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Instantiate(OnHitEffectPrefab, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
    }
}
