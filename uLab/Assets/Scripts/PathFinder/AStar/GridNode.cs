

namespace AStar
{
	public class GridNode : Node
	{
		public int x;
		public int y;

		public GridNode(int id) :
			base(id)
		{
			x = 0;
			y = 0;
		}
	}

}