
using Lite.Strategy;


namespace Lite
{
	public class BehaviourFacade : BaseFacade<BehaviourAgent>
	{
		private CommandHandlerManager CommandhandlerManager;

		public BehaviourFacade()
		{
			CommandhandlerManager = new CommandHandlerManager();
		}

		public void RegisterHandler(int CommandType, ICommandHandler handler)
		{
			CommandhandlerManager.RegisterHandler(CommandType, handler);
		}

		public void UnregisterHandler(int CommandType)
		{
			CommandhandlerManager.UnregisterHandler(CommandType);
		}

		public void HandleCommand(Command Command)
		{
			CommandhandlerManager.HandleCommand(Command);
		}

	}

}
