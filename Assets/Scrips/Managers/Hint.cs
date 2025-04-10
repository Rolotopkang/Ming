using System;
using TMPro;
using UnityEngine;

public class Hint : MonoBehaviour
{
    public TextMeshProUGUI showtext;
    private float LifeTime;
    private Transform player;

    private void Start()
    {
        if (Camera.main != null) player = Camera.main.transform;
    }

    private void Update()
    {
        Vector3 forward = transform.position - player.position;
        forward.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(forward, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime / 0.2f);
    }

    public void Init(string text , float lifetime)
    { 
        showtext.text = text;
        if (lifetime == -1)
        {
            return;
        }
        Destroy(gameObject, lifetime);
    }
}
