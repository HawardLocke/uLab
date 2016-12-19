
using UnityEngine;


namespace Lite
{
	public class Wandering : Steering
	{
		private Vector3 m_vWanderTarget;

		private float m_dWanderJitter = 1;

		private float m_dWanderDistance = 2;

		public Wandering(KinematicComponent kinm) : 
			base(kinm)
		{
			double theta = MathUtil.RandClamp() * MathUtil.PI;
			m_vWanderTarget = new Vector3(GetKinematic().wanderRadius * (float)System.Math.Cos(theta),
				0, GetKinematic().wanderRadius * (float)System.Math.Sin(theta));
		}

		public override Vector3 Calculate()
		{
			//first, add a small random vector to the target's position
			m_vWanderTarget += new Vector3(MathUtil.RandClamp() * m_dWanderJitter,
										0, MathUtil.RandClamp() * m_dWanderJitter);

			//reproject this new vector back on to a unit circle
			m_vWanderTarget.Normalize();

			//increase the length of the vector to the same as the radius
			//of the wander circle
			m_vWanderTarget *= GetKinematic().wanderRadius;

			//move the target into a position WanderDist in front of the agent
			Vector3 target = m_vWanderTarget + new Vector3(m_dWanderDistance, 0, 0);

			//project the target into world space
			Vector3 Target = GetKinematic().position + target;// GetKinematic().forward;
			/*PointToWorldSpace(target,
												 m_pRaven_Bot->Heading(),
												 m_pRaven_Bot->Side(),
												 m_pRaven_Bot->Pos());*/

			//and steer towards it
			return Target - GetKinematic().position;
		}

	}
}
