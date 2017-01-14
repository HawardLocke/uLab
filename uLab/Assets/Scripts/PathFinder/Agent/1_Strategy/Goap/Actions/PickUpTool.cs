
using ProtoBuf;
using Lite.Goap;

namespace Lite.Strategy
{
	[ProtoContract]
	public class PickUpTool : AgentAction
	{
		public PickUpTool(Agent agent) :
			base(agent)
		{
			actionType = (uint)ActionType.PickupTools;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)WorldStateType.HasTool, false);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasTool, true);
		}

		public override byte[] ToBytes()
		{
			return ProtobufUtil.Serialize<PickUpTool>(this);
		}

	}

}