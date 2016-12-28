
using UnityEngine;


namespace Lite
{
	public class Arriving : Steering
	{
		public override Vector3 Calculate(LocomotionComponent loco)
		{
			Vector3 toTarget = loco.targetPosition - loco.position;

			double distSqr = toTarget.sqrMagnitude;

			if (distSqr > 0)
			{
				float dist = toTarget.magnitude;

				const float DecelerationTweaker = 0.3f;

				float speed = dist / (DecelerationTweaker);

				speed = System.Math.Min(speed, loco.maxSpeed);

				Vector3 desiredVelocity = toTarget / dist * speed;

				return (desiredVelocity - loco.velocity);
			}

			return Vector3.zero;
		}

	}
}
