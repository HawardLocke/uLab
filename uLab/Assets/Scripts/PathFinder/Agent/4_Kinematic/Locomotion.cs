using UnityEngine;
using System.Collections;

namespace Lite
{

	public class Locomotion : IComponent
	{
		private KinematicComponent m_kinematic;

		public bool displayTrack;

		public float damping = 0.9f;


		private CharacterController controller;
		private Rigidbody theRigidbody;


		public override void OnAwake()
		{
			
		}

		public override void OnStart()
		{
			m_kinematic = GetComponent<KinematicComponent>();
			controller = GetComponent<CharacterController>();
			theRigidbody = GetComponent<Rigidbody>();
		}

		public override void OnUpdate()
		{
			UpdateMovement();
		}

		private void UpdateMovement()
		{
			Vector3 velocity = GetKinematic().velocity;

			if (velocity.sqrMagnitude > 0.00001)
			{
				Vector3 moveDistance = velocity * Time.fixedDeltaTime;

				if (displayTrack)
					Debug.DrawLine(transform.position, transform.position + moveDistance, Color.black, 30.0f);

				if (controller != null)
				{
					controller.SimpleMove(velocity);
				}
				else if (theRigidbody == null || theRigidbody.isKinematic)
				{
					transform.position += moveDistance;
				}
				else
				{
					theRigidbody.MovePosition(theRigidbody.position + moveDistance);
				}

				// force position
				GetKinematic().position = transform.position;
				GetKinematic().forward = transform.forward;
			}

			// turning
			if (velocity.sqrMagnitude > 0.00001)
			{
				Vector3 newForward = Vector3.Slerp(transform.forward, velocity, damping * Time.deltaTime);
				transform.forward = newForward;
			}

		}

		public KinematicComponent GetKinematic()
		{
			return m_kinematic;
		}

	}

}
