
using Lite.Goap;

namespace Lite.Strategy
{

	public class DropOffTools : GoapAction
	{
		public DropOffTools(Agent agent) :
			base(agent, GoapDefines.STATE_COUNT)
		{
			actionType = (uint)ActionType.DropOffTools;
			cost = 2;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)StateType.HasTool, true);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)StateType.HasTool, false);
			effects.Set((int)StateType.CollectTools, true);
		}

	}

}