
using System.Collections.Generic;


namespace Lite
{
	public abstract class BaseFacade<T> where T : Agent
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
			if (!m_agentMap.ContainsKey(agent.Guid))
			{
				m_agentMap.Add(agent.Guid, agent);
			}
		}

		public virtual void DeleteAgent(T agent)
		{
			m_agentMap.Remove(agent.Guid);
		}

		public T FindAgent(long guid)
		{
			T agent;
			m_agentMap.TryGetValue(guid, out agent);
			return agent;
		}


	}

}