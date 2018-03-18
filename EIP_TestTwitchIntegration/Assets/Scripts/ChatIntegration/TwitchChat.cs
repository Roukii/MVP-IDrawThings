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
	
	private string _str = string.Empty;
	private bool _stopThreads = false;
//	private Queue<string> _commandQueue = new Queue<string>();
	private readonly List<string> _recievedMsgs = new List<string>();
	private System.Threading.Thread _inProc;

	//public string username, oauth, channelName;
	public Text Username, Oauth, ChannelName;
	public class MsgEvent : UnityEngine.Events.UnityEvent<string> { }
	public MsgEvent MessageRecievedEvent = new MsgEvent();

	// Use this for initialization
	void Start () {
		Debug.Log("here : " + Username.text + " " + Oauth.text + " " + ChannelName.text);
		Connect();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (_twitchClient == null || !_twitchClient.Connected)
			Connect();
		else
			lock (_recievedMsgs)
			{
				if (_recievedMsgs.Count > 0)
				{
					for (int i = 0; i < _recievedMsgs.Count; i++)
					{
						MessageRecievedEvent.Invoke(_recievedMsgs[i]);
					}
					_recievedMsgs.Clear();
			}
		}
	}

	private void Connect()
	{
		_twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
		_reader = new StreamReader(_twitchClient.GetStream());
		_writer = new StreamWriter(_twitchClient.GetStream());
		
		_writer.WriteLine("PASS " + Oauth.text);
		_writer.WriteLine("NICK " + Username.text);
		_writer.WriteLine("USER " + Username.text + " 8 * :" + Username.text);
		_writer.WriteLine("JOIN #" + ChannelName.text);
		_writer.Flush();

		_inProc = new System.Threading.Thread(GetIrcInput);
		_inProc.Start();
	}

	private void GetIrcInput()
	{
		while (!_stopThreads)
		{
			if (!_twitchClient.GetStream().DataAvailable)
				continue ;
			
			_str = _reader.ReadLine();
			
			if (_str != null && !_str.Contains("PRIVMSG #")) continue;
			lock (_recievedMsgs)
				_recievedMsgs.Add(_str);
		}
	}

	void OnEnable()
	{
		_stopThreads = false;
	}

	void OnDisable()
	{
		_stopThreads = true;
	}

	void OnDestroy()
	{
		_stopThreads = true;
	}
}
