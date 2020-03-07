using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScarabNode))]
public class ScarabNodeEditor : Editor
{
	private void OnSceneGUI()
	{
		Handles.color = Color.green;
		ScarabNode owner = target as ScarabNode;

		if (owner == null)
		{
			return;
		}

		DrawNeighbours(owner);
		DrawEdges(owner);
	}

	private void DrawNeighbours(ScarabNode owner)
	{
		List<ScarabNode> neighbours = owner.Neighbours;

		if (neighbours.Count == 0)
		{
			return;
		}

		if (Selection.activeObject != null && Selection.activeObject.name == target.name)
		{
			Handles.color = Color.red;
			Handles.CircleHandleCap(
					0, owner.transform.position, owner.transform.rotation, 0.2f, EventType.Repaint);
			Handles.color = Color.green;

			foreach (ScarabNode neighbour in neighbours)
			{
				if (neighbour == null)
				{
					continue;
				}

				Handles.CircleHandleCap(
					0, neighbour.transform.position, neighbour.transform.rotation, 0.2f, EventType.Repaint);
			}
		}
	}

	private void DrawEdges(ScarabNode owner)
	{
		List<ScarabEdge> edges = owner.Edges;

		if (edges.Count == 0)
		{
			return;
		}

		if (Selection.activeObject != null && Selection.activeObject.name == target.name)
		{
			foreach (ScarabEdge edge in edges)
			{
				Handles.DrawLine(edge.First.transform.position, edge.Second.transform.position);
			}
		}
	}
}
