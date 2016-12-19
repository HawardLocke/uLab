
using UnityEngine;


namespace Lite
{
	public class WallAvoiding : Steering
	{

		public WallAvoiding(KinematicComponent kinm) : 
			base(kinm)
		{

		}

		public override Vector3 Calculate()
		{
			return Vector3.zero;
		}

	}
}
