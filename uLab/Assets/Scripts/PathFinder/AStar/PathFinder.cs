using System;
using System.Collections;
using System.Collections.Generic;


namespace AStar
{

	public abstract class PathFinder
	{
		private Node openList;

		private Node closedList;

		protected Map map;


		public void Setup(Map map)
		{
			this.map = map;
		}

		protected Node DoAStar(Node startNode)
		{
			openList = null;
			closedList = null;

			startNode.g = 0;
			startNode.h = CalCostH(startNode);
			startNode.f = startNode.g + startNode.h;
			startNode.prev = null;
			startNode.next = null;

			AddToOpenList(startNode);

			Node arriveNode = null;

			while (openList != null)
			{
				Node curNode = openList;
				if (CheckArrived(curNode))
				{
					arriveNode = curNode;
					break;
				}
				EvaluateAllNeighbours(curNode);
				RemoveFromOpenList(curNode);
				AddToClosedList(curNode);
			}

			return arriveNode;
		}

		protected abstract bool CheckArrived(Node node);

		protected abstract float CalCostG(Node prevNode, Node currentNode);

		protected abstract float CalCostH(Node node);

		private void EvaluateAllNeighbours(Node node)
		{
			int neighbourCount = map.GetNeighbourNodeCount(node);
			for (int i = 0; i < neighbourCount; ++i)
			{
				Node neighbour = map.GetNeighbourNode(node, i);
				if (neighbour != null)
					EvaluateNeighbour(node, neighbour);
			}
		}

		private void EvaluateNeighbour(Node currentNode, Node neighbourNode)
		{
			float blockValue = neighbourNode.blockValue;
			if (blockValue > 0.9)
				return;

			float g = CalCostG(currentNode, neighbourNode);
			float h = CalCostH(neighbourNode);
			float f = g + h;

			Node findNode = FindInOpenList(neighbourNode);
			if (findNode != null)
			{
				if (f < findNode.f)
				{
					findNode.g = g;
					findNode.h = h;
					findNode.f = f;
					findNode.prev = currentNode;
				}
			}
			else
			{
				findNode = FindInClosedList(neighbourNode);
				if (findNode != null)
				{
					if (f < findNode.f)
					{
						RemoveFromClosedList(findNode);
						findNode.g = g;
						findNode.h = h;
						findNode.f = f;
						findNode.prev = currentNode;
						AddToOpenList(findNode);
					}
				}
				else
				{
					Node newNode = neighbourNode;
					newNode.g = g;
					newNode.h = h;
					newNode.f = f;
					newNode.prev = currentNode;
					AddToOpenList(newNode);
				}
			}

		}

		private void AddToOpenList(Node node)
		{
			if (openList == null)
			{
				openList = node;
				node.next = null;
			}
			else
			{
				Node prevNode = null;
				Node curNode = openList;
				while (curNode != null)
				{
					if (node.f < curNode.f)
					{
						node.next = curNode;
						if (prevNode != null)
							prevNode.next = node;
						else
							openList = node;
						break;
					}
					else if (curNode.next == null)
					{
						curNode.next = node;
						node.next = null;
						break;
					}
					prevNode = curNode;
					curNode = curNode.next;
				}
			}
		}

		private void AddToClosedList(Node node)
		{
			if (closedList == null)
			{
				closedList = node;
				node.next = null;
			}
			else
			{
				Node prevNode = null;
				Node curNode = closedList;
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
					else if (curNode.next == null)
					{
						curNode.next = node;
						node.next = null;
						break;
					}
					prevNode = curNode;
					curNode = curNode.next;
				}
			}
		}

		private void RemoveFromOpenList(Node node)
		{
			if (openList != null)
			{
				Node prevNode = null;
				Node curNode = openList;
				while (curNode != null)
				{
					if (node.id == curNode.id)
					{
						if (prevNode != null)
							prevNode.next = curNode.next;
						else
							openList = curNode.next;
						curNode.next = null;
						break;
					}
					prevNode = curNode;
					curNode = curNode.next;
				}
			}
		}

		private void RemoveFromClosedList(Node node)
		{
			if (closedList != null)
			{
				Node prevNode = null;
				Node curNode = closedList;
				while (curNode != null)
				{
					if (node.id == curNode.id)
					{
						if (prevNode != null)
							prevNode.next = curNode.next;
						else
							closedList = curNode.next;
						curNode.next = null;
						break;
					}
					prevNode = curNode;
					curNode = curNode.next;
				}
			}
		}

		private Node FindInOpenList(Node node)
		{
			if (openList == null)
			{
				return null;
			}
			else
			{
				Node curNode = openList;
				while (curNode != null)
				{
					if (curNode.id == node.id)
						return curNode;
					
					curNode = curNode.next;
				}

				return null;
			}
		}

		private Node FindInClosedList(Node node)
		{
			if (closedList == null)
			{
				return null;
			}
			else
			{
				Node curNode = closedList; 
				while (curNode != null)
				{
					if (curNode.id == node.id)
						return curNode;
					
					curNode = curNode.next;
				}

				return null;
			}
		}


	}


}