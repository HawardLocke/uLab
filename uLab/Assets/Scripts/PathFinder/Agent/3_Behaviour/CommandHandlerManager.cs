
using System.Collections.Generic;

using Lite.Cmd;


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

		public void HandleCommand(byte[] cmd)
		{
			ICommandHandler handler = null;
			ByteBuffer bb = new ByteBuffer(cmd);
			int typeID = bb.ReadInt();
			long agentID = bb.ReadLong();
			byte[] buffer = bb.ReadBytes();
			Strategy.AgentAction action = ProtobufUtil.DeSerialize<Strategy.AgentAction>(buffer);
			m_handlerMap.TryGetValue(typeID, out handler);
			if (handler != null)
			{
				handler.OnCommand(action);
			}
		}

	}

}