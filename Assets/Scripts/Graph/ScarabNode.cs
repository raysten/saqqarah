using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ScarabNode : MonoBehaviour
{
	[SerializeField]
	private List<ScarabNode> _neighbours;
	[SerializeField]
	private Sprite _defaultSprite;
	[SerializeField]
	private Sprite _markedSprite;

	private SpriteRenderer img;

	public List<ScarabNode> Neighbours => _neighbours;
	public List<ScarabEdge> Edges { get; } = new List<ScarabEdge>();

	public ScarabEdge GetEdgeTo(ScarabNode neighbour)
	{
		return Edges.Find(x => x.HasNodes(neighbour, this));
	}

	public void Mark()
	{
		img.sprite = _markedSprite;
	}

	private void OnEnable()
	{
		img = GetComponent<SpriteRenderer>();
		AssignEdges();
	}

	private void AssignEdges()
	{
		foreach (ScarabNode neighbour in _neighbours)
		{
			ScarabEdge edgeFromCurrentNeighbourToThis = neighbour.GetEdgeTo(this);

			if (edgeFromCurrentNeighbourToThis != null)
			{
				Edges.Add(edgeFromCurrentNeighbourToThis);
			}
			else
			{
				Edges.Add(new ScarabEdge(this, neighbour));
			}
		}
	}
}
