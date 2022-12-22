using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryProjection : MonoBehaviour
{
    PlayerController playerController;
    LineRenderer lineRenderer;

    // Number of points on the line
    public int numPoints = 50;

    // Distance between those points on the line
    public float timeBetweenPoints = 0.1f;

    // The physics layers that will cause the line to stop being drawn
    public LayerMask CollidableLayers;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = (int)numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = Vector3.up; //playerController.launchPositionVector;
        Vector3 startingVelocity = Vector3.up; //playerController.launchVelocityVector;
        for (float t = 0; t < numPoints; t += timeBetweenPoints)
        {
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
            points.Add(newPoint);

            if (Physics.OverlapSphere(newPoint, 2, CollidableLayers).Length > 0)
            {
                lineRenderer.positionCount = points.Count;
                break;
            }
        }

        lineRenderer.SetPositions(points.ToArray());
    }
}
