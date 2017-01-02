
using System.Collections.Generic;

using Lite.Strategy;


namespace Lite
{
	public interface ICommandHandler
	{
		void OnCommand(Command Command);
	}

}