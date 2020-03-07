using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System;

public class PuzzleMessage : ITickable
{
	public event Action messageTimedOut;

	private Text _textBox;

	private bool _timerActive;
	private float _timeToLive;
	private float _timer;

	public PuzzleMessage(Text textBox)
	{
		_textBox = textBox;
	}

	public void Tick()
	{
		UpdateTimer();
	}

	public void DisplayMessage(string message, float duration = -1f)
	{
		if (_timerActive == false)
		{
			_textBox.text = message;
		}

		if (duration > 0f)
		{
			SetTimer(duration);
		}
	}

	private void SetTimer(float duration)
	{
		_timeToLive = duration;
		_timerActive = true;
	}

	private void UpdateTimer()
	{
		if (_timerActive == false)
		{
			return;
		}

		_timer += Time.deltaTime;

		if (_timer >= _timeToLive)
		{
			_timer = 0f;
			_timeToLive = 0f;
			_timerActive = false;
			_textBox.text = "";
			messageTimedOut?.Invoke();
		}
	}

	[Serializable]
	public class Settings
	{
		public string tryAgainMessage = "Try again";
		public string lossMessage = "Puzzle lost";
	}
}
