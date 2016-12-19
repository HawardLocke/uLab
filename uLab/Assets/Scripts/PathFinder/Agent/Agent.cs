
using UnityEngine;


namespace Lite
{

	public class Agent : MonoBehaviour
	{
		public Blackborad blackboard;

		private KinematicComponent kinematic;

		public Agent()
		{
			blackboard = new Blackborad(this);
		}

		void Update()
		{
			
		}

		public KinematicComponent GetKinematic()
		{
			if (kinematic == null)
			{
				var loco = GetComponent<Locomotion>();
				if (loco != null)
					kinematic = loco.GetKinematic();
			}
			return kinematic;
		}

	}

}