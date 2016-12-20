
using UnityEngine;


namespace Lite
{

	public abstract class Agent
	{
		protected long m_guid;

		protected Blackborad m_blackboard;

		public Agent(long guid)
		{
			m_guid = guid;
			m_blackboard = new Blackborad(this);
		}

		public long GUID
		{
			get { return m_guid; }
		}

		public Blackborad Blackborad
		{
			get { return m_blackboard; }
		}

	}

}