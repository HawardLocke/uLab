
using UnityEngine;

using System.Collections.Generic;

using Lite.Strategy;


namespace Lite
{

	public class CommandAgent : Agent
	{
		private Queue<Command> m_CommandQueue;

		public CommandAgent(long guid) :
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