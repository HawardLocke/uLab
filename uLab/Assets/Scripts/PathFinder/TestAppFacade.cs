

namespace Lite
{

	public class TestAppFacade
	{
		public Strategy.AgentManager stgAgentManager { private set; get; }

		public Cmd.AgentManager cmdAgentManager { private set; get; }

		public Bev.AgentManager bevAgentManager { private set; get; }

		//public static TestAppFacade Instance;

		long lastUpdateTime = 0;

		public TestAppFacade()
		{
			//Instance = this;
		}

		public void Init()
		{
			stgAgentManager = new Strategy.AgentManager();
			stgAgentManager.Init();

			cmdAgentManager = new Cmd.AgentManager();
			cmdAgentManager.Init();

			bevAgentManager = new Bev.AgentManager();
			bevAgentManager.Init();
		}

		public void Update(long ms)
		{
			if (ms - lastUpdateTime >= 200)
			{
				stgAgentManager.Update(ms);
				lastUpdateTime = ms;
			}
		}

	}

}