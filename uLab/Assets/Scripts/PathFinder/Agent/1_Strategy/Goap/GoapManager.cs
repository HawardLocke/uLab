
using System.Collections;
using System.Collections.Generic;
using Lite.Goap;


namespace Lite.Strategy
{

	public class GoapManager
	{
		private Agent owner;
		private GoapMap map;
		private GoapAStarPlanner planner;
		private List<GoapGoal> goalList;
		private GoapGoal currentGoal;

		public GoapManager(Agent agent)
		{
			owner = agent;
			map = new GoapMap();
			map.BuildActionTable(agent);
			planner = new GoapAStarPlanner();
			planner.Setup(map);
			goalList = new List<GoapGoal>();
		}

		public void Update()
		{
			if (currentGoal != null)
			{
				currentGoal.Update();
			}
			else if (goalList.Count > 0)
			{
				currentGoal = goalList[0];
				GoapPlan plan = BuildPlan(currentGoal);
				currentGoal.Active(plan);
			}
		}

		public void AddGoal(GoapGoal goal)
		{

		}

		public bool ContainsGoal(GoapGoal goal)
		{
			for (int i = 0; i < goalList.Count; ++i)
			{
				/*if ()
				{
				
				}*/
			}
			return false;
		}

		public GoapPlan BuildPlan(GoapGoal goal)
		{
			GoapPlan plan = null;
			planner.Plan(owner.worldState, goal.goalState);
			return plan;
		}

	}


}