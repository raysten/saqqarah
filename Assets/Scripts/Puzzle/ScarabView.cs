using UnityEngine;

public class ScarabView : MonoBehaviour
{
	[SerializeField]
	private Sprite _defaultSprite;
	[SerializeField]
	private Sprite _markedSprite;
	[SerializeField]
	private Sprite _lastMarkedSprite;

	private SpriteRenderer _img;
	private Animator _anmtr;

	public void Mark(bool isLast = false)
	{
		if (isLast)
		{
			_img.sprite = _lastMarkedSprite;
		}
		else
		{
			_img.sprite = _markedSprite;
		}
	}

	public void Shake()
	{
		_anmtr.Play("scarab_missclick");
	}

	public void Reset()
	{
		_img.sprite = _defaultSprite;
	}

	private void Awake()
	{
		_img = GetComponent<SpriteRenderer>();
		_anmtr = GetComponent<Animator>();
	}
}
