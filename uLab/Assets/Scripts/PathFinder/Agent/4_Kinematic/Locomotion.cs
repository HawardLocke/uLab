using UnityEngine;
using System.Collections;

namespace Lite
{

	public class Locomotion : MonoBehaviour
	{
		private KinematicComponent m_kinematic;

		public bool displayTrack;

		public float computeInterval = 0.2f;

		public float damping = 0.9f;

		private float timer;

		private CharacterController controller;
		private Rigidbody theRigidbody;


		void Awake()
		{
			m_kinematic = new KinematicComponent();
		}

		void Start()
		{
			timer = 0;
			controller = GetComponent<CharacterController>();
			theRigidbody = GetComponent<Rigidbody>();
		}

		void Update()
		{
			timer += Time.deltaTime;
			if (timer > computeInterval)
			{
				GetKinematic().UpdateSteering();

				timer = 0;
			}
		}

		void FixedUpdate()
		{
			GetKinematic().UpdatePosition(Time.fixedDeltaTime);

			Vector3 velocity = GetKinematic().velocity;

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
