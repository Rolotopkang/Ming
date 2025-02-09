using UnityEngine;

public class ItemDescriptionUI : MonoBehaviour
{
    public Transform target;         // 目标物体（UI 悬浮在哪个物体上）
    public Transform player;         // 玩家（用来计算距离）
    public Vector3 offset = new Vector3(0, 0.5f, 0); // UI 相对于目标的偏移量
    public float minScale = 0.5f;    // UI 最小缩放
    public float maxScale = 2f;      // UI 最大缩放
    public float minDistance = 1f;   // 最近的缩放距离
    public float maxDistance = 5f;   // 最远的缩放距离
    public float followSmoothTime = 0.2f;  // 位置平滑时间
    public float rotateSmoothTime = 0.2f;  // 旋转平滑时间

    private Vector3 velocity = Vector3.zero;

    public void ItemDescriptionUIRegister()
    {
        
    }
    
    void Update()
    {
        if (target == null || player == null) return;
        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, followSmoothTime);
        Vector3 forward = transform.position - player.position;
        forward.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(forward, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime / rotateSmoothTime);
        float distance = Vector3.Distance(player.position, transform.position);
        float scaleFactor = Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));
        float scale = Mathf.Lerp(minScale, maxScale, scaleFactor);
        transform.localScale = Vector3.one * scale;
    }
}