using UnityEngine;
using Zenject;
using System;
using UnityEngine.UI;

public class PlayerTargeter : IInitializable, IDisposable
{
	private Camera _cam;
	private PlayerInput _input;
	private EventBus _eventBus;
	private Image _aim;
	private Settings _settings;

	public PlayerTargeter(
		Camera cam,
		PlayerInput input,
		EventBus eventBus,
		Image aim,
		Settings settings
	)
	{
		_cam = cam;
		_input = input;
		_eventBus = eventBus;
		_aim = aim;
		_settings = settings;
	}

	public void Initialize()
	{
#if !UNITY_EDITOR
		Cursor.lockState = CursorLockMode.Locked;
#endif
		Cursor.visible = false;
		_input.mouseLeftButtonPressed += OnLeftClick;
		_eventBus.puzzleWon += OnPuzzleWon;
		_eventBus.victoryScreenComplete += OnVictoryScreenComplete;
	}

	public void Dispose()
	{
		_input.mouseLeftButtonPressed -= OnLeftClick;
		_eventBus.puzzleWon -= OnPuzzleWon;
		_eventBus.victoryScreenComplete -= OnVictoryScreenComplete;
	}

	private void OnLeftClick()
	{
		if (
			Physics.Raycast(
			_cam.transform.position,
			_cam.transform.forward,
			out RaycastHit hitInfo,
			_settings.raycastDistance,
			_settings.leftClickDetectionLayer)
		)
		{
			ScarabNode node = hitInfo.transform.GetComponent<ScarabNode>();
			node?.OnClick();
		}
	}

	private void OnPuzzleWon()
	{
		_input.mouseLeftButtonPressed -= OnLeftClick;
		_aim.enabled = false;
	}

	private void OnVictoryScreenComplete()
	{
		_input.mouseLeftButtonPressed += OnLeftClick;
		_aim.enabled = true;
	}

	[Serializable]
	public class Settings
	{
		public LayerMask leftClickDetectionLayer;
		public float raycastDistance = 100f;
	}
}
