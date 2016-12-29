

namespace Lite.Goap
{

	public class Planner
	{
		private Action openList;
		private Action closeList;

		public void Plan()
		{
			openList = null;
			closeList = null;
		}

	}

}