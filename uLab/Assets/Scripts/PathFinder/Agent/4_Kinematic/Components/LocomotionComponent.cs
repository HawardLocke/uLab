
using UnityEngine;


namespace Lite
{
	public class LocomotionComponent : IComponent
	{
		// configed members
		public float mass;

		public float maxWalkSpeed;

		public float maxRunSpeed;

		public float maxSprintSpeed;

		public float maxForce;

		public float wanderRadius;

		// dynamic members
		public Vector3 position = Vector3.zero;

		public Vector3 velocity = Vector3.zero;

		public float speed;

		public float speedSqr;

		public float maxSpeed;

		public Vector3 steeringForce = Vector3.zero;

		public Vector3 targetPosition = Vector3.zero;

		// constants
		public const bool isPlanar = true;
		public const float damping = 2f;

		// steering machine
		private SteeringMachine _steering;
		public SteeringMachine steeringMachine { get { return _steering; } }
		private const float updateSteeringInterval = 0.2f;
		private float updateSteeringForceTimer;
		
		// unity components
		private CharacterController controller;
		private Rigidbody theRigidbody;

		
		public override void OnAwake()
		{
			mass = 1;
			maxWalkSpeed = 1;
			maxRunSpeed = 2;
			maxSprintSpeed = 4;
			maxForce = 1;
			wanderRadius = 2;

			maxSpeed = maxRunSpeed;

			// steering
			updateSteeringForceTimer = Time.timeSinceLevelLoad + updateSteeringInterval;
		}

		public override void OnStart()
		{
			_steering = new SteeringMachine(this);

			controller = GetComponent<CharacterController>();
			theRigidbody = GetComponent<Rigidbody>();
		}

		public override void OnUpdate()
		{
			if (updateSteeringForceTimer > Time.timeSinceLevelLoad)
			{
				steeringForce = _steering.Calculate();
				updateSteeringForceTimer = Time.timeSinceLevelLoad + updateSteeringInterval;
			}

			UpdateMovement();
		}

		public void TurnSteeringOn(SteeringType st, bool isOn)
		{
			_steering.TurnSteeringOn(st, isOn);
		}

		public void SetPosition(float x, float y, float z)
		{
			position.Set(x, y, z);
			gameObject.transform.position = position;
		}

		public void StopMove()
		{
			_steering.TurnSteeringOn(SteeringType.Seek, false);
			this.velocity = Vector3.zero;
		}

		private void UpdateMovement()
		{
			float deltaTime = Time.deltaTime;
			velocity += steeringForce / mass * deltaTime;
			if (velocity.sqrMagnitude > maxSpeed * maxSpeed)
			{
				velocity = velocity.normalized * maxSpeed;
			}
			if (isPlanar)
			{
				velocity.Set(velocity.x, 0, velocity.z);
			}
			//position += velocity * deltaTime;

			this.speed = this.velocity.magnitude;
			this.speedSqr = this.velocity.sqrMagnitude;
			// unity
			if (this.speedSqr > 0.00001)
			{
				Vector3 moveDistance = this.velocity * Time.fixedDeltaTime;

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
			
				// rotating
				Vector3 newForward = Vector3.Slerp(transform.forward, this.velocity, damping * Time.deltaTime);
				transform.forward = newForward;
			}

		}


	}

}