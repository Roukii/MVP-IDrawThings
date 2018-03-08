using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;
using UnityEditor.VersionControl;
using UnityEngine.UI;

public class TwitchChat : MonoBehaviour
{
	private TcpClient _twitchClient;
	private StreamReader _reader;
	private StreamWriter _writer;

	public string username, oauth, channelName;
	public Text chatBox;

	// Use this for initialization
	void Start () {
		Connect();
	}
	
	// Update is called once per frame
	void Update () {
		if (!_twitchClient.Connected)
		{
			Connect();
		}
		ReadChat();
	}

	private void Connect()
	{
		_twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
		_reader = new StreamReader(_twitchClient.GetStream());
		_writer = new StreamWriter(_twitchClient.GetStream());
		
		_writer.WriteLine("PASS " + oauth);
		_writer.WriteLine("NICK " + username);
		_writer.WriteLine("USER " + username + " 8 * :" + username);
		_writer.WriteLine("JOIN #" + channelName);
		_writer.Flush();
	}

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
}
