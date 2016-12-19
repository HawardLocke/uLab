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


		public SteeringBehaviors(KinematicComponent agent)
		{
			m_kinematic = agent;
			m_steeringForce = new Vector3(0,0,0);
			m_steeringPriority = new List<Steering>();
			m_steeringMap = new Dictionary<SteeringType, Steering>();
			//
			m_steeringPriority.Add(new Seeking(agent));
			m_steeringMap.Add(SteeringType.Seek, new Seeking(agent));
			m_steeringPriority.Add(new Arriving(agent));
			m_steeringMap.Add(SteeringType.Arrive, new Arriving(agent));
			m_steeringPriority.Add(new Wandering(agent));
			m_steeringMap.Add(SteeringType.Wander, new Wandering(agent));
			// ...
		}

		public Vector3 Calculate()
		{
			m_steeringForce = new Vector3(0, 0, 0);

			Vector3 force;

			foreach (Steering s in m_steeringPriority)
			{
				if (!s.isOn)
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
			if (lenNow >= m_kinematic.maxForce)
			{
				return false;
			}
			else
			{
				float lenAdd = addForce.magnitude;
				if (lenNow + lenAdd > m_kinematic.maxForce)
				{
					addForce = addForce * ((m_kinematic.maxForce - lenNow) / lenAdd);
				}
				forceNow += addForce;
				return true;
			}
		}

		public void SetSteeringOn(SteeringType st, bool isOn)
		{
			Steering ster = null;
			m_steeringMap.TryGetValue(st, out ster);
			if (ster != null)
				ster.isOn = isOn;
		}

	}

}