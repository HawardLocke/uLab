using System;
using System.Collections;
using System.Collections.Generic;


namespace PathFinder
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

	public class AStarEngine
	{
		private AStarNode openList;
		private AStarNode closedList;
		private int _startX;
		private int _startY;
		private int _endX;
		private int _endY;

		public AStarMap mapData;


		public Point2D[] Search(int startX, int startY, int endX, int endY)
		{
			openList = null;
			closedList = null;

			this._startX = endX;
			this._startY = endY;
			this._endX = startX;
			this._endY = startY;

			AStarNode startNode = new AStarNode(this._startX, this._startY);
			startNode.g = 0;
			startNode.h = CalCostH(this._startX, this._startY);
			startNode.f = startNode.g + startNode.h;
			startNode.prev = null;
			startNode.next = null;

			AddToOpenList(startNode);

			AStarNode targetNode = null;
			
			while (openList != null)
			{
				AStarNode curNode = openList;
				if (curNode.x == this._endX && curNode.y == this._endY)
				{
					targetNode = curNode;
					break;
				}
				EvaluateAround(curNode);
				RemoveFromOpenList(curNode);
				AddToClosedList(curNode);
			}

			// build path points array.
			int pointCount = 0;
			AStarNode pathNode = targetNode;
			while (pathNode != null)
			{
				pointCount++;
				pathNode = pathNode.prev;
			}
			Point2D[] pointArray = new Point2D[pointCount];
			pathNode = targetNode;
			int index = 0;
			while (pathNode != null)
			{
				pointArray[index++] = new Point2D(pathNode.x, pathNode.y);
				pathNode = pathNode.prev;
			}
			return pointArray;
		}

		private void EvaluateAround(AStarNode node)
		{
			EvaluateAroundNode(node, - 1, - 1);
			EvaluateAroundNode(node, - 1, - 0);
			EvaluateAroundNode(node, - 1, + 1);
			EvaluateAroundNode(node, + 0, + 1);
			EvaluateAroundNode(node, + 1, + 1);
			EvaluateAroundNode(node, + 1, - 0);
			EvaluateAroundNode(node, + 1, - 1);
			EvaluateAroundNode(node, - 0, - 1);
		}

		private void EvaluateAroundNode(AStarNode prevNode, int dx, int dy)
		{
			int x = prevNode.x + dx;
			int y = prevNode.y + dy;

			float blockValue = mapData.GetBlockValue(x, y);
			if (blockValue > 0.9)
				return;

			float g = CalCostG(prevNode.g, dx, dy);
			float h = CalCostH(x, y);
			float f = g + h;

			AStarNode findNode = FindInOpenList(x, y);
			if (findNode != null)
			{
				if (f < findNode.f)
				{
					findNode.g = g;
					findNode.h = h;
					findNode.f = f;
					findNode.prev = prevNode;
				}
			}
			else
			{
				findNode = FindInClosedList(x, y);
				if (findNode != null)
				{
					if (f < findNode.f)
					{
						RemoveFromClosedList(findNode);
						findNode.g = g;
						findNode.h = h;
						findNode.f = f;
						findNode.prev = prevNode;
						AddToOpenList(findNode);
					}
				}
				else
				{
					AStarNode newNode = new AStarNode(x, y);
					newNode.g = g;
					newNode.h = h;
					newNode.f = f;
					newNode.prev = prevNode;
					AddToOpenList(newNode);
				}
			}
			
		}

		private void AddToOpenList(AStarNode node)
		{
			if (openList == null)
			{
				openList = node;
				node.next = null;
			}
			else
			{
				bool insertDone = false;
				AStarNode prevNode = null;
				AStarNode curNode = openList;
				while (curNode != null)
				{
					if (node.f < curNode.f)
					{
						node.next = curNode;
						if (prevNode != null)
							prevNode.next = node;
						else
							openList = node;
						insertDone = true;
						break;
					}
					prevNode = curNode;
					curNode = curNode.next;
				}
				if (!insertDone)
				{
					prevNode.next = node;
				}
			}
		}

		private void AddToClosedList(AStarNode node)
		{
			if (closedList == null)
			{
				closedList = node;
				node.next = null;
			}
			else
			{
				AStarNode prevNode = null;
				AStarNode curNode = closedList;
				while (curNode != null)
				{
					if (node.f < curNode.f)
					{
						node.next = curNode;
						if (prevNode != null)
							prevNode.next = node;
						else
							closedList = node;
						break;
					}
					prevNode = curNode;
					curNode = curNode.next;
				}
			}
		}

		private void RemoveFromOpenList(AStarNode node)
		{
			if (openList != null)
			{
				AStarNode prevNode = null;
				AStarNode curNode = openList;
				while (curNode != null)
				{
					if (node.x == curNode.x && node.y == curNode.y)
					{
						if (prevNode != null)
							prevNode.next = curNode.next;
						else
							openList = curNode.next;
						break;
					}
					prevNode = curNode;
					curNode = curNode.next;
				}
			}
		}

		private void RemoveFromClosedList(AStarNode node)
		{
			if (closedList != null)
			{
				AStarNode prevNode = null;
				AStarNode curNode = closedList;
				while (curNode != null)
				{
					if (node.x == curNode.x && node.y == curNode.y)
					{
						if (prevNode != null)
							prevNode.next = curNode.next;
						else
							closedList = curNode.next;
						break;
					}
					prevNode = curNode;
					curNode = curNode.next;
				}
			}
		}

		private AStarNode FindInOpenList(int x, int y)
		{
			if (openList == null)
			{
				return null;
			}
			else
			{
				AStarNode curNode = openList;
				while (curNode != null)
				{
					if (curNode.x == x && curNode.y == y)
						return curNode;
					
					curNode = curNode.next;
				}

				return null;
			}
		}

		private AStarNode FindInClosedList(int x, int y)
		{
			if (closedList == null)
			{
				return null;
			}
			else
			{
				AStarNode curNode = closedList; 
				while (curNode != null)
				{
					if (curNode.x == x && curNode.y == y)
						return curNode;
					
					curNode = curNode.next;
				}

				return null;
			}
		}


		private float CalCostG(float prevCostG, int dx, int dy)
		{
			dx = Math.Abs(dx);
			dy = Math.Abs(dy);
			float dist = dx > dy ? 1.4f * dy + 1.0f * (dx - dy) : 1.4f * dx + 1.0f * (dy - dx);
			return prevCostG + dist;
		}

		private float CalCostH(int x, int y)
		{
			int dx = Math.Abs(_endX - x);
			int dy = Math.Abs(_endY - y);
			float dist = dx > dy ? 1.4f * dy + 1.0f * (dx - dy) : 1.4f * dx + 1.0f * (dy - dx);
			return dist;
		}

	}


}