using System;
using Autohand;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform restPosition; // 弹弓的初始位置
    public float returnSpeed = 10f; // 回弹速度
    private bool isReturning = true;
    public Grabbable GrabPoint;
    public SlingshotTrajectory SlingshotTrajectory;
    public BulletBase BulletPrefab;
    
    private Rigidbody grabRig;
    
    void Start()
    {
        GrabPoint.onRelease.AddListener(OnRelease);
        GrabPoint.onGrab.AddListener(OnGrab);
        grabRig = GrabPoint.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isReturning)
        {
            GrabPoint.transform.position = Vector3.Lerp(GrabPoint.transform.position, restPosition.position, Time.deltaTime * returnSpeed);
            // 当距离足够小，停止回弹
            if (Vector3.Distance(GrabPoint.transform.position, restPosition.position) < 0.01f) 
            { 
                GrabPoint.transform.position = restPosition.position;
            }
        }
    }


    private void OnRelease(Hand arg0, Grabbable grabbable)
    {
        isReturning = true;
        grabRig.isKinematic = true;
        SlingshotTrajectory.isopen = false;
        BulletBase projectile = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Vector3 launchVelocity = (restPosition.position - transform.position) *20f;
        rb.linearVelocity = launchVelocity;
    }

    private void OnGrab(Hand arg0, Grabbable grabbable)
    {
        isReturning = false;
        grabRig.isKinematic = false;
        SlingshotTrajectory.isopen = true;
    }
}
