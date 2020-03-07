using System;
using System.Collections.Generic;
using Zenject;

public class PuzzleController : IInitializable, IDisposable
{
	private List<ScarabNode> _nodes;
	private EdgeDrawer _drawer;
	private VictoryHandler _victory;
	private EventBus _eventBus;
	private PuzzleMessage _messages;
	private PuzzleMessage.Settings _messageSettings;

	private List<ScarabEdge> _edges = new List<ScarabEdge>();
	private ScarabNode _lastClickedNode = null;
	private int _attemptsCounter;

	public PuzzleController(
		List<ScarabNode> nodes,
		EdgeDrawer drawer,
		VictoryHandler victory,
		EventBus eventBus,
		PuzzleMessage messages,
		PuzzleMessage.Settings messageSettings
	)
	{
		_nodes = nodes;
		_drawer = drawer;
		_victory = victory;
		_eventBus = eventBus;
		_messages = messages;
		_messageSettings = messageSettings;
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
		_messages.messageTimedOut -= ResetPuzzle;
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
				HandleLoss();
			}
		}
	}

	private void HandleLoss()
	{
		_attemptsCounter++;

		if (_attemptsCounter == 1)
		{
			_messages.DisplayMessage(_messageSettings.tryAgainMessage, 2f);
			_messages.messageTimedOut += ResetPuzzle;
		}
		else
		{
			_messages.DisplayMessage(_messageSettings.lossMessage);
			ResetPuzzle();
			DisablePuzzle();
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

	private void DisablePuzzle()
	{
		foreach (ScarabNode node in _nodes)
		{
			node.Disable();
		}
	}
}
