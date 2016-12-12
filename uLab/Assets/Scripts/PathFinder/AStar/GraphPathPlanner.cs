using System;
using System.Collections;
using System.Collections.Generic;


namespace Lite.AStar
{
	

	public class GraphPathPlanner : PathPlanner
	{
		private int start;
		private int end;
		private GraphAStarNode startNode;
		private GraphAStarNode targetNode;

		public Point2D[] FindPath(int start, int end)
		{
			this.start = end;
			this.end = start;

			startNode = map.GetNode(this.start) as GraphAStarNode;
			targetNode = map.GetNode(this.end) as GraphAStarNode;

			GridAStarNode endNode = DoAStar(startNode) as GridAStarNode;

			// build path points.
			int pointCount = 0;
			GridAStarNode pathNode = endNode;
			while (pathNode != null)
			{
				pointCount++;
				pathNode = pathNode.prev as GridAStarNode;
			}
			Point2D[] pointArray = new Point2D[pointCount];
			pathNode = endNode;
			int index = 0;
			while (pathNode != null)
			{
				pointArray[index++] = new Point2D(pathNode.x, pathNode.y);
				pathNode = pathNode.prev as GridAStarNode;
			}
			return pointArray;
		}

		protected override bool CheckArrived(AStarNode node)
		{
			return node.index == targetNode.index;
		}

		protected override int CalCostG(AStarNode prevNode, AStarNode currentNode)
		{
			return prevNode.g + map.GetEdge(prevNode.index, currentNode.index).cost;
		}

		protected override int CalCostH(AStarNode node)
		{
			return 0;
		}

	}


}