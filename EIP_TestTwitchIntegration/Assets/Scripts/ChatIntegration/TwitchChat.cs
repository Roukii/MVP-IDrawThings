using UnityEngine;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;

public class TwitchChat : MonoBehaviour
{
	private TcpClient _twitchClient;
	private StreamReader _reader;
	private StreamWriter _writer;

	//public string username, oauth, channelName;
	public Text username, oauth, channelName;
	public class MsgEvent : UnityEngine.Events.UnityEvent<string> { }
	public MsgEvent messageRecievedEvent = new MsgEvent();

	private string str = string.Empty;
	private bool stopThreads = false;
	private Queue<string> commandQueue = new Queue<string>();
	private List<string> recievedMsgs = new List<string>();
	private System.Threading.Thread inProc;

	// Use this for initialization
	void Start () {
		Debug.Log("Start twitch chat");

		Debug.Log(username.text + " " + oauth.text + " " + channelName.text);
		Connect();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (_twitchClient == null || !_twitchClient.Connected)
			Connect();
		else
			lock (recievedMsgs)
			{
				if (recievedMsgs.Count > 0)
				{
					for (int i = 0; i < recievedMsgs.Count; i++)
					{
						messageRecievedEvent.Invoke(recievedMsgs[i]);
					}
					recievedMsgs.Clear();
			}
		}
	}

	private void Connect()
	{
		_twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
		_reader = new StreamReader(_twitchClient.GetStream());
		_writer = new StreamWriter(_twitchClient.GetStream());
		
		_writer.WriteLine("PASS " + oauth.text);
		_writer.WriteLine("NICK " + username.text);
		_writer.WriteLine("USER " + username.text + " 8 * :" + username.text);
		_writer.WriteLine("JOIN #" + channelName.text);
		_writer.Flush();

		inProc = new System.Threading.Thread(GetIRCInput);
		inProc.Start();
	}

	private void GetIRCInput()
	{
		while (!stopThreads)
		{
			if (!_twitchClient.GetStream().DataAvailable)
				continue ;
			
			str = _reader.ReadLine();
			
			if (!str.Contains("PRIVMSG #")) continue;
			lock (recievedMsgs)
				recievedMsgs.Add(str);
		}
	}

	void OnEnable()
	{
		stopThreads = false;
	}

	void OnDisable()
	{
		stopThreads = true;
	}

	void OnDestroy()
	{
		stopThreads = true;
	}

/*
	private void ReadChat()
	{
		if (_twitchClient.Available > 0)
		{
			var msg = _reader.ReadLine();

			if (msg.Contains("PRIVMSG"))
			{
				// GET USERS NAME
				var splitPoint = msg.IndexOf("!", 1, StringComparison.Ordinal);
				var chatName = msg.Substring(0, splitPoint);
				chatName = chatName.Substring(1);
				
				// Get users messages by splitting it from string
				splitPoint = msg.IndexOf(":", 1, StringComparison.Ordinal);
				msg = msg.Substring(splitPoint + 1);
				print(String.Format("{0}: {1}", chatName, msg));
				chatBox.text = chatBox.text + "\n" + String.Format("{0}: {1}", chatName, msg);
			}
			print(msg);
		}
	}
*/
}
