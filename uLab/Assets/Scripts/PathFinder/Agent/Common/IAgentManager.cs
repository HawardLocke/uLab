
using System.Collections.Generic;


namespace Lite
{
	public abstract class IAgentManager<T> where T : IAgent
	{
		protected Dictionary<long, T> m_agentMap;

		public IAgentManager()
		{
			m_agentMap = new Dictionary<long, T>();
		}

		public virtual void Init() { }

		public virtual void Update() { }

		public virtual void Destroy() { }

		public virtual void AddAgent(T agent)
		{
			if (!m_agentMap.ContainsKey(agent.Guid))
			{
				m_agentMap.Add(agent.Guid, agent);
			}
		}

		public virtual void DeleteAgent(long guid)
		{
			m_agentMap.Remove(guid);
		}

		public T FindAgent(long guid)
		{
			T agent;
			m_agentMap.TryGetValue(guid, out agent);
			return agent;
		}


	}

}