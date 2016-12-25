
using System.Collections.Generic;


namespace Lite
{

	public class KinematicBlackboard : Blackborad
	{
		public KinematicAgent agent;

		public Anim.State currentAnimState = null;
		public Anim.State nextAnimState = null;
		public Anim.State defaultAnimState = null;

		public KinematicBlackboard(KinematicAgent agent)
		{
			this.agent = agent;
			
		}


	}

}