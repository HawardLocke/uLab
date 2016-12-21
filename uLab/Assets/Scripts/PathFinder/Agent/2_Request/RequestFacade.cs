
using System.Collections.Generic;


namespace Lite
{

	public class RequestFacade : BaseFacade<RequestAgent>
	{
		public void Process()
		{
			foreach (RequestAgent agent in this.m_agentMap.Values)
			{
				agent.Process();
			}
		}

		public void OnReceiveRequest(Request request)
		{
			RequestAgent agent = FindAgent(request.ownerGuid);
			if (agent == null)
			{
				agent = new RequestAgent(request.ownerGuid);
				AddAgent(agent);
			}
			agent.PushRequest(request);
		}

		public void OnAgentQuit(long agentGuid)
		{
			DeleteAgent(agentGuid);
		}


	}

}