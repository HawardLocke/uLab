
using UnityEngine;


namespace Lite
{
	public abstract class Steering
	{
		private bool isEnabled;
		
		public float weight;

		public Steering()
		{
			weight = 1;
			isEnabled = false;
		}

		public abstract Vector3 Calculate(LocomotionComponent loco);

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
