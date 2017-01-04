

namespace Lite
{

	public class TestAppFacade
	{
		public Cmd.AgentManager cmdAgentManager { private set; get; }

		public Bev.AgentManager bevAgentManager { private set; get; }

		public static TestAppFacade Instance;

		public TestAppFacade()
		{
			Instance = this;
		}

		public void Init()
		{
			cmdAgentManager = new Cmd.AgentManager();
			cmdAgentManager.Init();

			bevAgentManager = new Bev.AgentManager();
			bevAgentManager.Init();
		}

	}

}