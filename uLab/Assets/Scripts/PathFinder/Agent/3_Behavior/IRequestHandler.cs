
using System.Collections.Generic;


namespace Lite
{
	public interface IRequestHandler
	{
		void OnRequest(Request request);
	}

}