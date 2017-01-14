
using ProtoBuf;
using Lite.Goap;

namespace Lite.Strategy
{
	[ProtoContract]
	public class ForgeTool : AgentAction
	{
		public ForgeTool(Agent agent) :
			base(agent)
		{
			actionType = (uint)ActionType.ForgeTool;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)WorldStateType.HasOre, true);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasNewTools, true);
		}

		public override byte[] ToBytes()
		{
			return ProtobufUtil.Serialize<ForgeTool>(this);
		}

	}

}