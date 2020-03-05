using System;
using UnityEngine;
using Zenject;

public class PlayerMovement : ITickable
{
	private CharacterController _controller;
	private PlayerInput _input;
	private Transform _transform;
	private Settings _settings;

	public PlayerMovement(
		CharacterController controller,
		PlayerInput input,
		Transform transform,
		Settings settings
	)
	{
		_controller = controller;
		_input = input;
		_transform = transform;
		_settings = settings;
	}

	public void Tick()
	{
		Vector3 forward = _transform.forward * _input.Vertical * _settings.forwardSpeed;
		Vector3 side = _transform.right * _input.Horizontal * _settings.sideSpeed;
		_controller.Move((forward + side) * Time.deltaTime);
	}

	[Serializable]
	public class Settings
	{
		public float forwardSpeed = 2f;
		public float sideSpeed = 4f;
	}
}
