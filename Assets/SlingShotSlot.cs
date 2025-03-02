using UnityEngine;

public class SlingShotSlot : MonoBehaviour
{
    public Transform playerCamera; // 代表玩家头部的摄像机
    public Transform Slingshot;      // 弹药袋的Transform
    public Vector3 relativePosition; // 弹药袋相对于摄像机或玩家身体的偏移量
    public float waistHeight = 0.9f; // 假设的玩家腰部离地高度，依据实际情况调整
    public float rotationRadius = 0.3f;
    public float angleOffset = 90f;
    void Update()
    {
        // 计算摄像机的水平面方向
        Vector3 cameraForward = playerCamera.forward;
        cameraForward.y = 0; // 忽略上下角度，仅考虑水平方向
        cameraForward.Normalize();
    
        // 计算绕摄像机的旋转点
        Vector3 cameraRight = Vector3.Cross(Vector3.up, cameraForward).normalized;
        Vector3 offset = (cameraForward * Mathf.Cos(angleOffset * Mathf.Deg2Rad) +
                          cameraRight * Mathf.Sin(angleOffset * Mathf.Deg2Rad)) * rotationRadius;

        // 设置弹药袋的位置
        Vector3 waistPosition = playerCamera.position - new Vector3(0, waistHeight, 0);
        Slingshot.position = waistPosition + offset + relativePosition;
        transform.rotation = Quaternion.LookRotation(playerCamera.position - transform.position);
    }
}
