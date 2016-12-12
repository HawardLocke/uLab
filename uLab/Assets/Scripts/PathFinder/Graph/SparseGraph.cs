
using System;
using System.Collections;
using System.Collections.Generic;


namespace Lite.Graph
{
	using NodeList = List<GraphNode>;
	using EdgeList = List<GraphEdge>;

	public class SparseGraph
	{
		private NodeList nodeList;
		private List<EdgeList> edgeListList;

		private int nodeIndexCounter;

		public SparseGraph()
		{
			nodeList = new List<GraphNode>();
			edgeListList = new List<EdgeList>();
			nodeIndexCounter = 0;
		}

		public T AddNode<T>() where T : GraphNode, new()
		{
			T node = new T();
			node.index = nodeIndexCounter++;
			nodeList.Add(node);
			edgeListList.Add(new EdgeList());
			return node;
		}

		public void RemoveNode(int index)
		{
			GraphNode node = GetNode(index);
			if (node != null)
			{
				nodeList.RemoveAt(index);
				EdgeList edgeList = edgeListList[index];
				for (int i = 0; i < edgeList.Count; ++i)
				{
					GraphEdge edge = edgeList[i];
					RemoveEdge(edge.to, edge.from);
				}
				edgeList.Clear();
				edgeListList.RemoveAt(index);
			}
		}

		public GraphNode GetNode(int index)
		{
			if (IsIndexValid(index))
				return nodeList[index];
			return null;
		}

		public void AddEdge(int from, int to, int cost)
		{
			if (IsIndexValid(from) && IsIndexValid(to) && !IsEdgePresent(from, to))
			{
				GraphEdge edge = new GraphEdge(from, to, cost);
				edgeListList[from].Add(edge);
			}
		}

		public void RemoveEdge(int from, int to)
		{
			if (IsIndexValid(from) && IsIndexValid(to))
			{
				EdgeList edgeList = edgeListList[from];
				for (int i = 0; i < edgeList.Count; ++i)
				{
					if (edgeList[i].to == to)
					{
						edgeList.RemoveAt(i);
						break;
					}
				}
			}
		}

		public GraphEdge GetEdge(int from, int to)
		{
			if (IsIndexValid(from) && IsIndexValid(to))
			{
				EdgeList edgeList = edgeListList[from];
				for (int i = 0; i < edgeList.Count; ++i)
				{
					if (edgeList[i].to == to)
						return edgeList[i];
				}
			}
			return null;
		}

		public void SetEdgeCost(int from, int to, int cost)
		{
			GraphEdge edge = GetEdge(from, to);
			if (edge != null)
				edge.cost = cost;
		}

		public bool IsNodePresent(int index)
		{
			if (IsIndexValid(index))
			{
				return true;
			}
			return false;
		}

		public bool IsEdgePresent(int from, int to)
		{
			if (IsIndexValid(from) && IsIndexValid(to))
			{
				EdgeList edgeList = edgeListList[from];
				for (int i = 0; i < edgeList.Count; ++i)
				{
					if (edgeList[i].to == to)
						return true;
				}
			}
			return false;
		}

		private bool IsIndexValid(int index)
		{
			return index >= 0 && index < nodeList.Count;
		}

	}

}