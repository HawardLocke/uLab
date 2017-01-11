
using Lite.Goap;

namespace Lite.Strategy
{

	public class ChopTree : GoapAction
	{
		public ChopTree(Agent agent) : 
			base(agent, GoapDefines.STATE_COUNT)
		{
			actionType = (uint)ActionType.ChopLog;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)StateType.HasTool, true);
			preconditons.Set((int)StateType.HasLogs, false);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)StateType.HasLogs, true);
		}
		
	}

}