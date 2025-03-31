using System;
using Autohand;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion;
using UnityEngine;

public class PlacePointFollow : MonoBehaviour
{
    public GameObject target;
    public float set = 0.01f;
    private void Update()
    {
        transform.rotation = target.transform.rotation * Quaternion.Euler(-90, 0, 0);
        Vector3 offset = transform.forward * set;
        transform.position = target.transform.position + offset;
    }

    public void OnPlaced(PlacePoint point, Grabbable grabbable)
    {
        if (grabbable.GetComponent<Collider>())
        {
            grabbable.GetComponent<Collider>().enabled = false;
        }
    }
    
    public void OnReleased(PlacePoint point, Grabbable grabbable)
    {
        if (grabbable.GetComponent<Collider>())
        {
            grabbable.GetComponent<Collider>().enabled = true;
        }
    }
}
