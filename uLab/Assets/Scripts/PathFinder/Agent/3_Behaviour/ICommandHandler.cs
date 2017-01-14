
using System.Collections.Generic;


namespace Lite
{
	public interface ICommandHandler
	{
		void OnCommand(Strategy.AgentAction Command);
	}

}