

namespace AStar
{

	public abstract class Node
	{
		public int id;
		public float g;
		public float h;
		public float f;
		public Node prev;
		public Node next;	// for linked list
		public int blockValue;

		public Node(int id)
		{
			this.id = id;
			g = h = f = 0;
			prev = null;
			next = null;
			blockValue = 0;
		}
	}

}