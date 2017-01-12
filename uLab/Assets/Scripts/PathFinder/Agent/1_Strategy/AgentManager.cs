
using System;
using System.Collections;
using System.Collections.Generic;


namespace Lite.Strategy
{

	public class AgentManager : IAgentManager<Agent>
	{
		public override void Update()
		{
			IDictionaryEnumerator iter = m_agentMap.GetEnumerator();
			while (iter.MoveNext())
			{
				Agent agent = iter.Entry.Value as Agent;
				agent.Update();
			}
		}

	}


}