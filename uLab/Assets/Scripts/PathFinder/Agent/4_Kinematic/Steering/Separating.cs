
using System.Collections.Generic;
using UnityEngine;


namespace Lite
{
	public class Separating : Steering
	{
		public override Vector3 Calculate(LocomotionComponent loco)
		{
			Vector3 SteeringForce = new Vector3(0,0,0);
			List<LocomotionComponent> neighbors = new List<LocomotionComponent>();
			foreach(var kinm in neighbors)
			{
				Vector3 ToAgent = loco.position - kinm.position;
				SteeringForce += ToAgent.normalized / ToAgent.magnitude;
			}
			return SteeringForce;
		}

	}
}
