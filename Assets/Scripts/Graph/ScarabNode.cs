using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ScarabNode : MonoBehaviour
{
	public event Action<ScarabNode> nodeClicked;

	[SerializeField]
	private List<ScarabNode> _neighbours;
	
	public List<ScarabNode> Neighbours => _neighbours;
	public List<ScarabEdge> Edges { get; } = new List<ScarabEdge>();
	public ScarabView View { get; private set; }
	public bool Visited { get; set; }

	public ScarabEdge GetEdgeTo(ScarabNode neighbour)
	{
		return Edges.Find(x => x.HasNodes(neighbour, this));
	}

	public void OnClick()
	{
		nodeClicked?.Invoke(this);
	}

	private void OnEnable()
	{
		Edges.Clear();
		AssignEdges();
		View = GetComponent<ScarabView>();
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
