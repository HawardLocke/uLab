
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
			preconditons.Set((int)StateType.HasFirewood, true);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)StateType.HasFirewood, false);
			effects.Set((int)StateType.CollectFirewood, true);
		}

	}

}