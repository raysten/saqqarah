using UnityEngine;

public class ScarabView : MonoBehaviour
{
	[SerializeField]
	private Sprite _defaultSprite;
	[SerializeField]
	private Sprite _markedSprite;

	private SpriteRenderer img;

	public void Mark()
	{
		img.sprite = _markedSprite;
	}

	private void Awake()
	{
		img = GetComponent<SpriteRenderer>();
	}
}
