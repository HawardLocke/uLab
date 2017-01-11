
using Lite.Goap;

namespace Lite.Strategy
{

	public class ChopFirewood : GoapAction
	{
		public ChopFirewood(Agent agent) :
			base(agent, GoapDefines.STATE_COUNT)
		{
			actionType = (uint)ActionType.ChopFirewood;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)StateType.HasTool, true);
			preconditons.Set((int)StateType.HasFirewood, false);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)StateType.HasFirewood, true);
		}

	}

}