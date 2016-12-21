
using UnityEngine;

using System.Collections.Generic;


namespace Lite
{

	public class RequestAgent : Agent
	{
		private Queue<Request> m_requestQueue;

		public RequestAgent(long guid) :
			base(guid)
		{
			m_requestQueue = new Queue<Request>();
		}

		public void PushRequest(Request request)
		{
			m_requestQueue.Enqueue(request);
		}

		public void Process()
		{
			//filters
			//send to behavior layer
		}

	}

}