
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


	public class BehaviourAgent : Agent
	{
		public AgentComponent agentComponent;

		public LocomotionComponent locomotion;

		public AnimationComponent animComponent;

		public MotionType currentMotionType;

		public BehaviourBlackboard blackboard;


		public BehaviourAgent(long guid) :
			base(guid)
		{
			blackboard = new BehaviourBlackboard(this);
		}

		public void PushAction(Bev.AgentAction action)
		{
			agentComponent.PushAction(action);
		}

	}

}