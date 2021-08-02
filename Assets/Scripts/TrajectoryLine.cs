using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryLine : MonoBehaviour
{
    public LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void RenderLine(Vector3 startPoint, Vector3 endPoint)
    {
        // Position Count is set to 2, a beginning and end point, so that we can see it
        // (0 means it has no points and therefore can't be seen)
        lineRenderer.positionCount = 2;

        // An array to store the locations of the 2 points between which the line will be rendered
        Vector3[] points = new Vector3[2];
        points[0] = startPoint;
        points[1] = endPoint;

        lineRenderer.SetPositions(points);

        // Make the line appear on top of the background shader
        lineRenderer.sortingOrder = 10;
    }

    public void EndLine()
    {
        // remove the two points of the line, i.e. removing the line
        lineRenderer.positionCount = 0;
    }
}
