
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
			Walk,
			Run,
		}

		public FsmSimple()
		{
			mStateDic.Add((int)StateType.Idle, new Idle());
			mStateDic.Add((int)StateType.Walk, new Walk());
			mStateDic.Add((int)StateType.Run, new Run());

			this.defaultAnimState = mStateDic[(int)StateType.Idle];
		}

		public override void DoAction(KinematicAgent agent, Bev.Action action)
		{
			if (agent.blackboard.currentAnimState == null || ! agent.blackboard.currentAnimState.HandleAction(agent, action))
			{
				ActionType type = action.type;
				if (type == ActionType.MoveTo)
				{
					MoveTo moveTo = action as MoveTo;
					if (moveTo.speed == MoveTo.Speed.Slow)
						agent.blackboard.nextAnimState = mStateDic[(int)StateType.Walk];
					else if (moveTo.speed == MoveTo.Speed.Normal)
						agent.blackboard.nextAnimState = mStateDic[(int)StateType.Walk];
					else if (moveTo.speed == MoveTo.Speed.Fast)
						agent.blackboard.nextAnimState = mStateDic[(int)StateType.Run];
				}
				else if (type == ActionType.StopMove)
				{
					agent.blackboard.nextAnimState = mStateDic[(int)StateType.Idle];
				}
				
				ChangeToNextState(agent, action);
			}
		}

	}

}
