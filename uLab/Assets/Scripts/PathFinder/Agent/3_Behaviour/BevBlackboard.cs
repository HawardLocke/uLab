
using System.Collections.Generic;


namespace Lite.Bev
{

	public class BevBlackboard : Blackborad
	{
		public Agent agent;

		//public Anim.State currentAnimState = null;
		//public Anim.State nextAnimState = null;

		//public Bev.MoveToPosition moveToAction;
		//public Bev.AttackAgent attackAction;

		public BevBlackboard(Agent agent)
		{
			this.agent = agent;
			
		}


	}

}