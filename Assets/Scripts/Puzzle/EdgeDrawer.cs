using System.Collections.Generic;

public class EdgeDrawer
{
	private EdgeView.Factory _edgeFactory;
	private List<EdgeView> _edges = new List<EdgeView>();

	public EdgeDrawer(EdgeView.Factory edgeFactory)
	{
		_edgeFactory = edgeFactory;
	}

	public void DrawEdge(ScarabNode start, ScarabNode end)
	{
		_edges.Add(_edgeFactory.Create(start.transform.position, end.transform.position));
	}

	public void ClearAll()
	{
		foreach (EdgeView edge in _edges)
		{
			edge.Dispose();
		}
	}
}
