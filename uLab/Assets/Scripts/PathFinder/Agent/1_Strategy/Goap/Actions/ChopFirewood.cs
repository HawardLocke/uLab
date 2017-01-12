
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
			preconditons.Set((int)WorldStateType.HasTool, true);
			preconditons.Set((int)WorldStateType.HasFirewood, false);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasFirewood, true);
		}

	}

}