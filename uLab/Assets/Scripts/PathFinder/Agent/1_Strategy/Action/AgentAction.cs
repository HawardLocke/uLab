
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

		public bool IsFinished() { return isFinished; }

		public void SetFinished(bool value) { isFinished = value; }

	}

}