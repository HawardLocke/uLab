

using Lite.Goap;
using Lite.Knowledge;
using Lite.Common;


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

		public ISensor sensor { private set; get; }

		public Blackboard<int> blackboard { private set; get; }

		private GoapManager goapManager;

		public WorldState worldState { private set; get; }

		public Agent(long guid, Career career) :
			base(guid)
		{
			this.career = career;
			blackboard = new Blackboard<int>();
			goapManager = new GoapManager(this);
			worldState = new WorldState(GoapDefines.STATE_COUNT);
		}

		public override void OnCreate()
		{
			AppFacade.Instance.sensorManager.AddSensor<SimpleAgentSensor>(this);

			worldState.Set((int)WorldStateType.CollectFirewood, false);
		}

		public override void OnDestroy()
		{
			
		}

		public override void OnUpdate()
		{
			goapManager.Update();
		}

		public void AddGoal(GoapGoal goal)
		{
			goapManager.AddGoal(goal);
		}

	}

}