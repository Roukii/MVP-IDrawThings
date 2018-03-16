using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    public GameObject linePrefab;
    public float penSize = 0.5f;
    public Color lineColor = Color.black;

    Line activeLine;
    LineRenderer activeLineRenderer;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Update ()
    {
		if (Input.GetMouseButtonDown(0))
        {
            GameObject lineGO = Instantiate(linePrefab);
            activeLine = lineGO.GetComponent<Line>();
            activeLineRenderer = activeLine.GetComponent<LineRenderer>();
            activeLineRenderer.startWidth = penSize;
            activeLineRenderer.endWidth = penSize;
            activeLineRenderer.startColor = lineColor;
            activeLineRenderer.endColor = lineColor;
        }

        if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
        }

        if (activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }
    }
}
