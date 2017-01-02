using UnityEngine;


namespace Lite.Bev
{

	public class MoveToPosition : AgentAction
	{
		public Vector3 target;
		public MoveSpeed speed;

		public MoveToPosition()
		{
			actionType = ActionType.MoveTo;
		}

		public override void OnActive(BehaviourAgent agent)
		{

		}

		public override void OnDeactive(BehaviourAgent agent)
		{

		}

		public override void OnProcess(BehaviourAgent agent)
		{

		}

	}

}