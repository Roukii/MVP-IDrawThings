using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(TwitchChat))]
public class TwitchChatUI : MonoBehaviour
{

	public Transform chatBox;
	private int max = 100;
	private TwitchChat IRC;
	private LinkedList<GameObject> messages =
		new LinkedList<GameObject>();

	void Start () {
		IRC = GetComponent<TwitchChat>();
		IRC.messageRecievedEvent.AddListener(OnMsgRecieved);
	}
	
	void OnMsgRecieved(string msg)
	{
		if (messages.Count > max)
		{
			Destroy(messages.First.Value);
			messages.RemoveFirst();
		}

		var splitPoint = msg.IndexOf("!", 1, StringComparison.Ordinal);
		var chatName = msg.Substring(0, splitPoint);
		chatName = chatName.Substring(1);
				
/*
		// Get users messages by splitting it from string
		splitPoint = msg.IndexOf(":", 1, StringComparison.Ordinal);
		msg = msg.Substring(splitPoint + 1);
		print(String.Format("{0}: {1}", chatName, msg));
		chatBox.text = chatBox.text + "\n" + String.Format("{0}: {1}", chatName, msg);
		print(msg);
*/
		Color32 c = ColorFromUsername(chatName);
		string nameColor = "#" + c.r.ToString("X2") + c.g.ToString("X2") + c.b.ToString("X2");
		GameObject go = new GameObject("TwitchMsg");
		var text = go.AddComponent<UnityEngine.UI.Text>();
		var layout = go.AddComponent<UnityEngine.UI.LayoutElement>();
		go.transform.SetParent(chatBox);
		messages.AddLast(go);

		layout.minHeight = 20f;
		text.text = "<color=" + nameColor + "><b>" + chatName + "</b></color>" + ": " + msg;
		text.color = Color.black;
		text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
	}

	Color ColorFromUsername(string username)
	{
		Random.seed = username.Length + (int)username[0] + (int)username[username.Length - 1];
		return new Color(Random.Range(0.25f, 0.55f), Random.Range(0.20f, 0.55f), Random.Range(0.25f, 0.55f));
	}
}
