using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangePencilSize : MonoBehaviour, IPointerClickHandler
{
    private GameObject lineCreator;
    public float penSize = 1;

	void Start ()
    {
        lineCreator = GameObject.Find("LineCreator");
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        lineCreator.GetComponent<LineCreator>().penSize = penSize;
    }
}
