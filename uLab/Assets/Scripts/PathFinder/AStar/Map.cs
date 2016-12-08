
using System;
using System.Collections;
using System.Collections.Generic;


namespace AStar
{

	public abstract class Map
	{
		public abstract int GetNeighbourNodeCount(Node node);

		public abstract Node GetNeighbourNode(Node node, int index);

	}

}