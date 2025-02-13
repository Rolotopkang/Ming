using System;
using UnityEngine;

public class UI_Base : MonoBehaviour
{
    public Transform target;
    public Vector3 trueOffset = new Vector3(0, 0.1f, 0);
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public float minScale = 0.5f;
    public float maxScale = 2f;
    public float minDistance = 1f;
    public float maxDistance = 5f;
    public float followSmoothTime = 0.2f;
    public float rotateSmoothTime = 0.2f;
    
    
    private Transform player;
    private Vector3 velocity = Vector3.zero;

    protected virtual void Start()
    {
        target = transform.parent;
        if (Camera.main != null) player = Camera.main.transform;
        UpdateUI();
    }
    
    protected virtual void Update()
    {
        UpdatePos();
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    protected virtual void UpdatePos()
    {
        if (target == null || player == null) return;
        float distance = Vector3.Distance(player.position, transform.position);
        float scaleFactor = Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));
        
        Vector3 targetPos = target.position + offset*scaleFactor + trueOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, followSmoothTime);
        Vector3 forward = transform.position - player.position;
        forward.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(forward, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime / rotateSmoothTime);
        float scale = Mathf.Lerp(minScale, maxScale, scaleFactor);
        transform.localScale = Vector3.one * scale;
    }

    public virtual void UpdateUI()
    {
    }
}
