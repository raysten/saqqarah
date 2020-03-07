using UnityEngine;
using System;

public class VictoryTextIdleStateEnter : StateMachineBehaviour
{
	public static event Action victoryAnimationFinished;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		victoryAnimationFinished?.Invoke();
		animator.gameObject.SetActive(false);
	}
}
