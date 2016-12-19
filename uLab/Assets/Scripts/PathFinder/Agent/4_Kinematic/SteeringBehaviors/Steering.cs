
using UnityEngine;


namespace Lite
{
	public abstract class Steering
	{
		protected KinematicComponent m_kinematic;

		public bool isOn;
		
		public float weight;

		public Steering(KinematicComponent agent)
		{
			m_kinematic = agent;
			weight = 1;
			isOn = false;
		}

		public abstract Vector3 Calculate();

	}
}
