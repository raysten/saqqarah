using UnityEngine;
using Zenject;
using System;

public class PlayerInput : ITickable
{
	public event Action mouseLeftButtonPressed;

	public float Horizontal => Input.GetAxis("Horizontal");
	public float Vertical => Input.GetAxis("Vertical");
	public float MouseX => Input.GetAxis("Mouse X");
	public float MouseY => Input.GetAxis("Mouse Y");
	public bool LeftMouseButtonDown { get; private set; }

	public void Tick()
	{
		LeftMouseButtonDown = Input.GetMouseButtonDown(0);

		if (LeftMouseButtonDown || Input.GetKeyDown(KeyCode.Space)) // TODO: remove space.
		{
			mouseLeftButtonPressed?.Invoke();
		}
	}
}
