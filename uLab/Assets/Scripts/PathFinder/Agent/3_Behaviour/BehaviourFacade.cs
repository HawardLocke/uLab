

namespace Lite
{
	public class BehaviourFacade : BaseFacade<BehaviourAgent>
	{
		private RequestHandlerManager requesthandlerManager;

		public BehaviourFacade()
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
