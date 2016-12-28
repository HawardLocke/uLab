
using UnityEngine;


namespace Lite
{
	public class Seeking : Steering
	{
		public override Vector3 Calculate(LocomotionComponent loco)
		{
			Vector3 desiredVelocity = Vector3.Normalize(loco.targetPosition - loco.position) * loco.maxSpeed;

			return (desiredVelocity - loco.velocity);
		}

	}
}
