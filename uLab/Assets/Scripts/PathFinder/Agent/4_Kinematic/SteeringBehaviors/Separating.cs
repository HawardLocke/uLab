
using System.Collections.Generic;
using UnityEngine;


namespace Lite
{
	public class Separating : Steering
	{

		public Separating(KinematicComponent kinm) : 
			base(kinm)
		{

		}

		public override Vector3 Calculate()
		{
			Vector3 SteeringForce = new Vector3(0,0,0);
			List<KinematicComponent> neighbors = new List<KinematicComponent>();
			foreach(var kinm in neighbors)
			{
				Vector3 ToAgent = GetKinematic().position - kinm.position;
				SteeringForce += ToAgent.normalized / ToAgent.magnitude;
			}
			return SteeringForce;
		}

	}
}
