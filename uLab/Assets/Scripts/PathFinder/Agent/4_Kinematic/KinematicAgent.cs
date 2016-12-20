
using UnityEngine;


namespace Lite
{

	public class KinematicAgent : Agent
	{
		public AgentComponent agentComponent;

		public KinematicAgent(long guid):
			base(guid)
		{
			
		}


		public KinematicComponent GetKinematic()
		{
			return agentComponent.GetKinematic();
		}

		public SteeringComponent GetSteering()
		{
			return agentComponent.GetSteering();
		}

	}

}