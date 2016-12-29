using UnityEngine;

namespace Lite.Anim
{
	public class Attack : State
	{

		public Attack()
		{

		}

		protected override void OnEnter(KinematicAgent agent, Bev.Action action)
		{
			if (action.actionType == Bev.ActionType.Attack)
			{
				Bev.Attack attackAction = action as Bev.Attack;
				agent.blackboard.attackAction = attackAction;
			}

			//PlayAnim(agent);

		}

		protected override void OnExit(KinematicAgent agent)
		{

		}

		protected override void OnUpdate(KinematicAgent agent)
		{
			KinematicAgent targetAgent = agent.blackboard.attackAction.target;
			if (targetAgent != null)
			{
				Vector3 faceDir = agent.locomotion.forward;
				Vector3 desiredDir = targetAgent.locomotion.position;
				if (Vector3.Angle(faceDir, desiredDir) > 10)
				{
					float deltaTime = this.GetDeltaUpdateTime(agent);
					Vector3 dir = Vector3.Slerp(faceDir, desiredDir, 5 * deltaTime);
					agent.locomotion.SetForward(dir);
				}
				else
				{
					PlayAnim(agent);
				}
			}
			else
			{
				SetFinished(agent, true);
			}
		}

		public override bool HandleAction(KinematicAgent agent, Bev.Action action)
		{
			return false;
		}

		private void PlayAnim(KinematicAgent agent)
		{
			string name;
			name = agent.animComponent.animSet.GetAttack(agent);
			agent.animComponent.Play(name);
		}

	}

}