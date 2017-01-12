
using System.Collections;
using System.Collections.Generic;

using Lite.Goap;

namespace Lite.Strategy
{

	public class GoapPlan
	{
		private List<GoapAction> actionList;
		private int currentIndex;

		public GoapPlan()
		{
			actionList = new List<GoapAction>();
			currentIndex = 0;
		}

		public void Excute()
		{

		}

	}

}

