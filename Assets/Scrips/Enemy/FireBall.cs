using System;
using Tools;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject ColPrefab;
    public float ExplotionScale = 2f;
    public float explosionRadius;
    public LayerMask hitLayers;
    
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy")|| other.gameObject.CompareTag("WeakPoint"))
        {
            return;
        }
        
        GameObject go= Instantiate(ColPrefab, transform.position, Quaternion.identity);
        go.transform.localScale = new Vector3(ExplotionScale, ExplotionScale, ExplotionScale);
        
        // 检测范围内的对象
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, hitLayers);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Player"))
            {
                if (hit.GetComponentInParent<Player>())
                {
                    Debug.Log(hit.gameObject.name);
                    hit.GetComponentInParent<Player>().TakeDamage(10,EnumTools.DamageKind.Normal,Vector3.zero);
                    Destroy(gameObject);
                    return;
                }
            }
        }

        
        Destroy(gameObject);
    }
    
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.linearVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity);
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.4f); // 半透明红色
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}
