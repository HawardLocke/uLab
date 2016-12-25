
using UnityEngine;
using System.Collections.Generic;

namespace Lite.Anim
{

	public abstract class StateMachine
	{
		protected Dictionary<int, State> mStateDic = new Dictionary<int, State>();

		public StateMachine(KinematicAgent agent)
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
					agent.blackboard.currentAnimState.Enter(agent, null);
				}
				else
				{
					agent.blackboard.currentAnimState.Update(agent);
				}
			}
			else
			{
				agent.blackboard.currentAnimState = agent.blackboard.defaultAnimState;
				agent.blackboard.currentAnimState.Enter(agent, null);
			}
		}

		protected void ProgressToNextState(KinematicAgent agent, Bev.Action action)
		{
			if (agent.blackboard.nextAnimState != null)
			{
				if (agent.blackboard.nextAnimState == agent.blackboard.currentAnimState)
				{
					agent.blackboard.nextAnimState = null;
					return;
				}

				if (null != agent.blackboard.currentAnimState)
					agent.blackboard.currentAnimState.Exit(agent);
				agent.blackboard.currentAnimState = agent.blackboard.nextAnimState;
				agent.blackboard.currentAnimState.Enter(agent, action);
				agent.blackboard.nextAnimState = null;
			}
		}

	}

}
