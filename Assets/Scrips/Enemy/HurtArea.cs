using System;
using Tools;
using UnityEngine;

public class HurtArea : MonoBehaviour
{
    public bool hurtable;
    public float DMG;
    private void OnEnable()
    {
        hurtable = true;
    }

    private void OnDisable()
    {
        hurtable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && hurtable)
        {
            if (other.transform.GetComponentInParent<Player>())
            {
                Debug.Log("Hit player: ");
                Player.GetInstance().TakeDamage(DMG,EnumTools.DamageKind.None,Vector3.zero);
                hurtable = false;
                return;
            }
        }
    }
}
