
using UnityEngine;
using System.Collections.Generic;

namespace Lite.Anim
{
	public class FsmSimple : StateMachine
	{
		private enum StateType
		{
			Idle,
			Die,
			Walk,
			Run,
			Jump,
			Attack,
		}

		public FsmSimple(KinematicAgent agent) :
			base(agent)
		{
			//mAnimStates.Add((int)StateType.Idle, new Idle(agent, locomotion, animator));
			//mAnimStates.Add((int)StateType.Die, new Die(agent, locomotion, animator));
			//mAnimStates.Add((int)StateType.Walk, new GoTo(agent, locomotion, animator));
			// run??
			//mAnimStates.Add((int)StateType.Jump, new AnimStateJumpTo(agent, locomotion, animator));
			//mAnimStates.Add((int)StateType.Attack, new AnimStateAttack(agent, locomotion, animator));

			agent.blackboard.defaultAnimState = mStateDic[(int)StateType.Idle];
		}

		public override void DoAction(KinematicAgent agent, Bev.Action action)
		{
			if (null != agent.blackboard.currentAnimState && agent.blackboard.currentAnimState.HandleAction(agent, action))
			{

			}
			else
			{
				/*if (action is Bev.ActionIdle)
					this.mNextState = mAnimStates[(int)StateType.Idle];
				else if (action is Bev.ActionDie)
					this.mNextState = mAnimStates[(int)StateType.Die];
				else if (action is Bev.ActionGoTo)
					this.mNextState = mAnimStates[(int)StateType.Walk];
				else if (action is Bev.ActionMoveTowards)
					this.mNextState = mAnimStates[(int)StateType.Walk];
				else if (action is Bev.ActionJumpTo)
					this.mNextState = mAnimStates[(int)StateType.Jump];
				else if (action is Bev.ActionAttack)
					this.mNextState = mAnimStates[(int)StateType.Attack];*/

				ProgressToNextState(agent, action);
			}
		}

	}

}
