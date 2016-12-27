

namespace Lite.Anim
{
	public abstract class AnimSet
	{
		public abstract string GetIdle(KinematicAgent agent);
		public abstract string GetWalk(KinematicAgent agent);
		public abstract string GetRun(KinematicAgent agent);
		public abstract string GetAttack(KinematicAgent agent);

	}

}