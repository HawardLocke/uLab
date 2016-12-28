using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Lite
{

	public class SteeringMachine
	{
		private LocomotionComponent locomotion;

		private List<Steering> m_steeringPriority;

		private Dictionary<SteeringType, Steering> m_steeringMap;
		
		public SteeringMachine(LocomotionComponent locomotion)
		{
			this.locomotion = locomotion;
			m_steeringPriority = new List<Steering>();
			m_steeringMap = new Dictionary<SteeringType, Steering>();

			AddBehaviour(new Seeking(), SteeringType.Seek);
			AddBehaviour(new Arriving(), SteeringType.Arrive);
			AddBehaviour(new Wandering(), SteeringType.Wander);
			AddBehaviour(new Separating(), SteeringType.Separation);
			AddBehaviour(new WallAvoiding(), SteeringType.WallAvoidance);
		}

		private void AddBehaviour(Steering steering, SteeringType type)
		{
			m_steeringPriority.Add(steering);
			m_steeringMap.Add(type, steering);
		}

		public Vector3 Calculate()
		{
			Vector3 steeringForce = new Vector3(0, 0, 0);

			foreach (Steering s in m_steeringPriority)
			{
				if (!s.IsEnabled())
					continue;
				Vector3 force = s.Calculate(locomotion) * s.weight;
				if (!AccumulateForce(ref steeringForce, ref force))
					break;
			}

			return steeringForce;
		}

		private bool AccumulateForce(ref Vector3 forceNow, ref Vector3 addForce)
		{
			float lenNow = forceNow.magnitude;
			if (lenNow >= locomotion.maxForce)
			{
				return false;
			}
			else
			{
				float lenAdd = addForce.magnitude;
				if (lenNow + lenAdd > locomotion.maxForce)
				{
					addForce = addForce * ((locomotion.maxForce - lenNow) / lenAdd);
				}
				forceNow += addForce;
				return true;
			}
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