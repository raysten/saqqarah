using UnityEngine;

public class ScarabView : MonoBehaviour
{
	[SerializeField]
	private Material _defaultMaterial;
	[SerializeField]
	private Material _blueMaterial;
	[SerializeField]
	private Material _goldenMaterial;

	private MeshRenderer _renderer;
	private Animator _anmtr;

	public void Mark(bool isLast = false)
	{
		if (isLast)
		{
			_renderer.sharedMaterial = _goldenMaterial;
		}
		else
		{
			_renderer.sharedMaterial = _blueMaterial;
		}
	}

	public void Shake()
	{
		_anmtr.Play("scarab_missclick");
	}

	public void Reset()
	{
		_renderer.sharedMaterial = _defaultMaterial;
	}

	private void Awake()
	{
		_renderer = GetComponentInChildren<MeshRenderer>();
		_anmtr = GetComponent<Animator>();
	}
}
