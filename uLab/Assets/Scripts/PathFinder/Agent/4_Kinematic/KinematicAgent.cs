
using UnityEngine;


namespace Lite
{

	public enum MotionType
	{
		None,
		Idle,
		Die,
		Walk,
		Run,
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

		public AgentAction action;

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