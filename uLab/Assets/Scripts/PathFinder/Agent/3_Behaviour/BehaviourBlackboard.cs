
using System.Collections.Generic;


namespace Lite
{

	public class BehaviourBlackboard : Blackborad
	{
		public BehaviourAgent agent;

		//public Anim.State currentAnimState = null;
		//public Anim.State nextAnimState = null;

		//public Bev.MoveToPosition moveToAction;
		//public Bev.AttackAgent attackAction;

		public BehaviourBlackboard(BehaviourAgent agent)
		{
			this.agent = agent;
			
		}


	}

}