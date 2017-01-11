
using Lite.Goap;

namespace Lite.Strategy
{

	public class MineOre : GoapAction
	{
		public MineOre(Agent agent) :
			base(agent, GoapDefines.STATE_COUNT)
		{
			actionType = (uint)ActionType.MineOre;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)StateType.HasTool, true);
			preconditons.Set((int)StateType.HasOre, false);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)StateType.HasOre, true);
		}

	}

}