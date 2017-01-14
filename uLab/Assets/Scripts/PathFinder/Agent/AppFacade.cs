

namespace Lite
{

	public class AppFacade : Singleton<AppFacade>
	{
		public Knowledge.SensorManager sensorManager { private set; get; }

		public Strategy.AgentManager stgAgentManager { private set; get; }

		public Cmd.AgentManager cmdAgentManager { private set; get; }

		public Bev.AgentManager bevAgentManager { private set; get; }

		long lastUpdateTime = 0;

		public AppFacade()
		{

		}

		public void Init()
		{
			sensorManager = new Knowledge.SensorManager();
			sensorManager.Init();

			stgAgentManager = new Strategy.AgentManager();
			stgAgentManager.Init();

			cmdAgentManager = new Cmd.AgentManager();
			cmdAgentManager.Init();

			bevAgentManager = new Bev.AgentManager();
			bevAgentManager.Init();
		}

		public void Update()
		{
			long ms = GameTimer.tickTime;
			if (ms - lastUpdateTime >= 200)
			{
				sensorManager.Update();
				stgAgentManager.Update();
				cmdAgentManager.Update();
				lastUpdateTime = ms;
			}
		}

	}

}