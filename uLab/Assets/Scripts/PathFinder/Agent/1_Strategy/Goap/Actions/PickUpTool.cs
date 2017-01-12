
using Lite.Goap;

namespace Lite.Strategy
{

	public class PickUpTool : GoapAction
	{
		public PickUpTool(Agent agent) :
			base(agent, GoapDefines.STATE_COUNT)
		{
			actionType = (uint)ActionType.PickupTools;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)WorldStateType.HasTool, false);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasTool, true);
		}

	}

}