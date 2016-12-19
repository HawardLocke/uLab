
using UnityEngine;


namespace Lite
{
	public class Arriving : Steering
	{

		public Arriving(KinematicComponent kinm) : 
			base(kinm)
		{

		}

		public override Vector3 Calculate()
		{
			Vector3 toTarget = GetKinematic().targetPosition - GetKinematic().position;

			double distSqr = toTarget.sqrMagnitude;

			if (distSqr > 0)
			{
				float dist = toTarget.magnitude;

				const float DecelerationTweaker = 0.3f;

				float speed = dist / (DecelerationTweaker);

				speed = System.Math.Min(speed, GetKinematic().maxSpeed);

				Vector3 desiredVelocity = toTarget / dist * speed;

				return (desiredVelocity - GetKinematic().velocity);
			}

			return Vector3.zero;
		}

	}
}
