using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
	public Text TimerText, WordText;
	public GameObject RankingPanel;
	public Text FirstText, SecondText, ThirdText;

	private float _startTime;
	private bool _gameState;
	private float _timeGame;
	private readonly string[] _wordStrings = { "maison", "fleur", "chaise", "argent", "fraise"};

	// Use this for initialization
	void Start ()
	{
		_startTime = Time.time;
		_gameState = true;
		_timeGame = 60f;
		RankingPanel.gameObject.SetActive(false);
		WordText.text = "";
	}
	
	// Update is called once per frame
	void Update ()
	{
		var currTime = Time.time - _startTime;
		var timeLeft = _timeGame - currTime;

		if (_gameState == false)
			PauseState(timeLeft);
		else
			GameState(timeLeft);
	}

	void PauseState(float timeLeft)
	{
		if (!(timeLeft <= 0))
			return;
		_gameState = true;
		_timeGame = 60f;
		_startTime = Time.time;
		RankingPanel.gameObject.SetActive(false);
		WordText.text = _wordStrings[Random.Range(0, 4)];
		FirstText.text = "";
		SecondText.text = "";
		ThirdText.text = "";
	}

	void GameState(float timeLeft)
	{
		string seconds = (timeLeft % 60).ToString("f0");
		TimerText.text = seconds;

		if (!(timeLeft <= 0))
			return;
		_gameState = false;
		_timeGame = 10f;
		_startTime = Time.time;
		RankingPanel.gameObject.SetActive(true);
		WordText.text = "";
		TimerText.text = "";
	}
}