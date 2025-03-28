using System;
using UnityEngine;

public class LookAtRotation : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        transform.rotation = target.transform.rotation;
    }
}
