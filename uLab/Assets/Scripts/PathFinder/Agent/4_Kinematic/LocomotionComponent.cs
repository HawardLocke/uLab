using UnityEngine;
using System.Collections;

namespace Lite
{

	public class LocomotionComponent : KinematicComponent
	{
		public bool displayTrack;

		public float damping = 0.9f;


		private CharacterController controller;
		private Rigidbody theRigidbody;


		public override void OnStart()
		{
			controller = GetComponent<CharacterController>();
			theRigidbody = GetComponent<Rigidbody>();
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			UpdateMovement();
		}

		private void UpdateMovement()
		{
			if (this.velocity.sqrMagnitude > 0.00001)
			{
				Vector3 moveDistance = this.velocity * Time.fixedDeltaTime;

				if (displayTrack)
					Debug.DrawLine(transform.position, transform.position + moveDistance, Color.black, 30.0f);

				if (controller != null)
				{
					controller.SimpleMove(this.velocity);
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
				this.position = transform.position;
				this.forward = transform.forward;
			}

			// turning
			if (this.velocity.sqrMagnitude > 0.00001)
			{
				Vector3 newForward = Vector3.Slerp(transform.forward, this.velocity, damping * Time.deltaTime);
				transform.forward = newForward;
			}

		}

	}

}
