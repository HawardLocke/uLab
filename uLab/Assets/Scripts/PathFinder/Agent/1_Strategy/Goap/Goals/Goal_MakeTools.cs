
using Lite.Goap;
using Lite.Common;


namespace Lite.Strategy
{

	public class Goal_MakeTools : GoapGoal
	{
		public Goal_MakeTools() :
			base(GoalType.MakeTools) 
		{
		}

		protected override void OnInit()
		{
			// set goal state.
			goalState.Set((int)WorldStateType.CollectTools, true);
		}

	}


}