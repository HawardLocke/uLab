using UnityEngine;

namespace Lite.BevTree
{
	[AddNodeMenu("Composite/Random")]
	public class Random : Composite
	{
		protected BehaviourNode m_chosenChild;

		protected override void OnOpen(Context context)
		{
			if(m_children.Count > 0)
			{
				m_chosenChild = m_children[UnityEngine.Random.Range(0, m_children.Count)];
			}
			else
			{
				m_chosenChild = null;
			}
		}

		protected override RunningStatus OnTick(Context context)
		{
			if(m_chosenChild != null)
			{
				return m_chosenChild._tick(context);
			}

			return RunningStatus.Success;
		}
	}

}