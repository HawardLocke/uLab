
using Lite.Goap;

namespace Lite.Strategy
{

	public class PickUpOre : GoapAction
	{
		public UnityEngine.Vector3 targetPosition;

		public PickUpOre(Agent agent) :
			base(agent, GoapDefines.STATE_COUNT)
		{
			actionType = (uint)ActionType.PickupOre;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)WorldStateType.HasOre, false);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasOre, true);
		}

	}

}