using System.Collections.Generic;

public class EdgeDrawer
{
	private EdgeView.Factory _edgeFactory;
	private List<EdgeView> _edges = new List<EdgeView>();
	private EdgeView _lastEdge = null;

	public EdgeDrawer(EdgeView.Factory edgeFactory)
	{
		_edgeFactory = edgeFactory;
	}

	public void DrawEdge(ScarabNode start, ScarabNode end)
	{
		EdgeView edge = _edgeFactory.Create(start.transform.position, end.transform.position, end.View);
		_edges.Add(edge);
		_lastEdge = edge;
	}

	public void ClearAll()
	{
		foreach (EdgeView edge in _edges)
		{
			edge.Dispose();
		}

		_lastEdge = null;
	}

	public void CancelLast()
	{
		_edges.RemoveAt(_edges.Count - 1);
		_lastEdge.Dispose();
		
		if (_edges.Count > 0)
		{
			_lastEdge = _edges[_edges.Count - 1];
		}
	}
}
