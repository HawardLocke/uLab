using UnityEngine;

namespace Lite.Anim
{
	public class Idle : State
	{

		public Idle()
		{

		}

		protected override void OnEnter(KinematicAgent agent, Bev.Action action)
		{
			PlayAnim(agent);
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