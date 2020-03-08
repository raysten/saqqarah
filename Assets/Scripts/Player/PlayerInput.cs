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
		bool debugSpace = false;

#if UNITY_EDITOR
		debugSpace = Input.GetKeyDown(KeyCode.Space);
#endif

		if (LeftMouseButtonDown || debugSpace)
		{
			mouseLeftButtonPressed?.Invoke();
		}
	}
}
