using System;
using System.Collections;
using Autohand;
using TMPro.EditorUtilities;
using Tools;
using UnityEngine;

public class WeaponController : Singleton<WeaponController>
{
    //备忘录
    /// <summary>
    /// TODO 距离太小是否要发射
    /// </summary>
    
    public Transform restPosition; // 弹弓的初始位置
    public float returnSpeed = 10f; // 回弹速度
    private bool isReturning = true;
    public Grabbable GrabPoint;
    public bool ShowSlingShotTrajectory = false;
    public SlingshotTrajectory SlingshotTrajectory;
    public BulletBase BulletPrefab;
    
    public float MaxDragDistance = 30;
    public float DragPersentage = 0;
    public float DragDistance;
    public float forceMultiplier = 20f;
    
    public float vibrationDuration;
    public float vibrationstepSize;
    
    public Vector3 lunchVector3()
    {
        return (restPosition.position - GrabPoint.transform.position).normalized * DragDistance / 100;
    }

    private float distance;
    private Rigidbody grabRig;
    private float lastVibrationDistance;
    private Coroutine hapticCoroutine;
    private bool isVibrating = false;

    void Start()
    {
        GrabPoint.onRelease.AddListener(OnRelease);
        GrabPoint.onGrab.AddListener(OnGrab);
        grabRig = GrabPoint.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        UpdateDistance();
        if (isReturning)
        {
            GrabPoint.transform.position = Vector3.Lerp(GrabPoint.transform.position, restPosition.position, Time.deltaTime * returnSpeed);
            // 当距离足够小，停止回弹
            if (Vector3.Distance(GrabPoint.transform.position, restPosition.position) < 0.01f) 
            { 
                GrabPoint.transform.position = restPosition.position;
            }
        }
        else
        {
            CheckVibration();
        }
    }


    private void OnRelease(Hand arg0, Grabbable grabbable)
    {
        StopVibration();
        isReturning = true;
        grabRig.isKinematic = true;
        SlingshotTrajectory.isopen = false;

        int bulletCount =Mathf.FloorToInt(PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.BulletCount) >= 1
            ? PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.BulletCount)
            : 1) ;
        float spreadAngle = PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.BulletSpread) >= 0
            ? PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.BulletSpread)
            : 0;
        
        for (int i = 0; i < bulletCount; i++)
        {
            float angleOffset = (bulletCount == 1) ? 0f : Mathf.Lerp(-spreadAngle / 2, spreadAngle / 2, (float)i / (bulletCount - 1));
            
            Quaternion rotation = Quaternion.AngleAxis(angleOffset, SlingshotTrajectory.transform.up);
            
            BulletBase projectile = Instantiate(BulletPrefab, GrabPoint.transform.position, Quaternion.identity);
            
            
            //TODO lingshi
            projectile.AttackDmg = PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Attack);
            
            
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            Vector3 launchVelocity = rotation * lunchVector3() * forceMultiplier;
            rb.linearVelocity = launchVelocity;
        }
    }

    private void OnGrab(Hand arg0, Grabbable grabbable)
    {
        isReturning = false;
        grabRig.isKinematic = false;
        if (ShowSlingShotTrajectory)
        {
            SlingshotTrajectory.isopen = true;
        }
    }

    private void UpdateDistance()
    {
       distance = Vector3.Distance(restPosition.position, GrabPoint.transform.position) * 100f;
       DragPersentage = Mathf.Clamp01(distance / MaxDragDistance);
       DragDistance = DragPersentage>=1 ? MaxDragDistance : distance;
    }
    
    private void CheckVibration()
    {
        if (DragPersentage >= 1)
        {
            StartVibration(GrabPoint.GetHeldBy()[0],0.5f,0.5f);
        }
        else
        {
            StopVibration();
            if (Mathf.Floor(distance / vibrationstepSize) > Mathf.Floor(lastVibrationDistance / vibrationstepSize))
            {
                TriggerVibration();
                lastVibrationDistance = distance; // 更新上次震动位置
            }
            else if (Mathf.Floor(distance / vibrationstepSize) < Mathf.Floor(lastVibrationDistance / vibrationstepSize))
            {
                TriggerVibration();
                lastVibrationDistance = distance;
            }
        }
    }
    
    private void TriggerVibration()
    {
        if (GrabPoint.GetHeldBy() != null)
        {
            GrabPoint.GetHeldBy()[0].PlayHapticVibration(vibrationDuration,0.2f);
        }
    }
    
    public void StartVibration(Hand hand, float amplitude, float interval) {
        if (isVibrating) return; // 避免重复启动
        isVibrating = true;
        hapticCoroutine = StartCoroutine(VibrationLoop(hand, amplitude, interval));
    }

    public void StopVibration() {
        if (hapticCoroutine != null) {
            StopCoroutine(hapticCoroutine);
            hapticCoroutine = null;
        }
        isVibrating = false;
    }

    private IEnumerator VibrationLoop(Hand hand, float amplitude, float interval) {
        while (isVibrating) {
            hand.PlayHapticVibration(amplitude, interval * 0.2f);
            yield return new WaitForSeconds(interval);
        }
    }
}
