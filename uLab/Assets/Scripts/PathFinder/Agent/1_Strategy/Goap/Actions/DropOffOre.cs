
using Lite.Goap;

namespace Lite.Strategy
{

	public class DropOffOre : GoapAction
	{
		public DropOffOre(Agent agent) :
			base(agent, GoapDefines.STATE_COUNT)
		{
			actionType = (uint)ActionType.DropOffOre;
			cost = 2;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)StateType.HasOre, true);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)StateType.HasOre, false);
			effects.Set((int)StateType.CollectOre, true);
		}

	}

}