
using UnityEngine;


namespace Lite
{
	public class Wandering : Steering
	{
		private Vector3 m_vWanderTarget;

		private float m_dWanderJitter = 2;

		//private float m_dWanderDistance = 1;

		/*public Wandering()
		{
			double theta = MathUtil.RandClamp() * MathUtil.PI;
			m_vWanderTarget = new Vector3(loco.wanderRadius * (float)System.Math.Cos(theta),
				0, loco.wanderRadius * (float)System.Math.Sin(theta));
		}*/

		public override Vector3 Calculate(LocomotionComponent loco)
		{
			m_vWanderTarget += new Vector3(MathUtil.RandClamp() * m_dWanderJitter, 0, MathUtil.RandClamp() * m_dWanderJitter);

			m_vWanderTarget = m_vWanderTarget.normalized * loco.wanderRadius;

			Vector3 Target = m_vWanderTarget + loco.velocity.normalized * loco.maxSpeed;

			return Target.normalized * loco.maxSpeed - loco.velocity;
		}

	}
}
