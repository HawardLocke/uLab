
using ProtoBuf;
using Lite.Goap;


namespace Lite.Strategy
{
	// °ÑÔ­Ä¾log¿³³ÉÄ¾²ñfirewood
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
			preconditons.Set((int)WorldStateType.HasLogs, true);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasFirewood, true);
		}

		protected override byte[] ToBytes()
		{
			return ProtobufUtil.Serialize<ChopFirewood>(this);
		}

	}

}