
using ProtoBuf;
using Lite.Goap;

namespace Lite.Strategy
{
	[ProtoContract]
	public class DropOffLogs : AgentAction
	{
		public DropOffLogs(Agent agent) :
			base(agent)
		{
			actionType = (int)ActionType.DropOffLogs;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)WorldStateType.HasLogs, true);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasLogs, false);
			effects.Set((int)WorldStateType.CollectLogs, true);
		}

		protected override byte[] ToBytes()
		{
			return ProtobufUtil.Serialize<DropOffLogs>(this);
		}

	}

}