
using UnityEngine;


namespace Lite
{

	public enum ActionType
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

		public ActionType currentMotionType;

		public Anim.AnimSet animationSet;

		public KinematicBlackboard blackboard;

		public AgentAction action;

		public KinematicAgent(long guid):
			base(guid)
		{
			blackboard = new KinematicBlackboard(this);
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