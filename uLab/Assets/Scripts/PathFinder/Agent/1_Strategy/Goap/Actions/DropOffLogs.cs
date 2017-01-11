
using Lite.Goap;

namespace Lite.Strategy
{

	public class DropOffLogs : GoapAction
	{
		public DropOffLogs(Agent agent) :
			base(agent, GoapDefines.STATE_COUNT)
		{
			actionType = (uint)ActionType.DropOffLogs;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)StateType.HasLogs, true);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)StateType.HasLogs, false);
			effects.Set((int)StateType.CollectLogs, true);
		}

	}

}