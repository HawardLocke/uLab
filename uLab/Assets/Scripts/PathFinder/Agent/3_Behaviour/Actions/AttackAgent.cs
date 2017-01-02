using UnityEngine;


namespace Lite.Bev
{
	public class AttackAgent : AgentAction
	{
		public BehaviourAgent target;

		public AttackAgent()
		{
			actionType = ActionType.Attack;
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