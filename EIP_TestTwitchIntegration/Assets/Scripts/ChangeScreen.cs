using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeScreen : MonoBehaviour, IPointerClickHandler
{
	public Canvas connexion, chat;
	public GameObject twitchChat;

	// Use this for initialization
	void Start () {
		connexion.gameObject.SetActive(true);
		chat.gameObject.SetActive(false);
		twitchChat.gameObject.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		twitchChat.gameObject.SetActive(true);
		connexion.gameObject.SetActive(false);
		chat.gameObject.SetActive(true);
	}
}
