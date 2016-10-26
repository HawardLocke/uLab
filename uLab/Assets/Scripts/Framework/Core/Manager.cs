

namespace Locke
{
	public abstract class Manager : IListener
	{
		public virtual void Initialize() { }

		public virtual void Destroy() { }

		public virtual void Update() { }

		public void ListenFor(string eventName)
		{
		}
		public void OnEvent(Event evnt)
		{
		}

	}

}