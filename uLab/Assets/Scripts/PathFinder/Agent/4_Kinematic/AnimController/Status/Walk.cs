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
			Debug.Log("test walk finished..");
		}

	}

}