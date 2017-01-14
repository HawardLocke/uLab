
using Lite.Cmd;


namespace Lite.Bev
{
	public class AgentManager : IAgentManager<Agent>
	{
		private CommandHandlerManager CommandhandlerManager;

		public AgentManager()
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

		public void HandleCommand(byte[] Command)
		{
			CommandhandlerManager.HandleCommand(Command);
		}

	}

}
