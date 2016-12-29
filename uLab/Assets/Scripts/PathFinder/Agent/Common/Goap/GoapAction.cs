

namespace Lite.Goap
{

	public abstract class Action
	{
		public ContextStatus preconditon;
		public ContextStatus effects;

		public Action(int stateCount)
		{
			preconditon = new ContextStatus(stateCount);
			effects = new ContextStatus(stateCount);
		}
	}

}