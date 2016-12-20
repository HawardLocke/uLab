
using UnityEngine;


namespace Lite
{
	public class KinematicComponent : IComponent
	{
		public float mass;

		public Vector3 position;

		public Vector3 forward;

		public Vector3 velocity;

		public Vector3 acceleration;

		public Vector3 steeringForce;

		public float maxForce;

		public float maxSpeed;

		public bool isPlanar;

		public Vector3 targetPosition;

		public float wanderRadius;


		public override void OnAwake()
		{
			mass = 1;
			position = new Vector3(0, 0, 0);
			velocity = new Vector3(0, 0, 0);
			acceleration = new Vector3(0, 0, 0);
			steeringForce = new Vector3(0, 0, 0);
			maxForce = 1;
			maxSpeed = 2;
			isPlanar = true;
			targetPosition = new Vector3(0, 0, 0);
			wanderRadius = 2;
		}

		public override void OnUpdate()
		{
			UpdatePosition();
		}

		private void UpdatePosition()
		{
			float deltaTime = Time.deltaTime;
			acceleration = steeringForce / mass;
			velocity += acceleration * deltaTime;
			if (velocity.sqrMagnitude > maxSpeed * maxSpeed)
			{
				velocity = velocity.normalized * maxSpeed;
			}
			if (isPlanar)
			{
				velocity.Set(velocity.x, 0, velocity.z);
			}
			//position += velocity * deltaTime;
		}

		public void SetPosition(float x, float y, float z)
		{
			position.Set(x, y, z);
			gameObject.transform.position = position;
		}

	}

}