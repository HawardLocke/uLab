using UnityEngine;

namespace Lite.Anim
{
	public class Walk : State
	{
		
		public Walk()
		{

		}

		protected override void OnEnter(KinematicAgent agent, Bev.Action action)
		{
			if (action.type == Bev.ActionType.MoveTo)
			{
				agent.blackboard.moveToAction = action as Bev.MoveTo;
			}
			PlayAnim(agent);
		}

		protected override void OnExit(KinematicAgent agent)
		{

		}

		protected override void OnUpdate(KinematicAgent agent)
		{
			float deltaTime = this.GetDeltaUpdateTime(agent);
			if (agent.blackboard.moveToAction != null)
			{
				float distanceSqr = (agent.blackboard.moveToAction.target - agent.locomotion.position).sqrMagnitude;
				if (distanceSqr < agent.locomotion.speed * agent.locomotion.speed * deltaTime)
				{
					agent.blackboard.moveToAction = null;
					Log.Info("arrived");
					SetFinished(agent, true);
				}
			}
		}

		public override bool HandleAction(KinematicAgent agent, Bev.Action action)
		{
			return false;
		}

		private void PlayAnim(KinematicAgent agent)
		{
			string name = agent.animComponent.animSet.GetWalk(agent);
			agent.animComponent.Play(name);
		}

	}

}