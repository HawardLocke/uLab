
using UnityEngine;


namespace Lite
{
	public class Wandering : Steering
	{

		public Wandering(KinematicComponent kinm) : 
			base(kinm)
		{

		}

		public override Vector3 Calculate()
		{
			//first, add a small random vector to the target's position
			m_vWanderTarget += Vector2D(RandomClamped() * m_dWanderJitter,
										RandomClamped() * m_dWanderJitter);

			//reproject this new vector back on to a unit circle
			m_vWanderTarget.Normalize();

			//increase the length of the vector to the same as the radius
			//of the wander circle
			m_vWanderTarget *= m_dWanderRadius;

			//move the target into a position WanderDist in front of the agent
			Vector2D target = m_vWanderTarget + Vector2D(m_dWanderDistance, 0);

			//project the target into world space
			Vector2D Target = PointToWorldSpace(target,
												 m_pRaven_Bot->Heading(),
												 m_pRaven_Bot->Side(),
												 m_pRaven_Bot->Pos());

			//and steer towards it
			return Target - m_pRaven_Bot->Pos();
		}

	}
}
