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
			Bev.MoveSpeed speedType = Bev.MoveSpeed.Normal;

			if (action.actionType == Bev.ActionType.MoveTo)
			{
				Bev.MoveTo moveTo = action as Bev.MoveTo;
				agent.blackboard.moveToAction = moveTo;
				speedType = moveTo.speed;

				agent.locomotion.StartMove(moveTo.target, GetSpeed(agent, moveTo.speed));
			}

			PlayAnim(agent, speedType);
			
		}

		protected override void OnExit(KinematicAgent agent)
		{

		}

		protected override void OnUpdate(KinematicAgent agent)
		{
			float deltaTime = this.GetDeltaUpdateTime(agent);
			if (agent.blackboard.moveToAction != null)
			{
				var target = agent.blackboard.moveToAction.target;
				float distanceSqr = MathUtil.DistanceSqr(target, agent.locomotion.position);
				float speed = GetSpeed(agent, agent.blackboard.moveToAction.speed);
				if (distanceSqr < speed * speed * deltaTime)
				{
					agent.blackboard.moveToAction = null;
					//Log.Info("arrived");
					SetFinished(agent, true);
				}
			}
		}

		public override bool HandleAction(KinematicAgent agent, Bev.Action action)
		{
			Bev.ActionType type = action.actionType;
			if (type == Bev.ActionType.MoveTo)
			{
				Bev.MoveTo moveTo = (Bev.MoveTo)action;
				agent.blackboard.moveToAction = moveTo;
				agent.locomotion.StartMove(moveTo.target, GetSpeed(agent, moveTo.speed));
				//PlayAnim(agent, moveTo.speed);
				return true;
			}
			return false;
		}

		private void PlayAnim(KinematicAgent agent, Bev.MoveSpeed speed)
		{
			string name;
			switch(speed)
			{
				case Bev.MoveSpeed.Slow:
					name = agent.animComponent.animSet.GetWalk(agent);
					break;
				case Bev.MoveSpeed.Normal:
					name = agent.animComponent.animSet.GetRun(agent);
					break;
				case Bev.MoveSpeed.Fast:
					name = agent.animComponent.animSet.GetRunFast(agent);
					break;
				default:
					name = agent.animComponent.animSet.GetWalk(agent);
					break;
			}
			agent.animComponent.Play(name);
		}

		private float GetSpeed(KinematicAgent agent, Bev.MoveSpeed speedType)
		{
			float speed = 0;
			switch (speedType)
			{
				case Bev.MoveSpeed.Slow:
					speed = agent.locomotion.maxWalkSpeed;
					break;
				case Bev.MoveSpeed.Normal:
					speed = agent.locomotion.maxRunSpeed;
					break;
				case Bev.MoveSpeed.Fast:
					speed = agent.locomotion.maxSprintSpeed;
					break;
				default:
					speed = agent.locomotion.maxRunSpeed;
					break;
			}
			return speed;
		}

	}

}