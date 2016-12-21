
using System.Collections.Generic;


namespace Lite
{
	public class RequestHandlerManager
	{
		private Dictionary<int, IRequestHandler> m_handlerMap;

		public RequestHandlerManager()
		{
			m_handlerMap = new Dictionary<int, IRequestHandler>();
		}

		public void RegisterHandler(int requestType, IRequestHandler handler)
		{
			m_handlerMap.Add(requestType, handler);
		}

		public void UnregisterHandler(int requestType)
		{
			m_handlerMap.Remove(requestType);
		}

		public void HandleRequest(Request request)
		{
			IRequestHandler handler = null;
			m_handlerMap.TryGetValue(request.typeID, out handler);
			if (handler != null)
			{
				handler.OnRequest(request);
			}
		}

	}

}