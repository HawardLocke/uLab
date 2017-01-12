
using Lite.Goap;

namespace Lite.Strategy
{

	public class DropOffTools : GoapAction
	{
		public DropOffTools(Agent agent) :
			base(agent, GoapDefines.STATE_COUNT)
		{
			actionType = (uint)ActionType.DropOffTools;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)WorldStateType.HasTool, true);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasTool, false);
			effects.Set((int)WorldStateType.CollectTools, true);
		}

	}

}