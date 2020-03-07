using UnityEngine;
using Zenject;
using System;

public class VictoryHandler : IInitializable, IDisposable
{
	private ParticleSystem _winVfx;
	private Animator _victoryText;
	private EventBus _eventBus;

	public VictoryHandler(ParticleSystem winVfx, Animator victoryText, EventBus eventBus)
	{
		_winVfx = winVfx;
		_victoryText = victoryText;
		_eventBus = eventBus;
	}

	public void Initialize()
	{
		VictoryTextIdleStateEnter.victoryAnimationFinished += OnVictoryAnimationFinished;
		_eventBus.puzzleWon += TriggerWin;
	}

	public void Dispose()
	{
		VictoryTextIdleStateEnter.victoryAnimationFinished -= OnVictoryAnimationFinished;
		_eventBus.puzzleWon -= TriggerWin;
	}

	public void TriggerWin()
	{
		_winVfx.Play();
		_victoryText.gameObject.SetActive(true);
		_victoryText.Play("wintext");
	}

	private void OnVictoryAnimationFinished()
	{
		_eventBus.victoryScreenComplete?.Invoke();
	}
}
