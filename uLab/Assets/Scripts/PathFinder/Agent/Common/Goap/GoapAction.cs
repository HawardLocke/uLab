

namespace Lite.Goap
{

	public abstract class GoapAction
	{
		public ContextStatus preconditon;
		public ContextStatus effect;

		public GoapAction()
		{
			preconditon = new ContextStatus();
			effect = new ContextStatus();
		}
	}

}