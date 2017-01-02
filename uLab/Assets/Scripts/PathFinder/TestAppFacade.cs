

namespace Lite
{

	public class TestAppFacade
	{
		private CommandFacade m_CommandFacade;
		public CommandFacade CommandFacade { get { return m_CommandFacade; } }

		private BehaviourFacade m_behaviorFacade;
		public BehaviourFacade behaviorFacade { get { return m_behaviorFacade; } }

		private KinematicFacade m_kinematicFacade;
		public KinematicFacade kinematicFacade { get { return m_kinematicFacade; } }


		public static TestAppFacade Instance;

		public TestAppFacade()
		{
			Instance = this;
		}

		public void Init()
		{
			m_CommandFacade = new CommandFacade();
			m_CommandFacade.Init();
			m_behaviorFacade = new BehaviourFacade();
			m_CommandFacade.Init();
			m_kinematicFacade = new KinematicFacade();
			m_CommandFacade.Init();
		}

	}

}