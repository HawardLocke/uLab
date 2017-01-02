using UnityEngine;


namespace Lite.Bev
{

	public class StopMoving : AgentAction
	{
		public Vector3 target;

		public StopMoving()
		{
			actionType = ActionType.StopMove;
		}

		public override void OnActive(BehaviourAgent agent)
		{
			agent.animComponent.moveSpeed = 0;
		}

		public override void OnDeactive(BehaviourAgent agent)
		{

		}

		public override void OnProcess(BehaviourAgent agent)
		{

		}

	}

}