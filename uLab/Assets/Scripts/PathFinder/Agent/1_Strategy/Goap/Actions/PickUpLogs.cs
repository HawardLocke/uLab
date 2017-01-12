
using Lite.Goap;

namespace Lite.Strategy
{

	public class PickUpLogs : GoapAction
	{
		public PickUpLogs(Agent agent) :
			base(agent, GoapDefines.STATE_COUNT)
		{
			actionType = (uint)ActionType.PickupLogs;
			cost = 2;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)WorldStateType.HasLogs, false);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasLogs, true);
		}

	}

}