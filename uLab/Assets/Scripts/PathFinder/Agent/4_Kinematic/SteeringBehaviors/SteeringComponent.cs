using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Lite
{
	public enum SteeringType
	{
		None = 0,
		Seek = 1,
		Arrive = 2,
		Wander = 4,
		Separation = 8,
		WallAvoidance = 16,
	}

	public class SteeringComponent : IComponent
	{
		private KinematicComponent m_kinematic;

		private Vector3 m_steeringForce;

		private List<Steering> m_steeringPriority;

		private Dictionary<SteeringType, Steering> m_steeringMap;

		private const float updateForceInterval = 0.2f;

		private float updateForceTimer;

		public override void OnAwake()
		{
			m_steeringForce = new Vector3(0,0,0);
			m_steeringPriority = new List<Steering>();
			m_steeringMap = new Dictionary<SteeringType, Steering>();

			updateForceTimer = 0;
		}

		public override void OnStart()
		{
			KinematicComponent kinm = GetComponent<KinematicComponent>();
			m_kinematic = kinm;

			if (kinm != null)
			{
				// register steerings
				Steering st;
				st = new Seeking(kinm);
				m_steeringPriority.Add(st);
				m_steeringMap.Add(SteeringType.Seek, st);
				st = new Arriving(kinm);
				m_steeringPriority.Add(st);
				m_steeringMap.Add(SteeringType.Arrive, st);
				st = new Wandering(kinm);
				m_steeringPriority.Add(st);
				m_steeringMap.Add(SteeringType.Wander, st);
				st = new Separating(kinm);
				m_steeringPriority.Add(st);
				m_steeringMap.Add(SteeringType.Separation, st);
				st = new WallAvoiding(kinm);
				m_steeringPriority.Add(st);
				m_steeringMap.Add(SteeringType.WallAvoidance, st);
				// ...
			}
		}

		public override void OnUpdate()
		{
			updateForceTimer += Time.deltaTime;
			if (updateForceTimer > updateForceInterval)
			{
				Calculate();
				updateForceTimer = 0;
			}
		}

		public KinematicComponent GetKinematic()
		{
			return m_kinematic;
		}

		public Vector3 Calculate()
		{
			m_steeringForce = new Vector3(0, 0, 0);

			Vector3 force;

			foreach (Steering s in m_steeringPriority)
			{
				if (!s.IsEnabled())
					continue;
				force = s.Calculate() * s.weight;
				if (!AccumulateForce(ref m_steeringForce, ref force))
					break;
			}

			return m_steeringForce;
		}

		private bool AccumulateForce(ref Vector3 forceNow, ref Vector3 addForce)
		{
			float lenNow = forceNow.magnitude;
			if (lenNow >= GetKinematic().maxForce)
			{
				return false;
			}
			else
			{
				float lenAdd = addForce.magnitude;
				if (lenNow + lenAdd > GetKinematic().maxForce)
				{
					addForce = addForce * ((GetKinematic().maxForce - lenNow) / lenAdd);
				}
				forceNow += addForce;
				return true;
			}
		}

		public Vector3 GetSteeringForce()
		{
			return m_steeringForce;
		}

		public void TurnSteeringOn(SteeringType st, bool isOn)
		{
			Steering ster = null;
			m_steeringMap.TryGetValue(st, out ster);
			if (ster != null)
			{
				if (isOn)
					ster.Enable();
				else
					ster.Disable();
			}
		}

		public bool IsSteeringOn(SteeringType st)
		{
			Steering ster = null;
			m_steeringMap.TryGetValue(st, out ster);
			if (ster != null)
				return ster.IsEnabled();
			return false;
		}

	}

}