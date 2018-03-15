using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class VersionTextScript : MonoBehaviour {

	public Text             _version;

	// Use this for initialization
	void Start () {
		//string text = System.IO.File.ReadAllText(@"C:\Users\Public\TestFolder\WriteText.txt");
		string text = System.IO.File.ReadAllText("Assets/Files/version.txt");
		//_version.text = "TEST";
		_version.text = text;
	}
}
