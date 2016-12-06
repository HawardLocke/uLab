
using System;
using System.Collections;
using System.Collections.Generic;


namespace PathFinder
{
	public class MapNodeData
	{
		public int x;
		public int y;
		public float block;

		public MapNodeData(int x, int y)
		{
			this.x = y;
			this.y = y;
			this.block = 0.0f;
		}
	}

	public class AStarMap
	{
		public int width;
		public int height;
		public MapNodeData[,] nodes;

		public void Create(int w, int h)
		{
			width = w;
			height = h;
			nodes = new MapNodeData[width, height];
			for (int i = 0; i < width; ++i)
			{
				for (int j = 0; j < height; ++j)
				{
					var node = new MapNodeData(i, j);
					nodes[i, j] = node;

					if (i % 2 == 0 && i > 0 && i < width - 1 && j > 0 && j < height - 1 && j != height/3 && j != 2*height/3)
					{
						node.block = 1;
					}
				}
			}
		}

		public float GetBlockValue(int x, int y)
		{
			if (x >= 0 && x < width && y >= 0 && y < height)
				return nodes[x, y].block;
			else
				return 1;
		}

	}

}