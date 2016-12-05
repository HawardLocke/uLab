using System;
using System.Collections;
using System.Collections.Generic;


namespace PathFinder
{
	public class AStarEngine
	{
		private AStarNode openList;
		private AStarNode closedList;
		private int startX;
		private int startY;
		private int endX;
		private int endY;

		private AStarMap mapData;


		public void Search(int startX, int startY, int endX, int endY)
		{
			openList = null;
			closedList = null;

			this.startX = startX;
			this.startY = startY;
			this.endX = endX;
			this.endY = endY;

			AStarNode startNode = new AStarNode(startX, startY);
			startNode.g = CalculateCostFromStart(0, startX, startY);
			startNode.h = CalculateCostToEnd(startX, startY);
			startNode.f = startNode.g + startNode.h;
			startNode.parent = null;
			startNode.next = null;

			AddToOpenList(startNode);

			AStarNode curNode = openList;
			while (curNode != null)
			{
				EvaluateAroundNode(curNode, curNode.x - 1, curNode.y - 1);
				EvaluateAroundNode(curNode, curNode.x - 1, curNode.y - 0);
				EvaluateAroundNode(curNode, curNode.x - 1, curNode.y + 1);
				EvaluateAroundNode(curNode, curNode.x + 0, curNode.y + 1);
				EvaluateAroundNode(curNode, curNode.x + 1, curNode.y + 1);
				EvaluateAroundNode(curNode, curNode.x + 1, curNode.y - 0);
				EvaluateAroundNode(curNode, curNode.x + 1, curNode.y - 1);
				EvaluateAroundNode(curNode, curNode.x - 0, curNode.y - 1);

				AddToClosedList(curNode);

				curNode = curNode.next;
			}

			// find none

			// reverse to get path.
		}

		private void EvaluateAroundNode(AStarNode prevNode, int x, int y)
		{
			float g = CalculateCostFromStart(prevNode.f, x, y);
			float h = CalculateCostToEnd(x, y);
			float f = g + h;

			AStarNode findNode = FindInOpenList(x, y);
			if (findNode != null)
			{
				if (f < findNode.f)
				{
					findNode.g = g;
					findNode.h = h;
					findNode.f = f;
					findNode.parent = prevNode;
				}
			}
			else
			{
				findNode = FindInClosedList(x, y);
				if (findNode != null)
				{

				}
				else
				{
					AStarNode newNode = new AStarNode(x, y);
					newNode.g = g;
					newNode.h = h;
					newNode.f = f;
					newNode.parent = prevNode;
					AddToOpenList(newNode);
				}
			}
			
		}

		private void AddToOpenList(AStarNode node)
		{
			if (openList == null)
			{
				openList = node;
			}
			else
			{
				AStarNode prevNode = null;
				AStarNode curNode = openList;
				while (curNode != null)
				{
					if (node.f < curNode.f)
					{
						node.next = curNode;
						if (prevNode != null)
							prevNode.next = node;
						break;
					}
					prevNode = curNode;
					curNode = curNode.next;
				}
			}
		}

		private void AddToClosedList(AStarNode node)
		{
			if (closedList == null)
			{
				closedList = node;
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
						break;
					}
					prevNode = curNode;
					curNode = curNode.next;
				}
			}
		}

		public AStarNode FindInOpenList(int x, int y)
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
					{
						return curNode;
					}
					curNode = curNode.next;
				}

				return null;
			}
		}

		public AStarNode FindInClosedList(int x, int y)
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
					{
						return curNode;
					}
					curNode = curNode.next;
				}

				return null;
			}
		}


		private float CalculateCostFromStart(float prevF, int x, int y)
		{
			return prevF + Math.Abs(startX - x) + Math.Abs(startY - y);
		}

		private float CalculateCostToEnd(int x, int y)
		{
			return Math.Abs(endX - x) + Math.Abs(endY - y);
		}

	}


}