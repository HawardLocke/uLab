
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
			preconditons.Set((int)WorldStateType.HasTool, true);
			preconditons.Set((int)WorldStateType.HasOre, false);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasOre, true);
		}

	}

}