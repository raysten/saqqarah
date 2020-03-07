using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PuzzleController : IInitializable, IDisposable
{
	private List<ScarabNode> _nodes;
	private EdgeDrawer _drawer;
	private VictoryHandler _victory;
	private EventBus _eventBus;

	private List<ScarabEdge> _edges = new List<ScarabEdge>();
	private ScarabNode _lastClickedNode = null;

	public PuzzleController(List<ScarabNode> nodes, EdgeDrawer drawer, VictoryHandler victory, EventBus eventBus)
	{
		_nodes = nodes;
		_drawer = drawer;
		_victory = victory;
		_eventBus = eventBus;
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
		_eventBus.victoryScreenComplete -= OnVictoryScreenComplete;
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

	private void OnVictoryScreenComplete()
	{
		ResetPuzzle();
		_eventBus.victoryScreenComplete -= OnVictoryScreenComplete;
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

			if (anyUnmarkedEdge == null)
			{
				_eventBus.puzzleWon?.Invoke();
				_eventBus.victoryScreenComplete += OnVictoryScreenComplete;
			}
			else
			{
				// TODO:
				Debug.Log("LOSS");
			}
		}
	}

	private void ResetPuzzle()
	{
		foreach (ScarabNode node in _nodes)
		{
			node.Reset();
		}

		foreach (ScarabEdge edge in _edges)
		{
			edge.Reset();
		}

		_lastClickedNode = null;
		_drawer.ClearAll();
	}
}
