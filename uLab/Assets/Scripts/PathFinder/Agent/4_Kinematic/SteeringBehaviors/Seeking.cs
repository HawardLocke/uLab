
using UnityEngine;


namespace Lite
{
	public class Seeking : Steering
	{

		public Seeking(KinematicComponent kinm) : 
			base(kinm)
		{

		}

		public override Vector3 Calculate()
		{
			Vector3 desiredVelocity = Vector3.Normalize(GetKinematic().targetPosition - GetKinematic().position) * GetKinematic().maxSpeed;

			return (desiredVelocity - GetKinematic().velocity);
		}

	}
}
