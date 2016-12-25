
using UnityEngine;


namespace Lite
{

	public abstract class Agent
	{
		protected long m_guid;

		public Agent(long guid)
		{
			m_guid = guid;
		}

		public long Guid
		{
			get { return m_guid; }
		}

	}

}