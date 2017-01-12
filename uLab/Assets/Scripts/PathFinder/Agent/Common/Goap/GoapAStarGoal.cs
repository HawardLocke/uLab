

namespace Lite.Goap
{
	public abstract class GoapAStarGoal
	{
		public abstract bool IsSatisfied(WorldState state);

		public abstract void SetNodeGoalState(WorldState goalState);

	}

}
