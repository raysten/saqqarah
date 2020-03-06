using System;
using UnityEngine;
using Zenject;

public class PlayerLookCamera : ITickable
{
	private Transform _transform;
	private Camera _cam;
	private PlayerInput _input;
	private Settings _settings;

	private float _xRotation;

	public PlayerLookCamera(Transform transform, Camera cam, PlayerInput input, Settings settings)
	{
		_transform = transform;
		_cam = cam;
		_input = input;
		_settings = settings;
	}

	public void Tick()
	{
		_xRotation -= _input.MouseY * Time.deltaTime * _settings.mouseYSensitivity;
		_xRotation = Mathf.Clamp(_xRotation, -_settings.verticalClamp, _settings.verticalClamp);
		_cam.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

		_transform.Rotate(Vector3.up * _input.MouseX * Time.deltaTime * _settings.mouseXSensitivity);
	}

	[Serializable]
	public class Settings
	{
		public float mouseXSensitivity = 20f;
		public float mouseYSensitivity = 20f;
		public float verticalClamp = 30f;
	}
}
