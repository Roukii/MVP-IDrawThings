using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(TwitchChat))]
public class TwitchChatUI : MonoBehaviour
{
	public Image Canvas;
	public Text ResponsText, FirstText, SecondText, ThirdText;
	private const int Max = 20;

	private TwitchChat _irc;
	private readonly LinkedList<GameObject> _messages =
		new LinkedList<GameObject>();

	void Start () {
		_irc = GetComponent<TwitchChat>();
		_irc.MessageRecievedEvent.AddListener(OnMsgRecieved);
	}
	
	void OnMsgRecieved(string msg)
	{
		if (_messages.Count >= Max)
		{
			print("Debug msg : " + msg);
			Destroy(_messages.First.Value);
			_messages.RemoveFirst();
		}

		var splitPoint = msg.IndexOf("!", 1, StringComparison.Ordinal);
		var chatName = msg.Substring(0, splitPoint);
		chatName = chatName.Substring(1);

		// Get users messages by splitting it from string
		splitPoint = msg.IndexOf(":", 1, StringComparison.Ordinal);
		msg = msg.Substring(splitPoint + 1);

		// Get name color
		Color32 c = ColorFromUsername(chatName);
		string nameColor = "#" + c.r.ToString("X2") + c.g.ToString("X2") + c.b.ToString("X2");

		// Create game object and layout level
		GameObject go = new GameObject("TwitchMsg");
		var text = go.AddComponent<Text>();
		var layout = go.AddComponent<LayoutElement>();
		go.transform.SetParent(Canvas.transform);
		_messages.AddLast(go);
		layout.minHeight = 7f;
		layout.minWidth = 7f;

		// Set text
		text.text = "<color=" + nameColor + "><b>" + chatName + "</b></color>" + ": " + msg;
		text.color = Color.black;
		text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

		if (msg.ToLower().Equals(ResponsText.text))
		{
			if (FirstText.text == "")
				FirstText.text = chatName;
			else if (SecondText.text == "")
				SecondText.text = chatName;
			else if (ThirdText.text == "")
				ThirdText.text = chatName;
			else
				return;
		}
	}

	Color ColorFromUsername(string username)
	{
		Random.seed = username.Length + username[0] + username[username.Length - 1];
		return new Color(Random.Range(0.25f, 0.55f), Random.Range(0.20f, 0.55f), Random.Range(0.25f, 0.55f));
	}
}
    