
using Lite.Goap;

namespace Lite.Strategy
{

	public class PickUpTool : GoapAction
	{
		public PickUpTool(Agent agent) :
			base(agent, GoapDefines.STATE_COUNT)
		{
			actionType = (uint)ActionType.PickupTools;
			cost = 2;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)StateType.HasTool, false);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)StateType.HasTool, true);
		}

	}

}