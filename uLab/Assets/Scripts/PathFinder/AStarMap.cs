
using System;
using System.Collections;
using System.Collections.Generic;


namespace PathFinder
{
	class MapNodeData
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

	class AStarMap
	{
		public int width;
		public int height;
		public MapNodeData[,] nodeArray;

		public void Create(int w, int h)
		{
			for (int i = 0; i < w; ++i)
			{
				for (int j = 0; j < h; ++j )
					nodeArray[i,j] = new MapNodeData(i,j);
			}
			
		}

	}

}