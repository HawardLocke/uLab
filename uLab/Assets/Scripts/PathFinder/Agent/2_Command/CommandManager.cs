

using System.Collections.Generic;
using Lite.Strategy;




namespace Lite.Cmd
{

	public class CommandManager
	{
		private Queue<byte[]> commandList = new Queue<byte[]>();

		public void Init()
		{

		}

		public void OnActionActive()
		{

		}

		public void PushCommand(AgentAction action)
		{
			if (CheckAction(action))
			{
				if (ConstDefine.STAND_ALONE)
					AppFacade.Instance.commandHandlerManager.HandleCommand(action);
				else
					commandList.Enqueue(action.ToBytes());
			}
		}

		public void Update()
		{
			if (commandList.Count > 0)
			{
				byte[] cmd = commandList.Dequeue();
				AppFacade.Instance.commandHandlerManager.HandleCommand(cmd);
			}
		}

		private bool CheckAction(AgentAction action)
		{
			return true;
		}

	}

}