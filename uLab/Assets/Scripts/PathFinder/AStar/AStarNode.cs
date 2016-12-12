

namespace Lite.AStar
{

	public abstract class AStarNode : Lite.Graph.GraphNode
	{
		public int g;
		public int h;
		public int f;
		public AStarNode prev;
		public AStarNode next;	// for linked list
		public int blockValue;

		public AStarNode(int id)
		{
			this.index = id;
			g = h = f = 0;
			prev = null;
			next = null;
			blockValue = 0;
		}

		public AStarNode()
		{
			this.index = -1;
			g = h = f = 0;
			prev = null;
			next = null;
			blockValue = 0;
		}

	}

}