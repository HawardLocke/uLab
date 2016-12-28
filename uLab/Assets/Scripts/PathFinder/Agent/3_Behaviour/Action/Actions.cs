using UnityEngine;


namespace Lite.Bev
{
	public abstract class Action
	{
		public ActionType type;

		public Action()
		{
			type = ActionType.Default;
		}
	}

	public class Nothing : Action
	{
		private static Nothing _inst;
		public static Nothing Inst
		{ 
			get 
			{ 
				if (_inst == null) 
					_inst = new Nothing(); 
				return _inst; 
			} 
		}
	}

	public class MoveTo : Action
	{
		public Vector3 target;
		public MoveSpeed speed;

		public MoveTo(Vector3 target, MoveSpeed speed)
		{
			type = ActionType.MoveTo;
			this.target = target;
			this.speed = speed;
		}
	}

	public class StopMove : Action
	{
		public Vector3 target;

		public StopMove(Vector3 target)
		{
			type = ActionType.StopMove;
			this.target = target;
		}
	}

	public class Attack : Action
	{
		public KinematicAgent target;

		public Attack(KinematicAgent target)
		{
			type = ActionType.Attack;
			this.target = target;
		}
	}

}