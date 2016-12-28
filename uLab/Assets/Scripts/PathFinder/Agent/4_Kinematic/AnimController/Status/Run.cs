using UnityEngine;

namespace Lite.Anim
{
	public class Run : State
	{
		
		public Run()
		{

		}

		protected override void OnEnter(KinematicAgent agent, Bev.Action action)
		{
			if (action.type == Bev.ActionType.MoveTo)
			{
				Bev.MoveTo moveTo = action as Bev.MoveTo;
				agent.blackboard.moveToAction = moveTo;
				if (moveTo.speed == Bev.MoveTo.Speed.Slow)
					agent.locomotion.maxSpeed = agent.locomotion.maxWalkSpeed;
				else if (moveTo.speed == Bev.MoveTo.Speed.Normal)
					agent.locomotion.maxSpeed = agent.locomotion.maxRunSpeed;
				else if (moveTo.speed == Bev.MoveTo.Speed.Fast)
					agent.locomotion.maxSpeed = agent.locomotion.maxSprintSpeed;
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