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

	public class SteeringBehaviors
	{
		private KinematicComponent m_kinematic;

		private Vector3 m_steeringForce;

		private List<Steering> m_steeringPriority;

		private Dictionary<SteeringType, Steering> m_steeringMap;


		public SteeringBehaviors(KinematicComponent kinm)
		{
			m_kinematic = kinm;
			m_steeringForce = new Vector3(0,0,0);
			m_steeringPriority = new List<Steering>();
			m_steeringMap = new Dictionary<SteeringType, Steering>();
			//
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
			st = new Seperating(kinm);
			m_steeringPriority.Add(st);
			m_steeringMap.Add(SteeringType.Separation, st);
			st = new WallAvoiding(kinm);
			m_steeringPriority.Add(st);
			m_steeringMap.Add(SteeringType.WallAvoidance, st);
			// ...
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

		public void TurnSteeringOn(SteeringType st)
		{
			Steering ster = null;
			m_steeringMap.TryGetValue(st, out ster);
			if (ster != null)
				ster.Enable();
		}

		public void TurnSteeringOff(SteeringType st)
		{
			Steering ster = null;
			m_steeringMap.TryGetValue(st, out ster);
			if (ster != null)
				ster.Disable();
		}

	}

}