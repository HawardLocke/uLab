
using System.Collections.Generic;

using Lite.Cmd;


namespace Lite.Bev
{
	public abstract class ICommandHandler
	{
		public abstract Strategy.AgentAction ToAction(byte[] Command);

		public abstract void OnCommand(Strategy.AgentAction action);

		public void OnCommand(byte[] Command)
		{
			Strategy.AgentAction action = ToAction(Command);
			OnCommand(action);
		}

	}

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

		public void HandleCommand(Strategy.AgentAction action)
		{
			ICommandHandler handler = null;
			m_handlerMap.TryGetValue(action.actionType, out handler);
			if (handler != null)
			{
				handler.OnCommand(action);
			}
		}

		public void HandleCommand(byte[] cmd)
		{
			ByteBuffer bb = new ByteBuffer(cmd);
			int typeID = bb.ReadInt();
			long agentID = bb.ReadLong();
			byte[] buffer = bb.ReadBytes();
			//Strategy.AgentAction action = ProtobufUtil.DeSerialize<Strategy.AgentAction>(buffer);

			ICommandHandler handler = null;
			m_handlerMap.TryGetValue(typeID, out handler);
			if (handler != null)
			{
				handler.OnCommand(buffer);
			}
		}

	}

}