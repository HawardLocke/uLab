
using System.Collections.Generic;


namespace Lite
{
	public abstract class LayeredFacade<T> where T : Agent
	{
		private Dictionary<long, T> m_agentMap;

		public virtual void Init() 
		{
			m_agentMap = new Dictionary<long, T>();
		}

		public virtual void Update() { }

		public virtual void Destroy() { }

		public virtual void AddAgent(T agent)
		{
			if (!m_agentMap.ContainsKey(agent.GUID))
			{
				m_agentMap.Add(agent.GUID, agent);
			}
		}

		public virtual void DeleteAgent(T agent)
		{
			m_agentMap.Remove(agent.GUID);
		}


	}

}