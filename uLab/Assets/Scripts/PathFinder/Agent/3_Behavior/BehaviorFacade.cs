

namespace Lite
{
	public class BehaviorFacade : BaseFacade<BehaviorAgent>
	{
		private RequestHandlerManager requesthandlerManager;

		public BehaviorFacade()
		{
			requesthandlerManager = new RequestHandlerManager();
		}

		public void RegisterHandler(int requestType, IRequestHandler handler)
		{
			requesthandlerManager.RegisterHandler(requestType, handler);
		}

		public void UnregisterHandler(int requestType)
		{
			requesthandlerManager.UnregisterHandler(requestType);
		}

		public void HandleRequest(Request request)
		{
			requesthandlerManager.HandleRequest(request);
		}

	}

}
