
using System;
using System.Collections;
using System.Collections.Generic;


namespace AStar
{
	public class GraphMap : Map
	{
		public override int GetNeighbourNodeCount(Node node)
		{
			return 0;
		}

		public override Node GetNeighbourNode(Node node, int index)
		{
			return null;
		}
	}
}

