using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

    public LineRenderer lineRenderer;

    List<Vector2> points;

    public void UpdateLine(Vector2 mousePos)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(mousePos);
            return;
        }

        if (Vector2.Distance(points.Last(), mousePos) > .1f)
            SetPoint(mousePos);
    }

    void SetPoint(Vector2 mousePos)
    {
        points.Add(mousePos);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, mousePos) ;
    }
}
