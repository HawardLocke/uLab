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

		protected override void OnAnimationEnd(KinematicAgent agent)
		{
			Debug.Log("test run finished..");
		}

	}

}