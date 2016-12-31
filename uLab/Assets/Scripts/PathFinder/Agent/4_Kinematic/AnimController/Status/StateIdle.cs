using UnityEngine;

namespace Lite.Anim
{
	public class StateIdle : State
	{

		public StateIdle()
		{

		}

		protected override void OnEnter(KinematicAgent agent, Bev.Action action)
		{
			//agent.locomotion.StopMove();
			PlayAnim(agent);
			Log.Info("enter idle");
		}

		protected override void OnExit(KinematicAgent agent)
		{

		}

		protected override void OnUpdate(KinematicAgent agent)
		{

		}

		public override bool HandleAction(KinematicAgent agent, Bev.Action action)
		{
			return false;
		}

		private void PlayAnim(KinematicAgent agent)
		{
			string name = agent.animComponent.animSet.GetIdle(agent);
			agent.animComponent.Play(name);
		}

	}

}