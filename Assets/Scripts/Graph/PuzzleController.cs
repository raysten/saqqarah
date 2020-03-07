using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PuzzleController : IInitializable, IDisposable
{
	private List<ScarabNode> _nodes;
	private EdgeDrawer _drawer;

	private List<ScarabEdge> _edges = new List<ScarabEdge>();
	private ScarabNode _lastClickedNode = null;

	public PuzzleController(List<ScarabNode> nodes, EdgeDrawer drawer)
	{
		_nodes = nodes;
		_drawer = drawer;
	}

	#region Initialization and Disposal
	public void Initialize()
	{
		CreateEdges();
		SubscribeClick();
	}

	public void Dispose()
	{
		UnsubscribeClick();
	}

	private void SubscribeClick()
	{
		foreach (ScarabNode node in _nodes)
		{
			node.nodeClicked += OnNodeClicked;
		}
	}

	private void UnsubscribeClick()
	{
		foreach (ScarabNode node in _nodes)
		{
			node.nodeClicked -= OnNodeClicked;
		}
	}
	#endregion

	private void CreateEdges()
	{
		foreach (ScarabNode node in _nodes)
		{
			_edges.AddRange(node.AssignEdges());
		}
	}

	private void OnNodeClicked(ScarabNode node)
	{
		if (IsValidClick(node))
		{
			VisitNode(node);
			CheckWinCondition();
		}
		else
		{
			node.View.Shake();
		}

	}

	private bool IsValidClick(ScarabNode node)
	{
		if (_lastClickedNode == null)
		{
			return true;
		}

		ScarabEdge edge = node.GetEdgeTo(_lastClickedNode);

		if (edge != null && edge.IsMarked == false)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private void VisitNode(ScarabNode node)
	{
		if (_lastClickedNode == null)
		{
			ColorNode(node, true);
		}
		else
		{
			ColorNode(node);
			ScarabEdge edge = node.GetEdgeTo(_lastClickedNode);
			edge.IsMarked = true;
			_drawer.DrawEdge(_lastClickedNode, node);
		}

		node.Visited = true;
		_lastClickedNode = node;
	}

	private void ColorNode(ScarabNode node, bool isFirst = false)
	{
		if (node.Visited == false)
		{
			node.View.Mark(isFirst);
		}
	}

	private void CheckWinCondition()
	{
		if (_lastClickedNode.HasUnmarkedEdge() == false)
		{
			ScarabEdge anyUnmarkedEdge = _edges.Find(x => x.IsMarked == false);

			// TODO:
			if (anyUnmarkedEdge == null)
			{
				Debug.Log("WIN");
			}
			else
			{
				Debug.Log("LOSS");
			}
		}
	}
}
