
using System.Collections.Generic;

using Lite.Strategy;


namespace Lite
{
	public class CommandHandlerManager
	{
		private Dictionary<int, ICommandHandler> m_handlerMap;

		public CommandHandlerManager()
		{
			m_handlerMap = new Dictionary<int, ICommandHandler>();
		}

		public void RegisterHandler(int CommandType, ICommandHandler handler)
		{
			m_handlerMap.Add(CommandType, handler);
		}

		public void UnregisterHandler(int CommandType)
		{
			m_handlerMap.Remove(CommandType);
		}

		public void HandleCommand(Command Command)
		{
			ICommandHandler handler = null;
			m_handlerMap.TryGetValue(Command.typeID, out handler);
			if (handler != null)
			{
				handler.OnCommand(Command);
			}
		}

	}

}