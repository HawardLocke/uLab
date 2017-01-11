
using Lite.Goap;

namespace Lite.Strategy
{

	public class ForgeTool : GoapAction
	{
		public ForgeTool(Agent agent) :
			base(agent, GoapDefines.STATE_COUNT)
		{
			actionType = (uint)ActionType.ForgeTool;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)StateType.HasOre, true);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)StateType.HasNewTools, true);
		}

	}

}