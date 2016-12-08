using System;
using System.Collections;
using System.Collections.Generic;


namespace AStar
{
	public struct Point2D
	{
		public float x;
		public float y;
		public Point2D(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
	}

	public class GridPathFinder : PathFinder
	{
		private int startX;
		private int startY;
		private int endX;
		private int endY;
		private GridNode startNode;
		private GridNode targetNode;

		public Point2D[] FindPath(int startX, int startY, int endX, int endY)
		{
			this.startX = endX;
			this.startY = endY;
			this.endX = startX;
			this.endY = startY;

			GridMap gridMap = (GridMap)map;
			startNode = gridMap.GetNode(this.startX, this.startY);
			targetNode = gridMap.GetNode(this.endX, this.endY);

			GridNode endNode = DoAStar(startNode) as GridNode;

			// build path points.
			int pointCount = 0;
			GridNode pathNode = endNode;
			while (pathNode != null)
			{
				pointCount++;
				pathNode = pathNode.prev as GridNode;
			}
			Point2D[] pointArray = new Point2D[pointCount];
			pathNode = endNode;
			int index = 0;
			while (pathNode != null)
			{
				pointArray[index++] = new Point2D(pathNode.x, pathNode.y);
				pathNode = pathNode.prev as GridNode;
			}
			return pointArray;
		}

		protected override bool CheckArrived(Node node)
		{
			return node.id == targetNode.id;
		}

		protected override float CalCostG(Node prevNode, Node currentNode)
		{
			int dx = Math.Abs(((GridNode)prevNode).x - ((GridNode)currentNode).x);
			int dy = Math.Abs(((GridNode)prevNode).y - ((GridNode)currentNode).y);
			float dist = dx > dy ? 1.4f * dy + 1.0f * (dx - dy) : 1.4f * dx + 1.0f * (dy - dx);
			return prevNode.g + dist;
		}

		protected override float CalCostH(Node node)
		{
			int dx = Math.Abs(endX - ((GridNode)node).x);
			int dy = Math.Abs(endY - ((GridNode)node).y);
			float dist = dx > dy ? 1.4f * dy + 1.0f * (dx - dy) : 1.4f * dx + 1.0f * (dy - dx);
			return dist;
		}

	}


}