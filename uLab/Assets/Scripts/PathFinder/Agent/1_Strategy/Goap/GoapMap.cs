

using Lite.Goap;


namespace Lite.Strategy
{
	public class GoapMap : GoapAStarMap
	{
		public override void BuildActionTable(IAgent agnt)
		{
			Agent agent = agnt as Agent;
			if (agent.career == Strategy.Agent.Career.Miner)
			{
				AddAction(null);
			}
			else if (agent.career == Strategy.Agent.Career.Logger)
			{

			}
			else if (agent.career == Strategy.Agent.Career.WoodCutter)
			{

			}
			else if (agent.career == Strategy.Agent.Career.Blacksmith)
			{

			}
		}

	}
}

