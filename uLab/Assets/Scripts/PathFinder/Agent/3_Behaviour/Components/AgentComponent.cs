
using System.Collections.Generic;
using UnityEngine;


namespace Lite
{

	public class AgentComponent : IComponent
	{
		public BehaviourAgent agent;

		private Queue<Bev.AgentAction> actionQueue = new Queue<Bev.AgentAction>();

		private Bev.AgentAction currentAction;


		public override void OnStart()
		{
			
		}

		public override void OnUpdate()
		{
			ProcessActions();
		}

		public void PushAction(Bev.AgentAction action)
		{
			actionQueue.Enqueue(action);
		}

		private void ProcessActions()
		{
			if (currentAction == null || currentAction.IsFinished())
			{
				if (actionQueue.Count > 0)
				{
					Bev.AgentAction action = actionQueue.Dequeue();
					if (currentAction != null)
						currentAction.Deactive(agent);
					currentAction = action;
					currentAction.Active(agent);
				}
				else
				{
					currentAction = null;
				}
			}

			if (currentAction != null)
			{
				currentAction.OnProcess(agent);
			}
		}

	}

}