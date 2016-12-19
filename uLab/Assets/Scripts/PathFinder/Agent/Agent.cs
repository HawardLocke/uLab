
using UnityEngine;


namespace Lite
{

	public class Agent : IComponent
	{
		private Blackborad blackboard;

		private KinematicComponent kinematic;

		private SteeringComponent steering;


		public override void OnAwake()
		{
			blackboard = new Blackborad(this);
		}

		public override void OnStart()
		{
			kinematic = GetComponent<KinematicComponent>();

			steering = GetComponent<SteeringComponent>();
		}

		public override void OnUpdate()
		{
			
		}

		public Blackborad GetBlackborad()
		{
			return blackboard;
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