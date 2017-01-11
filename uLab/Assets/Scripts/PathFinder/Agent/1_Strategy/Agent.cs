

using Lite.Goap;


namespace Lite.Strategy
{
	public enum Career
	{
		Miner,
		Logger,
		WoodCutter,
		Blacksmith
	}

	public class Agent : IAgent
	{

		public Career career
		{
			private set;
			get;
		}

		GoapMap graph;
		GoapAction[] actionList;
		GoapAStarPlanner goapPlanner;


		public Agent(long guid, Career career) :
			base(guid)
		{
			this.career = career;

			graph = new GoapMap();
			graph.BuildActionTable(this);
			goapPlanner = new GoapAStarPlanner();
			goapPlanner.Setup(graph);
		}

		public void Update(long ms)
		{
			if (actionList == null)
			{
				GoapGoal goal = MakeGoal();
				actionList = goapPlanner.Plan(goal);
			}
			else
			{

			}
		}

		private GoapGoal MakeGoal()
		{
			return null;
		}

	}

}