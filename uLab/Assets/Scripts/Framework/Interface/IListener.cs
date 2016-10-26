

namespace Locke
{

	public interface IListener
	{
		void ListenFor(string eventName);
		void OnEvent(Event evnt);
	}

}
