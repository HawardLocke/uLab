
using UnityEngine;


namespace Lite.Anim
{

	public class AnimSetSimple : AnimSet
	{
		public override void Init(Animation animation)
		{
			animation["idle break"].wrapMode = WrapMode.Loop;
			animation["walk"].wrapMode = WrapMode.Loop;
			animation["run"].wrapMode = WrapMode.Loop;
			animation["run fast"].wrapMode = WrapMode.Loop;
		}

		public override string GetIdle(KinematicAgent agent)
		{
			return "idle break";
		}

		public override string GetWalk(KinematicAgent agent)
		{
			return "walk";
		}

		public override string GetRun(KinematicAgent agent)
		{
			return "run";
		}

		public override string GetRunFast(KinematicAgent agent)
		{
			return "run fast";
		}

		public override string GetAttack(KinematicAgent agent)
		{
			return "attack1";
		}

		public override string GetTurnBack(KinematicAgent agent)
		{
			return "Run";
		}

	}

}