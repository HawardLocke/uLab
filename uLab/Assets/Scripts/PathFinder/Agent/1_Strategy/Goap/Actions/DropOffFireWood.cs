
using Lite.Goap;

namespace Lite.Strategy
{

	public class DropOffFirewood : GoapAction
	{
		public DropOffFirewood(Agent agent) :
			base(agent, GoapDefines.STATE_COUNT)
		{
			actionType = (uint)ActionType.DropOffFirewood;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)WorldStateType.HasFirewood, true);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasFirewood, false);
			effects.Set((int)WorldStateType.CollectFirewood, true);
		}

	}

}