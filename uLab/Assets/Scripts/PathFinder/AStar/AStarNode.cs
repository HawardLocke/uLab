

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
			Reset();
			this.id = id;
		}

		public AStarNode()
		{
			Reset();
			this.id = -1;
		}

		public virtual void Reset()
		{
			id = -1;
			g = h = f = 0;
			prev = null;
			next = null;
			blockValue = 0;
		}

	}

}