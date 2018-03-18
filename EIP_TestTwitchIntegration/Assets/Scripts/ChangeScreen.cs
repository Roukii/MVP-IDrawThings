using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;

public class ChangeScreen : MonoBehaviour, IPointerClickHandler
{
	public Canvas ConnexionUi, GameUi;
	public GameObject TwitchChatUi, TwitchChat, GameLoop;
	public LineCreator LineCreator;
	
	// Use this for initialization
	void Start () {
		ConnexionUi.gameObject.SetActive(true);
		GameUi.gameObject.SetActive(false);
		TwitchChatUi.gameObject.SetActive(false);
		LineCreator.gameObject.SetActive(false);
		TwitchChat.gameObject.SetActive(false);
		GameLoop.gameObject.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		TwitchChatUi.gameObject.SetActive(true);
		ConnexionUi.gameObject.SetActive(false);
		GameUi.gameObject.SetActive(true);
		LineCreator.gameObject.SetActive(true);
		TwitchChat.gameObject.SetActive(true);
		GameLoop.gameObject.SetActive(true);
	}
}
