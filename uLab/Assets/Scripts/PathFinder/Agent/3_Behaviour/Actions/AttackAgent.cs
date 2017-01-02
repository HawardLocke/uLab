using UnityEngine;


namespace Lite.Bev
{
	public class AttackAgent : AgentAction
	{
		public BehaviourAgent targetAgent;

		public AttackAgent()
		{
			actionType = ActionType.Attack;
		}

		public override void OnActive(BehaviourAgent agent)
		{
			Log.Info("enter attack");
		}

		public override void OnDeactive(BehaviourAgent agent)
		{

		}

		public override void OnProcess(BehaviourAgent agent)
		{
			if (targetAgent != null)
			{
				Vector3 faceDir = agent.locomotion.forward;
				Vector3 desiredDir = targetAgent.locomotion.position - agent.locomotion.position;
				if (Vector3.Angle(faceDir, desiredDir) > 10)
				{
					float deltaTime = GameTimer.deltaTime;
					Vector3 dir = Vector3.Slerp(faceDir, desiredDir, 5 * deltaTime);
					agent.locomotion.SetForward(dir);
				}
				else
				{
					if (!agent.animComponent.attack)
					{
						agent.animComponent.attack = true;
						Log.Info("play attack");
					}
				}
			}
			else
			{
				this.Finish();
			}
		}

	}

}