
using Lite.Goap;


namespace Lite.Strategy
{
	public abstract class GoapGoal
	{
		public GoalType goalType;

		public WorldState goalState;

		private GoapPlan plan;

		public GoapGoal()
		{
			goalType = GoalType.Default;
			goalState = new WorldState(GoapDefines.STATE_COUNT);
			plan = null;
		}

		public virtual void Active(GoapPlan plan)
		{
			this.plan = plan;
		}

		public virtual void Deactive()
		{

		}

		public void Update()
		{
			if (plan != null)
				plan.Excute();
		}

		public virtual bool IsSatisfied()
		{
			return false;
		}

	}

}
