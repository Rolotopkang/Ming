
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SlingshotTrajectory : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform grabPoint;
    public Transform restPosition;
    public float forceMultiplier = 20f;
    public int trajectorySteps = 30;
    public float timeStep = 0.05f;
    public bool isopen = false;

    void Update()
    {
        if (isopen)
        {
            lineRenderer.enabled = true;
            DrawTrajectory();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void DrawTrajectory()
    {
        Vector3 startPos = grabPoint.position;
        Vector3 velocity = (restPosition.position - grabPoint.position) * forceMultiplier;
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < trajectorySteps; i++)
        {
            float t = i * timeStep;
            Vector3 point = startPos + velocity * t + 0.5f * Physics.gravity * t * t;
            points.Add(transform.InverseTransformPoint(point));
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
