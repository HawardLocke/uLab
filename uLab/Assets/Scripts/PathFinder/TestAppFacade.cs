

namespace Lite
{

	public class TestAppFacade
	{
		private CommandFacade m_CommandFacade;
		public CommandFacade CommandFacade { get { return m_CommandFacade; } }

		private BehaviourFacade m_behaviorFacade;
		public BehaviourFacade behaviorFacade { get { return m_behaviorFacade; } }

		private BehaviourFacade m_behaviourFacade;
		public BehaviourFacade behaviourFacade { get { return m_behaviourFacade; } }


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
			m_behaviourFacade = new BehaviourFacade();
			m_CommandFacade.Init();
		}

	}

}