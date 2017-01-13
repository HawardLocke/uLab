
using System.Collections;
using System.Collections.Generic;

using Lite.Goap;

namespace Lite.Strategy
{

	public class GoapPlan
	{
		private List<GoapAction> actionList;
		private int currentIndex;
		public bool isFinished { private set; get; }

		public GoapPlan()
		{
			actionList = new List<GoapAction>();
			currentIndex = 0;
			isFinished = false;
		}

		public void AddAction(GoapAction action)
		{
			actionList.Add(action);
		}

		public void Excute()
		{
			if (actionList.Count > 0)
			{
				GoapAction action = actionList[currentIndex];
				action.Update();
				if (action.IsFinished())
				{
					currentIndex++;
					if (currentIndex >= actionList.Count)
					{
						isFinished = true;
					}
				}
			}
			else
			{
				isFinished = true;
			}
		}

	}

}

