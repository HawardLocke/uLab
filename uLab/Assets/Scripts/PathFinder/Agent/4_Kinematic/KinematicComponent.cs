
using UnityEngine;


namespace Lite
{
	public class KinematicComponent
	{
		public float mass;

		public Vector3 position;

		public Vector3 velocity;

		public Vector3 acceleration;

		public Vector3 steeringForce;

		public float maxForce;

		public float maxSpeed;

		public bool isPlanar;

		public Vector3 targetPosition;


		private SteeringBehaviors steering;


		public KinematicComponent()
		{
			mass = 1;
			position = new Vector3(0,0,0);
			velocity = new Vector3(0, 0, 0);
			acceleration = new Vector3(0, 0, 0);
			steeringForce = new Vector3(0, 0, 0);
			maxForce = 10;
			maxSpeed = 10;
			isPlanar = true;
			targetPosition = new Vector3(0, 0, 0);

			steering = new SteeringBehaviors(this);
		}

		public void UpdateSteering()
		{
			steeringForce = steering.Calculate();
		}

		public void UpdatePosition(float deltaTime)
		{
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
			position += velocity * deltaTime;
		}

	}

}