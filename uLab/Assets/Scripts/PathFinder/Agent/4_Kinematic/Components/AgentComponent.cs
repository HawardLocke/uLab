
using System.Collections.Generic;
using UnityEngine;


namespace Lite
{

	public class AgentComponent : IComponent
	{
		public KinematicAgent agent;

		private Queue<Bev.Action> actionQueue = new Queue<Bev.Action>();


		public override void OnStart()
		{
			
		}

		public override void OnUpdate()
		{
			if (actionQueue.Count > 0)
			{
				Bev.Action action = actionQueue.Dequeue();

				agent.animComponent.HandleAction(action);
			}
		}

		public void PushAction(Bev.Action action)
		{
			actionQueue.Enqueue(action);
		}

	}

}