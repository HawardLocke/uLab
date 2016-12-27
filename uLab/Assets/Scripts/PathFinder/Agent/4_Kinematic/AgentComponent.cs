
using System.Collections.Generic;
using UnityEngine;


namespace Lite
{

	public class AgentComponent : IComponent
	{
		public KinematicAgent agent;

		private KinematicComponent _kinematic;
		public KinematicComponent kinematic { get { return _kinematic; } }

		private SteeringComponent _steering;
		public SteeringComponent steering { get { return _steering; } }

		private Queue<Bev.Action> actionQueue = new Queue<Bev.Action>();


		public override void OnStart()
		{
			_kinematic = GetComponent<KinematicComponent>();

			_steering = GetComponent<SteeringComponent>();
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