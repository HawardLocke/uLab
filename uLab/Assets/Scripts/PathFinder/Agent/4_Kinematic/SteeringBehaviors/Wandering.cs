
using UnityEngine;


namespace Lite
{
	public class Wandering : Steering
	{
		private Vector3 m_vWanderTarget;

		private float m_dWanderJitter = 2;

		//private float m_dWanderDistance = 1;

		public Wandering(KinematicComponent kinm) : 
			base(kinm)
		{
			double theta = MathUtil.RandClamp() * MathUtil.PI;
			m_vWanderTarget = new Vector3(GetKinematic().wanderRadius * (float)System.Math.Cos(theta),
				0, GetKinematic().wanderRadius * (float)System.Math.Sin(theta));
		}

		public override Vector3 Calculate()
		{
			m_vWanderTarget += new Vector3(MathUtil.RandClamp() * m_dWanderJitter, 0, MathUtil.RandClamp() * m_dWanderJitter);

			m_vWanderTarget = m_vWanderTarget.normalized * GetKinematic().wanderRadius;

			Vector3 Target = m_vWanderTarget + GetKinematic().velocity.normalized * GetKinematic().maxSpeed;

			return Target.normalized * GetKinematic().maxSpeed - GetKinematic().velocity;
		}

	}
}
