using UnityEngine;
using Zenject;

public class PlayerInput
{
	public float Horizontal => Input.GetAxis("Horizontal");
	public float Vertical => Input.GetAxis("Vertical");
}
