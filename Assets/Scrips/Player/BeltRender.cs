using UnityEngine;

public class BeltRender : MonoBehaviour
{
    public Transform leftAnchor, rightAnchor, grabPoint;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 3;
    }
    
    
    void Update()
    {
        lineRenderer.SetPosition(0, leftAnchor.localPosition);
        lineRenderer.SetPosition(1, transform.InverseTransformPoint(grabPoint.position));
        lineRenderer.SetPosition(2, rightAnchor.localPosition);
    }
}
