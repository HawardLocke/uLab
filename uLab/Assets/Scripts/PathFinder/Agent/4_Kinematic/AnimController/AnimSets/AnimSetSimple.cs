

namespace Lite.Anim
{

	public class AnimSetSimple : AnimSet
	{
		public override string GetIdle(KinematicAgent agent)
		{
			return "idle";
		}

		public override string GetWalk(KinematicAgent agent)
		{
			return "walk";
		}

		public override string GetRun(KinematicAgent agent)
		{
			return "run";
		}

		public override string GetAttack(KinematicAgent agent)
		{
			return "attack";
		}

	}

}