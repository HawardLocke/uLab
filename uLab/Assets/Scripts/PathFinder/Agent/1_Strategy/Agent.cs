

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
		public Career career { private set; get; }

		public string name;

		private GoapManager goapManager;

		public WorldState worldState { private set; get; }

		public Agent(long guid, Career career) :
			base(guid)
		{
			this.career = career;
			goapManager = new GoapManager(this);
			worldState = new WorldState(GoapDefines.STATE_COUNT);
		}

		public void Init()
		{
			worldState.Set((int)WorldStateType.CollectFirewood, false);
		}

		public void Update()
		{
			goapManager.Update();
		}

		public void AddGoal(GoapGoal goal)
		{
			goapManager.AddGoal(goal);
		}

	}

}