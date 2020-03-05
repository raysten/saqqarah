using UnityEngine;
using Zenject;
using System;

public class PlayerInput : ITickable
{
	public event Action mouseLeftButtonPressed;

	public float Horizontal => Input.GetAxis("Horizontal");
	public float Vertical => Input.GetAxis("Vertical");
	public bool LeftMouseButtonDown { get; private set; }

	public void Tick()
	{
		LeftMouseButtonDown = Input.GetMouseButtonDown(0);

		//if (LeftMouseButtonDown) // TODO: temporary space
		if (Input.GetKeyDown(KeyCode.Space))
		{
			mouseLeftButtonPressed?.Invoke();
		}
	}
}
