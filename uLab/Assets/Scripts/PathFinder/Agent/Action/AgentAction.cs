
namespace Lite
{

	public abstract class AgentAction
	{
		private bool isFinished = false;

		public void Active()
		{
			OnActive();
		}

		public void Update()
		{
			OnUpdate();
		}

		public void Terminate()
		{
			OnTerminate();
		}

		public abstract void OnActive();

		public abstract void OnUpdate();

		public abstract void OnTerminate();

		public virtual bool IsFinished() { return isFinished; }

		public virtual bool SetFinished(bool value) { isFinished = value; }

	}

}