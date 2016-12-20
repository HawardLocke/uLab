
using System.Collections.Generic;


namespace Lite
{
	public class RequestHandlerManager : Singleton<RequestHandlerManager>
	{
		private Dictionary<int, IRequestHandler> m_handlerMap;

		public RequestHandlerManager()
		{
			m_handlerMap = new Dictionary<int, IRequestHandler>();
		}

		public void AddHandler(int requestID, IRequestHandler handler)
		{
			m_handlerMap.Add(requestID, handler);
		}

		public void HandleRequest()
		{
			int requestID = 0xfff;
			IRequestHandler handler = null;
			m_handlerMap.TryGetValue(requestID, out handler);
			if (handler != null)
			{
				handler.OnRequest();
			}
		}

	}

}