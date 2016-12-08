
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

		private List<List<GridNode>> nodes;
		private int width;
		private int height;

		private int nodeIdCounter;

		public void BuildMap()
		{
			nodeIdCounter = 0;
			width = 50;
			height = 30;

			if (nodes != null)
				nodes.Clear();

			nodes = new List<List<GridNode>>();

			Random ran=new Random(DateTime.Now.Millisecond);
			

			for (int x = 0; x < width; ++x)
			{
				List<GridNode> columnNodes = new List<GridNode>();
				nodes.Add(columnNodes);
				for (int y = 0; y < height; ++y)
				{
					GridNode node = new GridNode(nodeIdCounter++);
					columnNodes.Add(node);
					node.x = x;
					node.y = y;

					if (x % 2 == 0 && x > 0 && x < width - 1 && y > 0 && y < height - 1 && y != height / 3 && y != 2 * height / 3)
					{
						node.blockValue = ran.Next(1, 3) > 1 ? 1 : 0;
					}
					if (x > 0 && x < width && y > 0 && y < height && (y == 1 * height / 3 || y == 2 * height / 3))
					{
						node.blockValue = ran.Next(1,5) > 1 ? 1 : 0;
					}

				}
			}
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
					return nodes[x][y];
			}
			return null;
		}

		public GridNode GetNode(int x, int y)
		{
			if (x >=0 && x < width && y >= 0 && y < height)
			{
				return nodes[x][y] as GridNode;
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

	}
}

