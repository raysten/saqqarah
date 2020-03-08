using UnityEngine;
using System;
using Zenject;

public class ScarabView : MonoBehaviour
{
	[SerializeField]
	private Texture2D _brownTexture;
	[SerializeField]
	private Texture2D _blueTexture;
	[SerializeField]
	private Texture2D _goldTexture;

	[Inject]
	private Settings _settings;

	private MeshRenderer _renderer;
	private Animator _anmtr;
	private MaterialPropertyBlock _propertyBlock;
	private float _blend;
	private Texture2D _blendTargetTexture;

	public void Mark(bool isLast = false)
	{
		if (isLast)
		{
			BlendTexture(_goldTexture);
		}
		else
		{
			BlendTexture(_blueTexture);
		}
	}

	public void Shake()
	{
		_anmtr.Play("scarab_missclick");
	}

	public void Reset()
	{
		BlendTexture(_brownTexture);
	}

	private void Awake()
	{
		_renderer = GetComponentInChildren<MeshRenderer>();
		_anmtr = GetComponent<Animator>();
		_propertyBlock = new MaterialPropertyBlock();
		enabled = false;
	}

	private void Update()
	{
		UpdateBlending();
	}

	private void BlendTexture(Texture2D targetTexture)
	{
		enabled = true;
		_blendTargetTexture = targetTexture;
		_renderer.GetPropertyBlock(_propertyBlock);
		_propertyBlock.SetTexture("_SecTex", _blendTargetTexture);
		_renderer.SetPropertyBlock(_propertyBlock);
	}

	private void UpdateBlending()
	{
		_renderer.GetPropertyBlock(_propertyBlock);
		_blend = Mathf.Clamp(_blend + Time.deltaTime * _settings.blendSpeed, 0f, 1f);
		_propertyBlock.SetFloat("_Blend", _blend);
		_renderer.SetPropertyBlock(_propertyBlock);

		if (_blend == 1f)
		{
			OnBlendingFinished();
		}
	}

	private void OnBlendingFinished(bool isReset = false)
	{
		enabled = false;
		_renderer.GetPropertyBlock(_propertyBlock);
		_blend = 0f;
		_propertyBlock.SetFloat("_Blend", 0f);
		_propertyBlock.SetTexture("_MainTex", _blendTargetTexture);
		_renderer.SetPropertyBlock(_propertyBlock);
	}

	[Serializable]
	public class Settings
	{
		public float blendSpeed = 2f;
	}
}
