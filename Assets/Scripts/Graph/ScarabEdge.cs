public class ScarabEdge
{
	public ScarabNode First { get; }
	public ScarabNode Second { get; }
	public bool IsMarked { get; set; }

	public ScarabEdge(ScarabNode first, ScarabNode second)
	{
		First = first;
		Second = second;
	}

	public bool HasNodes(ScarabNode node1, ScarabNode node2)
	{
		return (First == node1 && Second == node2) || (First == node2 && Second == node1);
	}
}
