
using ProtoBuf;
using Lite.Goap;

namespace Lite.Strategy
{
	[ProtoContract]
	public class MineOre : AgentAction
	{
		public MineOre(Agent agent) :
			base(agent)
		{
			actionType = (int)ActionType.MineOre;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)WorldStateType.HasTool, true);
			preconditons.Set((int)WorldStateType.HasOre, false);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasOre, true);
		}

		public override byte[] ToBytes()
		{
			return ProtobufUtil.Serialize<MineOre>(this);
		}

	}

}