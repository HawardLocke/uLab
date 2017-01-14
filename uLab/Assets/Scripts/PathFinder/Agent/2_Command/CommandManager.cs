

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

		public void PushCommand(byte[] buffer)
		{
			commandList.Enqueue(buffer);
		}

		public void Update()
		{
			if (commandList.Count > 0)
			{
				byte[] cmd = commandList.Dequeue();
				AppFacade.Instance.bevAgentManager.HandleCommand(cmd);
			}
		}

	}

}