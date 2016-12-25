

namespace Lite.Anim
{
	public class AnimSet
	{
		private string mDefaultName = "idle";

		public string GetAnimName(ActionType motion)
		{
			string name = mDefaultName;

			switch (motion)
			{
				case ActionType.Idle:
					name = "idle";
					break;
				case ActionType.Die:
					name = "death";
					break;
				case ActionType.Walk:
					name = "walk";
					break;
				case ActionType.Run:
					name = "run";
					break;
				case ActionType.Jump:
					name = "jump";
					break;
				case ActionType.Attack:
					name = "attack";
					break;
			}

			return name;
		}

	}

}