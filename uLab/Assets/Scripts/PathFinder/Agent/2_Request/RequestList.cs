

using System.Collections.Generic;


namespace Lite
{

	public class RequestList
	{
		private long m_agentGuid;

		private Queue<Request> m_requestQueue;

		public void AddRequest(Request request)
		{
			m_requestQueue.Enqueue(request);
		}

		public void Process()
		{

		}


	}

}