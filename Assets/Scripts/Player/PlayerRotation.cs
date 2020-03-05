using UnityEngine;
using Zenject;
using System;

public class PlayerRotation : ITickable
{
	private Camera _cam;
	private Transform _transform;
	private Settings _settings;

	// TODO: remove settings if not needed eventually.
	public PlayerRotation(Camera cam, Transform transform, Settings settings)
	{
		_cam = cam;
		_transform = transform;
		_settings = settings;
	}

	public void Tick()
	{
		_transform.rotation = Quaternion.Euler(
			_transform.rotation.eulerAngles.x,
			_cam.transform.rotation.eulerAngles.y,
			_transform.rotation.eulerAngles.z
		);
	}

	[Serializable]
	public class Settings
	{
		public float speed = 5f;
	}
}
