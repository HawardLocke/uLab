
using UnityEngine;
using System.Collections.Generic;

using Lite.Bev;


namespace Lite.Anim
{
	public class FsmSimple : Fsm
	{
		private enum StateType
		{
			Idle,
			Run,
			Attack
		}

		public FsmSimple()
		{
			mStateDic.Add((uint)StateType.Idle, new StateIdle());
			mStateDic.Add((uint)StateType.Run, new StateRun());
			mStateDic.Add((uint)StateType.Attack, new StateAttack());

			this.defaultAnimState = mStateDic[(int)StateType.Idle];
		}

		public override void DoAction(KinematicAgent agent, Bev.Action action)
		{
			if (agent.blackboard.currentAnimState == null || ! agent.blackboard.currentAnimState.HandleAction(agent, action))
			{
				ActionType type = action.actionType;
				if (type == ActionType.MoveTo)
				{
					agent.blackboard.nextAnimState = mStateDic[(uint)StateType.Run];
				}
				else if (type == ActionType.StopMove)
				{
					agent.blackboard.nextAnimState = mStateDic[(uint)StateType.Idle];
				}
				else if (type == ActionType.Attack)
				{
					agent.blackboard.nextAnimState = mStateDic[(uint)StateType.Attack];
				}
				else
				{
					action.Finish();
				}
				
				ChangeToNextState(agent, action);
			}
		}

	}

}
