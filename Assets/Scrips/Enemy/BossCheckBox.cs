using System;
using UnityEngine;

public class BossCheckBox : MonoBehaviour
{
    public GameObject boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boss.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
