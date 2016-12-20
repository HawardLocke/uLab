
using UnityEngine;


namespace Lite
{

	public class AgentComponent : IComponent
	{
		public KinematicAgent agent;

		private KinematicComponent kinematic;

		private SteeringComponent steering;


		public override void OnStart()
		{
			kinematic = GetComponent<KinematicComponent>();

			steering = GetComponent<SteeringComponent>();
		}

		public KinematicComponent GetKinematic()
		{
			return kinematic;
		}

		public SteeringComponent GetSteering()
		{
			return steering;
		}

	}

}