
using ProtoBuf;
using Lite.Goap;


namespace Lite.Strategy
{
	[ProtoContract]
	public class ChopFirewood : AgentAction
	{
		public ChopFirewood(Agent agent) :
			base(agent)
		{
			actionType = (int)ActionType.ChopFirewood;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)WorldStateType.HasTool, true);
			preconditons.Set((int)WorldStateType.HasFirewood, false);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasFirewood, true);
		}

		public override byte[] ToBytes()
		{
			return ProtobufUtil.Serialize<ChopFirewood>(this);
		}

	}

}