using UnityEngine;
using Zenject;
using System;
using UnityEngine.UI;

public class PlayerTargeter : IInitializable, IDisposable
{
	private Camera _cam;
	private PlayerInput _input;
	private Settings _settings;

	public PlayerTargeter(Camera cam, PlayerInput input, Settings settings)
	{
		_cam = cam;
		_input = input;
		_settings = settings;
	}

	public void Initialize()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		_input.mouseLeftButtonPressed += OnLeftClick;
	}

	public void Dispose()
	{
		_input.mouseLeftButtonPressed -= OnLeftClick;
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
			Debug.Log($"Hit: {hitInfo.transform.name}");
		}
	}

	[Serializable]
	public class Settings
	{
		public LayerMask leftClickDetectionLayer;
		public float raycastDistance = 100f;
	}
}
