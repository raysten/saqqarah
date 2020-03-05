using UnityEngine;
using Zenject;
using System;
using UnityEngine.UI;

public class PlayerTargeter : IInitializable, IDisposable, ITickable
{
	private Camera _cam;
	private PlayerInput _input;
	private Settings _settings;
	private Image _aim;

	public PlayerTargeter(Camera cam, PlayerInput input, Settings settings, Image aim)
	{
		_cam = cam;
		_input = input;
		_settings = settings;
		_aim = aim;
	}

	public void Initialize()
	{
		_input.mouseLeftButtonPressed += OnLeftClick;
	}

	public void Dispose()
	{
		_input.mouseLeftButtonPressed -= OnLeftClick;
	}

	public void Tick()
	{
		if (
			Physics.Raycast(
			_cam.transform.position,
			_cam.transform.forward,
			out RaycastHit hitInfo,
			_settings.raycastDistance,
			_settings.wallLayer)
		)
		{
			_aim.rectTransform.anchoredPosition = _cam.WorldToScreenPoint(hitInfo.point);
		}
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
		public LayerMask wallLayer;
		public float raycastDistance = 100f;
	}
}
