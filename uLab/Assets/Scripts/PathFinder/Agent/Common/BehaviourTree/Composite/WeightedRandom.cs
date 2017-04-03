using UnityEngine;
using System;
using System.Collections;

namespace Lite.BevTree
{
	[AddNodeMenu("Composite/WeightedRandom")]
	public class WeightedRandom : Random
	{
		private float[] m_weights;

		protected override void OnOpen(Context context)
		{
			m_chosenChild = ChooseRandomChild();
		}

		private BehaviourNode ChooseRandomChild()
		{
			BehaviourNode child = null;

			float rand = UnityEngine.Random.value;
			for(int i = 0; i < m_children.Count; i++)
			{
				if (rand < m_children[i].Weight)
				{
					child = m_children[i];
					break;
				}

				rand -= m_children[i].Weight;
			}

			return child;
		}

	}

}