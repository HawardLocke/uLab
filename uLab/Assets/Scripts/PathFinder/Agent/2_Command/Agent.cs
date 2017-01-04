

using System.Collections.Generic;
using Lite.Strategy;


namespace Lite.Cmd
{

	public class Agent : IAgent
	{
		private Queue<Command> m_CommandQueue;

		public Agent(long guid) :
			base(guid)
		{
			m_CommandQueue = new Queue<Command>();
		}

		public void PushCommand(Command Command)
		{
			m_CommandQueue.Enqueue(Command);
		}

		public void Process()
		{
			//filters
			//send to behavior layer
		}

	}

}