
using UnityEngine;
using System.Collections.Generic;

namespace Lite.Anim
{

	public abstract class Fsm
	{
		protected Dictionary<int, State> mStateDic = new Dictionary<int, State>();

		protected State defaultAnimState;

		public Fsm()
		{
		}

		public abstract void DoAction(KinematicAgent agent, Bev.Action action);

		public void Update(KinematicAgent agent)
		{
			if (agent.blackboard.currentAnimState != null)
			{
				if (agent.blackboard.currentAnimState.IsFinished(agent))
				{
					agent.blackboard.currentAnimState.Exit(agent);
					agent.blackboard.currentAnimState = agent.blackboard.defaultAnimState;
					agent.blackboard.currentAnimState.Enter(agent, Bev.Nothing.Inst);
				}
				else
				{
					agent.blackboard.currentAnimState.Update(agent);
				}
			}
			else
			{
				agent.blackboard.currentAnimState = agent.blackboard.defaultAnimState;
				agent.blackboard.currentAnimState.Enter(agent, Bev.Nothing.Inst);
			}
		}

		protected void ChangeToNextState(KinematicAgent agent, Bev.Action action)
		{
			if (agent.blackboard.nextAnimState != null)
			{
				if (agent.blackboard.nextAnimState == agent.blackboard.currentAnimState)
				{
					agent.blackboard.nextAnimState = null;
					return;
				}

				if (agent.blackboard.currentAnimState != null)
					agent.blackboard.currentAnimState.Exit(agent);
				agent.blackboard.currentAnimState = agent.blackboard.nextAnimState;
				agent.blackboard.currentAnimState.Enter(agent, action);
				agent.blackboard.nextAnimState = null;
			}
		}

	}

}
