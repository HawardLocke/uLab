using UnityEngine;

namespace Lite.Anim
{
	public class Attack : State
	{
		protected float endOfAnimTime;

		public Attack()
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

		protected override void OnAnimationEnd(KinematicAgent agent)
		{
			Debug.Log("test attack finished..");
		}

		protected virtual bool IsAnimLoopEnded()
		{
			return endOfAnimTime <= Time.timeSinceLevelLoad;
		}

		private void PlayAnim(KinematicAgent agent)
		{
			string name = agent.animComponent.animSet.GetAttack(agent);
			endOfAnimTime = Time.timeSinceLevelLoad + agent.animComponent.GetAnimLenth(name);
			agent.animComponent.Play(name);
		}

	}

}