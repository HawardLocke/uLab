
using ProtoBuf;
using Lite.Goap;

namespace Lite.Strategy
{
	[ProtoContract]
	public class DropOffTools : AgentAction
	{
		public DropOffTools(Agent agent) :
			base(agent)
		{
			actionType = (uint)ActionType.DropOffTools;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)WorldStateType.HasTool, true);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasTool, false);
			effects.Set((int)WorldStateType.CollectTools, true);
		}

		public override byte[] ToBytes()
		{
			return ProtobufUtil.Serialize<DropOffTools>(this);
		}

	}

}