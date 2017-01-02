
using UnityEngine;


namespace Lite
{

	public enum MotionType
	{
		None = 0,
		Idle,
		Die,
		Walk,
		Run,
		Sprint,
		Jump,
		Attack,
	}


	public class KinematicAgent : Agent
	{
		public AgentComponent agentComponent;

		public LocomotionComponent locomotion;

		public AnimationComponent animComponent;

		public MotionType currentMotionType;

		public KinematicBlackboard blackboard;


		public KinematicAgent(long guid):
			base(guid)
		{
			blackboard = new KinematicBlackboard(this);
		}

		public void PushAction(Bev.Action action)
		{
			agentComponent.PushAction(action);
		}

	}

}