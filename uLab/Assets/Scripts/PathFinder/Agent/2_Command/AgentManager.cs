
using System.Collections.Generic;

using Lite.Strategy;


namespace Lite.Cmd
{

	public class AgentManager : IAgentManager<Agent>
	{
		public void Process()
		{
			foreach (Agent agent in this.m_agentMap.Values)
			{
				agent.Process();
			}
		}

		public void OnReceiveCommand(Command Command)
		{
			Agent agent = FindAgent(Command.ownerGuid);
			if (agent == null)
			{
				agent = new Agent(Command.ownerGuid);
				AddAgent(agent);
			}
			agent.PushCommand(Command);
		}

		public void OnAgentQuit(long agentGuid)
		{
			DeleteAgent(agentGuid);
		}


	}

}