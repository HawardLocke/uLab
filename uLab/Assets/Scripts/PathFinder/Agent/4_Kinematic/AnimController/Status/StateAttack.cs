using UnityEngine;

namespace Lite.Anim
{
	public class StateAttack : State
	{

		public StateAttack()
		{

		}

		protected override void OnEnter(KinematicAgent agent, Bev.Action action)
		{
			if (action.actionType == Bev.ActionType.Attack)
			{
				Bev.Attack attackAction = action as Bev.Attack;
				agent.blackboard.attackAction = attackAction;
			}

			Log.Info("enter attack");
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
				Vector3 desiredDir = targetAgent.locomotion.position - agent.locomotion.position;
				if (Vector3.Angle(faceDir, desiredDir) > 10)
				{
					float deltaTime = this.GetDeltaUpdateTime(agent);
					Vector3 dir = Vector3.Slerp(faceDir, desiredDir, 5 * deltaTime);
					agent.locomotion.SetForward(dir);
				}
				else
				{
					string name = agent.animComponent.animSet.GetAttack(agent);
					if (!agent.animComponent.IsPlaying(name))
					{
						PlayAnim(agent);
						Log.Info("play attack");
					}
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

		protected override bool IsAnimLoopEnded(KinematicAgent agent)
		{
			string name = agent.animComponent.animSet.GetAttack(agent);
			if (agent.animComponent.IsPlaying(name))
			{
				float endTime = agent.blackboard.GetFloat(guid, "anim_end_time");
				return Time.timeSinceLevelLoad > endTime;
			}
			return false;
		}

		protected override void OnAnimationEnd(KinematicAgent agent)
		{
			SetFinished(agent, true);
			agent.blackboard.attackAction.Finish();
		}

		private void PlayAnim(KinematicAgent agent)
		{
			string name;
			name = agent.animComponent.animSet.GetAttack(agent);
			agent.animComponent.Play(name);
			agent.blackboard.SetFloat(guid, "anim_end_time", Time.timeSinceLevelLoad + agent.animComponent.GetAnimLenth(name));
		}

	}

}