using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangePenColor : MonoBehaviour, IPointerClickHandler
{
    private GameObject lineCreator;
    public Color penColor = Color.black;

    void Start()
    {
        lineCreator = GameObject.Find("LineCreator");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        lineCreator.GetComponent<LineCreator>().lineColor = penColor;
    }
}
