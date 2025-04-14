using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VerticalLine : MonoBehaviour
{
    public float lineLength = 10f;
    public float lineWidth = 0.05f;
    public Color lineColor = Color.green;

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }

    void Update()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.up * lineLength;

        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}