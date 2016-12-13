
using System;
using System.Collections;
using System.Collections.Generic;


namespace Lite.AStar
{
	public class GraphAStarMap : AStarMap
	{
		private const int NEIGHBOUR_COUNT = 8;


		public void InitMap()
		{


		}

		public override int GetNeighbourNodeCount(AStarNode node)
		{
			return NEIGHBOUR_COUNT;
		}

		public override AStarNode GetNeighbourNode(AStarNode node, int index)
		{
			return null;
		}

		public GraphAStarNode GetNodeAt(int posx, int posy)
		{
			foreach (var nd in nodeTable.Values)
			{
				GraphAStarNode node = nd as GraphAStarNode;
				if (node.x == posx && node.y == posy)
					return node;
			}
			return null;
		}

	}
}

