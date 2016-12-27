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
			agent.animComponent.Play(agent.animComponent.animSet.GetIdle(agent));
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

		protected override void OnAnimationFinished(KinematicAgent agent)
		{
			Debug.Log("test idle finished..");
		}

	}

}