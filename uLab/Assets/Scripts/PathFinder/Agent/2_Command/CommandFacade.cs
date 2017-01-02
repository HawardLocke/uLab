
using System.Collections.Generic;

using Lite.Strategy;


namespace Lite
{

	public class CommandFacade : BaseFacade<CommandAgent>
	{
		public void Process()
		{
			foreach (CommandAgent agent in this.m_agentMap.Values)
			{
				agent.Process();
			}
		}

		public void OnReceiveCommand(Command Command)
		{
			CommandAgent agent = FindAgent(Command.ownerGuid);
			if (agent == null)
			{
				agent = new CommandAgent(Command.ownerGuid);
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