using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Eraser : MonoBehaviour, IPointerClickHandler
{
    private GameObject lineCreator;

    void Start()
    {
        lineCreator = GameObject.Find("LineCreator");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        lineCreator.GetComponent<LineCreator>().lineColor = Color.white;
    }
}
