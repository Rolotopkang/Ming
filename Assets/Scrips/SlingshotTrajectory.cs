
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SlingshotTrajectory : MonoBehaviour
{
    public Transform grabPoint;

    public int trajectorySteps = 30;
    public float timeStep = 0.05f;
    public bool isopen = false;
    
    public GameObject lineRendererPrefab;
    public GameObject RendererRoot;
    private List<LineRenderer> trajectoryLines = new List<LineRenderer>();

    void Update()
    {
        if (isopen)
        {
            RendererRoot.SetActive(true);
            DrawTrajectory();
        }
        else
        {
            RendererRoot.SetActive(false);
        }
    }
    
    void DrawTrajectory()
    {
        int bulletCount = WeaponController.GetInstance().bulletCount;
        float spreadAngle = WeaponController.GetInstance().spreadAngle;
        float forceMultiplier = WeaponController.GetInstance().forceMultiplier;

        Vector3 startPos = grabPoint.position;
        Vector3 baseVelocity = WeaponController.GetInstance().lunchVector3() * forceMultiplier;

        // **保证对象池里有足够的 LineRenderer**
        while (trajectoryLines.Count < bulletCount)
        {
            GameObject lineObj = Instantiate(lineRendererPrefab, RendererRoot.transform, true);
            lineObj.name = "TrajectoryLine_" + trajectoryLines.Count;
            LineRenderer lr = lineObj.GetComponent<LineRenderer>();
            trajectoryLines.Add(lr);
        }

        // **更新子弹轨迹**
        for (int b = 0; b < trajectoryLines.Count; b++)
        {
            LineRenderer lr = trajectoryLines[b];

            if (b < bulletCount)
            {
                lr.gameObject.SetActive(true); // **确保启用**
            
                // **计算散射角**
                float angleOffset = (bulletCount == 1) ? 0f : Mathf.Lerp(-spreadAngle / 2, spreadAngle / 2, (float)b / (bulletCount - 1));
                Quaternion rotation = Quaternion.AngleAxis(angleOffset, transform.up);
                Vector3 velocity = rotation * baseVelocity;

                // **计算弹道轨迹**
                List<Vector3> points = new List<Vector3>();
                for (int i = 0; i < trajectorySteps; i++)
                {
                    float t = i * timeStep;
                    Vector3 point = startPos + velocity * t + 0.5f * Physics.gravity * t * t;
                    points.Add(lr.transform.InverseTransformPoint(point));
                }

                // **更新 LineRenderer**
                lr.positionCount = points.Count;
                lr.SetPositions( points.ToArray());
            }
            else
            {
                // **多余的 LineRenderer 隐藏**
                lr.positionCount = 0;
                lr.gameObject.SetActive(false);
            }
        }
    }





    // void DrawTrajectory()
    // {
    //     Vector3 startPos = grabPoint.position;
    //     Vector3 velocity = WeaponController.GetInstance().lunchVector3 * WeaponController.GetInstance().forceMultiplier;
    //     List<Vector3> points = new List<Vector3>();
    //
    //     for (int i = 0; i < trajectorySteps; i++)
    //     {
    //         float t = i * timeStep;
    //         Vector3 point = startPos + velocity * t + 0.5f * Physics.gravity * t * t;
    //         points.Add(transform.InverseTransformPoint(point));
    //     }
    //
    //     lineRenderer.positionCount = points.Count;
    //     lineRenderer.SetPositions(points.ToArray());
    // }
}
