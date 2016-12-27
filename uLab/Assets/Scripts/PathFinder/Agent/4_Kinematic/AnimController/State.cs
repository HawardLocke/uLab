
using UnityEngine;

namespace Lite.Anim
{
	public abstract class State
	{
		protected long guid;

		protected MotionType actionType = MotionType.None;

		public State()
		{
			guid = GuidGenerator.NextLong();

			actionType = MotionType.None;
		}

		public void Enter(KinematicAgent agent, Bev.Action action)
		{
			this.SetFinished(agent, false);
			this.OnEnter(agent, action);
		}

		public void Exit(KinematicAgent agent)
		{
			this.SetFinished(agent, false);
			this.OnExit(agent);
		}

		public void Update(KinematicAgent agent)
		{
			if (IsAnimLoopEnded())
				OnAnimationEnd(agent);
			this.OnUpdate(agent);
		}

		protected abstract void OnEnter(KinematicAgent agent, Bev.Action action);
		protected abstract void OnExit(KinematicAgent agent);
		protected abstract void OnUpdate(KinematicAgent agent);
		public virtual bool HandleAction(KinematicAgent agent, Bev.Action action) { return false; }
		protected virtual void OnAnimationEnd(KinematicAgent agent) { }
		protected virtual bool IsAnimLoopEnded() { return false; }

		public bool IsFinished(KinematicAgent agent)
		{
			return agent.blackboard.GetBool(guid, "isFinished");
		}

		public void SetFinished(KinematicAgent agent, bool value)
		{
			agent.blackboard.SetBool(guid, "isFinished", value);
		}

	}

}
