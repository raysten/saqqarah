using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ScarabNode : MonoBehaviour
{
	public event Action<ScarabNode> nodeClicked;

	[SerializeField]
	private List<ScarabNode> _neighbours;
	private bool _isDisabled;
	
	public List<ScarabNode> Neighbours => _neighbours;
	public List<ScarabEdge> Edges { get; } = new List<ScarabEdge>();
	public ScarabView View { get; private set; }
	public bool Visited { get; set; }

	public List<ScarabEdge> AssignEdges()
	{
		List<ScarabEdge> newEdges = new List<ScarabEdge>();

		foreach (ScarabNode neighbour in _neighbours)
		{
			ScarabEdge edgeFromCurrentNeighbourToThis = neighbour.GetEdgeTo(this);

			if (edgeFromCurrentNeighbourToThis != null)
			{
				Edges.Add(edgeFromCurrentNeighbourToThis);
			}
			else
			{
				ScarabEdge newEdge = new ScarabEdge(this, neighbour);
				Edges.Add(newEdge);
				newEdges.Add(newEdge);
			}
		}

		return newEdges;
	}

	public ScarabEdge GetEdgeTo(ScarabNode neighbour)
	{
		return Edges.Find(x => x.HasNodes(neighbour, this));
	}

	public bool HasUnmarkedEdge()
	{
		return Edges.Find(x => x.IsMarked == false) != null;
	}

	public void OnClick()
	{
		if (_isDisabled == false)
		{
			nodeClicked?.Invoke(this);	
		}
	}

	public void Reset()
	{
		Visited = false;
		View.Reset();
	}

	public void Disable()
	{
		_isDisabled = true;
	}

	private void OnEnable()
	{
		Edges.Clear();

		if (Application.isPlaying == false)
		{
			AssignEdges();
		}

		View = GetComponent<ScarabView>();
	}
}
