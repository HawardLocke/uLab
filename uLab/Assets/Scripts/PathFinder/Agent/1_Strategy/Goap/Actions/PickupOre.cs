
using Lite.Goap;

namespace Lite.Strategy
{

	public class PickupOre : GoapAction
	{
		public UnityEngine.Vector3 targetPosition;

		public PickupOre(Agent agent) :
			base(agent, GoapDefines.STATE_COUNT)
		{
			actionType = (uint)ActionType.PickupOre;
			cost = 2;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)StateType.HasOre, false);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)StateType.HasOre, true);
		}

	}

}