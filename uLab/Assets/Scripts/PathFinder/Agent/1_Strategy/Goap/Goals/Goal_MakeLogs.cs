
using Lite.Goap;
using Lite.Common;


namespace Lite.Strategy
{

	public class Goal_MakeLogs : GoapGoal
	{
		public Goal_MakeLogs() :
			base(GoalType.MakeLogs) 
		{
		}

		protected override void OnInit()
		{
			// set goal state.
			goalState.Set((int)WorldStateType.CollectLogs, true);
		}

	}


}