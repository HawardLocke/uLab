
using UnityEngine;


namespace Lite
{
	public abstract class Steering
	{
		private KinematicComponent m_kinematic;

		private bool isEnabled;
		
		public float weight;

		public Steering(KinematicComponent agent)
		{
			m_kinematic = agent;
			weight = 1;
			isEnabled = false;
		}

		public abstract Vector3 Calculate();

		public KinematicComponent GetKinematic()
		{
			return m_kinematic;
		}

		public void Enable()
		{
			isEnabled = true;
		}

		public void Disable()
		{
			isEnabled = false;
		}

		public bool IsEnabled()
		{
			return isEnabled;
		}

	}
}
