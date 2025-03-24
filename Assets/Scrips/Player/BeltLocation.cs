using System;
using UnityEngine;

public class BeltLocation : MonoBehaviour
{
    public Transform playerCamera; // 代表玩家头部的摄像机
    public float waistHeight = 0.9f; // 假设的玩家腰部离地高度，依据实际情况调整

    private void Update()
    {
        Quaternion cameraRotation = playerCamera.rotation;
        Vector3 euler = cameraRotation.eulerAngles; // 转换为欧拉角
        transform.rotation = Quaternion.Euler(0, euler.y, 0); // 只应用Y轴旋转
        transform.position = playerCamera.position - new Vector3(0, waistHeight, 0);
    }
}
