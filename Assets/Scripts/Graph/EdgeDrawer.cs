public class EdgeDrawer
{
	private EdgeView.Factory _edgeFactory;

	public EdgeDrawer(EdgeView.Factory edgeFactory)
	{
		_edgeFactory = edgeFactory;
	}

	public void DrawEdge(ScarabNode start, ScarabNode end)
	{
		_edgeFactory.Create(start.transform.position, end.transform.position);
	}
}
