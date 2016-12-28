
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
			mStateDic.Add((uint)StateType.Idle, new Idle());
			mStateDic.Add((uint)StateType.Run, new Run());
			mStateDic.Add((uint)StateType.Attack, new Attack());

			this.defaultAnimState = mStateDic[(int)StateType.Idle];
		}

		public override void DoAction(KinematicAgent agent, Bev.Action action)
		{
			if (agent.blackboard.currentAnimState == null || ! agent.blackboard.currentAnimState.HandleAction(agent, action))
			{
				ActionType type = action.type;
				if (type == ActionType.MoveTo)
				{
					agent.blackboard.nextAnimState = mStateDic[(uint)StateType.Run];
				}
				else if (type == ActionType.StopMove)
				{
					agent.blackboard.nextAnimState = mStateDic[(uint)StateType.Idle];
				}
				
				ChangeToNextState(agent, action);
			}
		}

	}

}
