
using System.Collections.Generic;
using UnityEngine;


namespace Lite
{

	public class AgentComponent : IComponent
	{
		public KinematicAgent agent;

		private Queue<Bev.Action> actionQueue = new Queue<Bev.Action>();

		private Bev.Action currentAction;


		public override void OnStart()
		{
			
		}

		public override void OnUpdate()
		{
			ProcessActions();
		}

		public void PushAction(Bev.Action action)
		{
			actionQueue.Enqueue(action);
		}

		private void ProcessActions()
		{
			if (currentAction == null || currentAction.IsFinished())
			{
				if (actionQueue.Count > 0)
				{
					Bev.Action action = actionQueue.Dequeue();
					currentAction = action;
					//agent.animComponent.HandleAction(action);
				}
				else
				{
					currentAction = null;
				}
			}

			if (currentAction != null)
			{
				currentAction.OnProcess();
			}
		}

	}

}