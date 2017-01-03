

namespace Lite.Goap
{
	// worldstate is node, action is edge
	public class GoapAStarNode : AStar.AStarNode
	{
		public ContextStatus currentStatus;
		
		public ContextStatus goalStatus;

	}

}