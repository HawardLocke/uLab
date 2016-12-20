
using UnityEngine;

using System.Collections.Generic;


namespace Lite
{
	public class Blackborad
	{
		private Agent m_agent;

		private Dictionary<string, System.Object> dataDic;

		public Blackborad(Agent agent)
		{
			m_agent = agent;
			dataDic = new Dictionary<string, object>();
		}

		public T Get<T>(string key)
		{
			return (T)dataDic[key];
		}

		public void Set(string key, System.Object value)
		{
			if (dataDic.ContainsKey(key))
				dataDic[key] = value;
			else
				dataDic.Add(key, value);
		}

	}

}