
using System;
using System.Collections;
using System.Collections.Generic;


namespace AStar
{
	public class GridMap : Map
	{
		private const int NEIGHBOUR_COUNT = 8;
		private readonly int[] xOffset = { -1, -1, -1, 0, 1, 1, 1, 0 };
		private readonly int[] yOffset = { -1, 0, 1, 1, 1, 0, -1, -1 };

		private GridNode[,] nodes;
		private int width;
		private int height;

		private int nodeIdCounter;

		public void InitMap(int width, int height)
		{
			nodeIdCounter = 0;
			this.width = width;
			this.height = height;

			if (nodes != null)
				nodes = null;

			nodes = new GridNode[width, height];

			for (int x = 0; x < width; ++x)
			{
				for (int y = 0; y < height; ++y)
				{
					GridNode node = new GridNode(nodeIdCounter++);
					nodes[x, y] = node;
					node.x = x;
					node.y = y;
				}
			}

		}

		public void SetNodePassable(int x, int y, bool passable)
		{
			if (x >= 0 && x < width && y >= 0 && y <= height)
			{
				GridNode node = nodes[x, y];
				node.blockValue = passable ? 0 : 1;
			}
		}

		public bool IsNodePassable(int x, int y)
		{
			if (x >= 0 && x < width && y >= 0 && y <= height)
			{
				GridNode node = nodes[x, y];
				return node.blockValue == 0;
			}
			return false;
		}

		public override int GetNeighbourNodeCount(Node node)
		{
			return NEIGHBOUR_COUNT;
		}

		public override Node GetNeighbourNode(Node node, int index)
		{
			if (index >= 0 && index < NEIGHBOUR_COUNT && node != null)
			{
				GridNode gridNode = node as GridNode;
				int x = gridNode.x + xOffset[index];
				int y = gridNode.y + yOffset[index];
				if (x >= 0 && x < width && y >= 0 && y < height)
				{
					GridNode toNode = nodes[x, y] as GridNode;
					if (IsNeighbourPassable(gridNode, toNode))
						return toNode;
				}
			}
			return null;
		}

		public GridNode GetNode(int x, int y)
		{
			if (x >=0 && x < width && y >= 0 && y < height)
			{
				return nodes[x,y] as GridNode;
			}
			return null;
		}

		public int GetWidth()
		{
			return width;
		}

		public int GetHeight()
		{
			return height;
		}

		private bool IsNeighbourPassable(GridNode from, GridNode to)
		{
			return (from.x == to.x || from.y == to.y) || (nodes[from.x, to.y].blockValue < 1 && nodes[to.x, from.y].blockValue < 1);
		}

	}
}

