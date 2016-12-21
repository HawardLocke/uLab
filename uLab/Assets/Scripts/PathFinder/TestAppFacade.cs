

namespace Lite
{

	public class TestAppFacade
	{
		private RequestFacade m_requestFacade;
		public RequestFacade requestFacade { get { return m_requestFacade; } }

		private BehaviorFacade m_behaviorFacade;
		public BehaviorFacade behaviorFacade { get { return m_behaviorFacade; } }

		private KinematicFacade m_kinematicFacade;
		public KinematicFacade kinematicFacade { get { return m_kinematicFacade; } }


		public TestAppFacade()
		{
			
		}

		public void Init()
		{
			m_requestFacade = new RequestFacade();
			m_requestFacade.Init();
			m_behaviorFacade = new BehaviorFacade();
			m_requestFacade.Init();
			m_kinematicFacade = new KinematicFacade();
			m_requestFacade.Init();
		}

	}

}