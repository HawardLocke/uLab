
using UnityEngine;


namespace Lite
{
	public class Seperating : Steering
	{

		public Seperating(KinematicComponent kinm) : 
			base(kinm)
		{

		}

		public override Vector3 Calculate()
		{
			Vector3 desiredVelocity = 
				Vector3.Normalize(m_kinematic.position - m_kinematic.targetPosition)
				* m_kinematic.maxSpeed;

			return (desiredVelocity - m_kinematic.velocity);
		}

	}
}
