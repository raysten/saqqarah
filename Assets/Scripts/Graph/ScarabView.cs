using UnityEngine;

public class ScarabView : MonoBehaviour
{
	[SerializeField]
	private Sprite _defaultSprite;
	[SerializeField]
	private Sprite _markedSprite;
	[SerializeField]
	private Sprite _firstMarkedSprite;

	private SpriteRenderer img;

	public void Mark(bool isFirst = false)
	{
		if (isFirst)
		{
			img.sprite = _firstMarkedSprite;
		}
		else
		{
			img.sprite = _markedSprite;
		}
	}

	private void Awake()
	{
		img = GetComponent<SpriteRenderer>();
	}
}
