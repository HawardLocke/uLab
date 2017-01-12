
using Lite.Goap;


namespace Lite.Strategy
{
	public abstract class GoapGoal : GoapAStarGoal
	{
		public GoalType goalType;

		private GoapPlan plan;

		public GoapGoal()
		{
			goalType = GoalType.Default;
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

	}

}
