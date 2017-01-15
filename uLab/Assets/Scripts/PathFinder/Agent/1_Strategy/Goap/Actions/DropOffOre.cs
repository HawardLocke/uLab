
using ProtoBuf;
using Lite.Goap;

namespace Lite.Strategy
{
	[ProtoContract]
	public class DropOffOre : AgentAction
	{
		public DropOffOre(Agent agent) :
			base(agent)
		{
			actionType = (int)ActionType.DropOffOre;
			cost = 1;
		}

		protected override void OnSetupPreconditons()
		{
			preconditons.Set((int)WorldStateType.HasOre, true);
		}

		protected override void OnSetupEffects()
		{
			effects.Set((int)WorldStateType.HasOre, false);
			effects.Set((int)WorldStateType.CollectOre, true);
		}

		public override byte[] ToBytes()
		{
			return ProtobufUtil.Serialize<DropOffOre>(this);
		}

	}

}