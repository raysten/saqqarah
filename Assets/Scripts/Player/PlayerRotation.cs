using UnityEngine;
using Zenject;
using System;

public class PlayerRotation : ITickable
{
	private Camera _cam;
	private Transform _transform;
	private Settings _settings;

	public PlayerRotation(Camera cam, Transform transform, Settings settings)
	{
		_cam = cam;
		_transform = transform;
		_settings = settings;
	}

	public void Tick()
	{
		Quaternion targetRotation = Quaternion.Euler(
			_transform.rotation.eulerAngles.x,
			_cam.transform.rotation.eulerAngles.y,
			_transform.rotation.eulerAngles.z
		);
		// TODO: use correct lerping.
		_transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.deltaTime * _settings._speed);
	}

	[Serializable]
	public class Settings
	{
		public float _speed;
	}
}
