
using System;
using System.Collections.Generic;

namespace AStar
{
	public class GraphNode : Node
	{
		public List<GraphNode> neighbours;

		public GraphNode(int id) :
			base(id)
		{
			neighbours = new List<GraphNode>();
		}
	}

}