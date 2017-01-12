
using Lite.Goap;
using Lite.Common;


namespace Lite.Strategy
{

	public class Goal_MakeFirewoods : GoapGoal
	{
		public Goal_MakeFirewoods()
		{
			goalType = GoalType.MakeFirewoods;
		}

		public override void Active(GoapPlan plan)
		{
			base.Active(plan);
		}

		public override void Deactive()
		{
			base.Deactive();
		}

		public override bool IsSatisfied(WorldState state)
		{
			BoolValue value = state.Get((int)WorldStateType.CollectFirewood) as BoolValue;
			return value != null && value.ToBool() == true;
		}

	}


}