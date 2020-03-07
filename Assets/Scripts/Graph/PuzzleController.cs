using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PuzzleController : IInitializable, IDisposable
{
	private List<ScarabNode> _nodes;
	private List<ScarabNode> _markedNodes = new List<ScarabNode>();
	private ScarabNode _lastClickedNode = null;

	public PuzzleController(List<ScarabNode> nodes)
	{
		_nodes = nodes;
	}

	#region Initialization and Disposal
	public void Initialize()
	{
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

	private void OnNodeClicked(ScarabNode node)
	{
		if (IsValidClick(node))
		{
			VisitNode(node);
		}
	}

	private bool IsValidClick(ScarabNode node)
	{
		if (_lastClickedNode == null)
		{
			return true;
		}

		return true; // TODO: Disallow marking through already used edge.
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
}
