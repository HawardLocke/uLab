
using ProtoBuf;
using Lite.Goap;

namespace Lite.Strategy
{
	[ProtoContract]
	public class PickUpLogs : AgentAction
	{
		public PickUpLogs(Agent agent) :
			base(agent)
		{
			actionType = (int)ActionType.PickupLogs;
			cost = 2;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)WorldStateType.HasLogs, false);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasLogs, true);
		}

		public override byte[] ToBytes()
		{
			return ProtobufUtil.Serialize<PickUpLogs>(this);
		}

	}

}