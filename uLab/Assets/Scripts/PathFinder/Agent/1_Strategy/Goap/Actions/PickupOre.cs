
using ProtoBuf;
using Lite.Goap;

namespace Lite.Strategy
{
	[ProtoContract]
	public class PickUpOre : AgentAction
	{
		public UnityEngine.Vector3 targetPosition;

		public PickUpOre(Agent agent) :
			base(agent)
		{
			actionType = (int)ActionType.PickupOre;
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

		protected override byte[] ToBytes()
		{
			return ProtobufUtil.Serialize<PickUpOre>(this);
		}

	}

}