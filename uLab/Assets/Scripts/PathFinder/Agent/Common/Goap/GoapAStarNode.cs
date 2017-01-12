

namespace Lite.Goap
{
	public class GoapAStarNode : AStar.AStarNode
	{
		public WorldState state;
		
		//public WorldState goalStatus;

		public GoapAction fromAction;

		public GoapAStarNode(int stateCount)
		{
			state = new WorldState(stateCount);
			fromAction = null;
		}

		public override void Reset()
		{
			base.Reset();
			state.Reset();
			fromAction = null;
		}

	}

}