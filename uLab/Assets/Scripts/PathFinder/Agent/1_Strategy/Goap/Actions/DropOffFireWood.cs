
using ProtoBuf;
using Lite.Goap;

namespace Lite.Strategy
{
	[ProtoContract]
	public class DropOffFirewood : AgentAction
	{
		public DropOffFirewood(Agent agent) :
			base(agent)
		{
			actionType = (int)ActionType.DropOffFirewood;
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

		public override byte[] ToBytes()
		{
			return ProtobufUtil.Serialize<DropOffFirewood>(this);
		}

	}

}