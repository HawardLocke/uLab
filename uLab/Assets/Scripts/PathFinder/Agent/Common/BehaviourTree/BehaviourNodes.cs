
using System.Collections.Generic;


namespace Lite.BevTree
{

	public enum NodeType
	{
		Default,
		Composite,
		Decorator,
		Action
	}


	public abstract class BehaviourNode
	{
		public long guid;
		public NodeType nodeType;
		public BehaviourNode parent;
		protected List<BehaviourNode> m_children;

		public RunningStatus lastRet;

		public BehaviourNode()
		{
			guid = GuidGen.NextLong();
			m_children = new List<BehaviourNode>();
		}

		protected virtual void OnOpen(Context context) { }

		protected virtual void OnClose(Context context) { }

		protected virtual void OnEnter(Context context) { }

		protected virtual void OnExit(Context context) { }

		protected abstract RunningStatus OnTick(Context context);

		public void _enter(Context context)
		{
			OnEnter(context);
		}

		public void _exit(Context context)
		{
			OnExit(context);
		}

		public void _open(Context context)
		{
			context.blackboard.SetBool(context.tree.guid, guid, "isOpen", true);
			OnOpen(context);
		}

		public void _close(Context context)
		{
			context.blackboard.SetBool(context.tree.guid, guid, "isOpen", false);
			OnClose(context);
		}

		public RunningStatus _tick(Context context)
		{
			if (!context.blackboard.GetBool(context.tree.guid, guid, "isOpen"))
			{
				_open(context);
			}

			context._openNodes[context.tree.guid].Push(this);
			context._travelNodes[context.tree.guid].Push(this);
			_enter(context);

			RunningStatus ret = OnTick(context);

			_exit(context);

			if (ret != RunningStatus.Running)
			{
				context._openNodes[context.tree.guid].Pop();
				_close(context);
			}

			lastRet = ret;
			return ret;
		}

		public void AddChildren(params BehaviourNode[] nodes)
		{
			for (int i = 0; i < nodes.Length; ++i)
			{
				AddChild(nodes[i]);
			}
		}

		public void AddChild(BehaviourNode node)
		{
			if (!m_children.Contains(node))
			{
				node.parent = this;
				m_children.Add(node);
			}
		}

		public void DeleteChild(BehaviourNode node)
		{
			if (m_children.Contains(node))
			{
				node.parent = null;
				m_children.Remove(node);
			}
		}

		public List<BehaviourNode> _getChildren()
		{
			return m_children;
		}
	}

	public abstract class Composite : BehaviourNode
	{
		public Composite(params BehaviourNode[] nodes)
			: base()
		{
			nodeType = NodeType.Composite;
			AddChildren(nodes);
		}

	}

	public class Selector : Composite
	{
		public Selector(params BehaviourNode[] nodes) :
			base(nodes)
		{
			
		}

		protected override RunningStatus OnTick(Context context)
		{
			for (int i = 0; i < m_children.Count; ++i)
			{
				RunningStatus ret = m_children[i]._tick(context);
				if (ret != RunningStatus.Failure)
					return ret;
			}

			return RunningStatus.Running;
		}
	}

	public class Sequence : Composite
	{
		public Sequence(params BehaviourNode[] nodes) :
			base(nodes)
		{
			
		}

		protected override void OnOpen(Context context)
		{
			context.blackboard.SetInt(context.tree.guid, this.guid, "childIndex", 0);
		}

		protected override RunningStatus OnTick(Context context)
		{
			RunningStatus ret = RunningStatus.Running;

			int currentNodeIndex = context.blackboard.GetInt(context.tree.guid, this.guid, "childIndex");

			for (int i = currentNodeIndex; i < m_children.Count; ++i)
			{
				RunningStatus retChild = m_children[i]._tick(context);
				if (retChild != RunningStatus.Success || currentNodeIndex == m_children.Count - 1)
				{
					ret = retChild;
					break;
				}
				currentNodeIndex++;
			}

			context.blackboard.SetInt(context.tree.guid, this.guid, "childIndex", currentNodeIndex);

			return ret;
		}
	}

	public class Parallel : Composite
	{
		public Parallel(params BehaviourNode[] nodes) :
			base(nodes)
		{
			
		}

		protected override RunningStatus OnTick(Context context)
		{
			for (int i = 0; i < m_children.Count; ++i)
			{
				RunningStatus ret = m_children[i]._tick(context);
				if (ret != RunningStatus.Running)
					return ret;
			}

			return RunningStatus.Running;
		}
	}

	public class RandomSelector : Composite
	{
		public RandomSelector(params BehaviourNode[] nodes) :
			base(nodes)
		{
			
		}

		protected override void OnOpen(Context context)
		{
			int randomIndex = RandomGen.RandInt(0, m_children.Count - 1);
			context.blackboard.SetInt(context.tree.guid, this.guid, "randomIndex", randomIndex);
		}

		protected override RunningStatus OnTick(Context context)
		{
			int randomIndex = context.blackboard.GetInt(context.tree.guid, this.guid, "randomIndex");
			return m_children[randomIndex]._tick(context);
		}

	}

	public abstract class Decorator : BehaviourNode
	{
		public Decorator(BehaviourNode node)
			: base()
		{
			AddChild(node);
			nodeType = NodeType.Decorator;
		}

	}

	public class Inverter : Decorator
	{
		public Inverter(BehaviourNode node) : 
			base(node) 
		{
			
		}

		protected override RunningStatus OnTick(Context context)
		{
			RunningStatus ret = m_children[0]._tick(context);
			if (ret == RunningStatus.Success)
				return RunningStatus.Failure;
			else if (ret == RunningStatus.Failure)
				return RunningStatus.Success;

			return RunningStatus.Running;
		}

	}

	/// <summary>
	/// Repeater
	/// </summary>
	public class Repeater : Decorator
	{
		private int m_targetCount = 0;

		/// <summary>
		/// Repeater
		/// </summary>
		/// <param name="count">-1:forever repeating; positive: repeating count</param>
		/// <param name="node"></param>
		public Repeater(int count, BehaviourNode node)
			: base(node)
		{
			m_targetCount = count;
		}

		protected override void OnOpen(Context context)
		{
			context.blackboard.SetInt(context.tree.guid, this.guid, "count", 0);
		}

		protected override RunningStatus OnTick(Context context)
		{
			int m_count = context.blackboard.GetInt(context.tree.guid, this.guid, "count");
			if (m_targetCount >= 0 && m_count >= m_targetCount)
				return RunningStatus.Success;

			RunningStatus ret = m_children[0]._tick(context);
			if (m_targetCount >= 0 && ret == RunningStatus.Success)
			{
				m_count++;
				context.blackboard.SetInt(context.tree.guid, this.guid, "count", m_count);
				if (m_count >= m_targetCount)
					return RunningStatus.Success;
			}

			return RunningStatus.Running;
		}

	}

	public class Delay : Decorator
	{
		private uint m_millseconds = 0;

		public Delay(float seconds, BehaviourNode node)
			: base(node)
		{
			if (seconds < 0)
				seconds = 0;
			m_millseconds = (uint)(1000 * seconds);
		}

		protected override void OnOpen(Context context)
		{
			long beginTime = System.DateTime.Now.Ticks / 10000;
			context.blackboard.SetLong(context.tree.guid, this.guid, "beginTime", beginTime);
		}

		protected override RunningStatus OnTick(Context context)
		{
			long beginTime = context.blackboard.GetLong(context.tree.guid, this.guid, "beginTime");
			if (System.DateTime.Now.Ticks / 10000 > beginTime + m_millseconds)
			{
				return m_children[0]._tick(context);
			}

			return RunningStatus.Running;
		}

	}

	public abstract class Action : BehaviourNode
	{
		public Action()
			: base()
		{
			nodeType = NodeType.Action;
		}

	}

	public class Wait : Action
	{
		private uint m_millseconds = 0;

		public Wait(float seconds)
			: base()
		{
			if (seconds < 0)
				seconds = 0;
			m_millseconds = (uint)(1000 * seconds);
		}

		protected override void OnOpen(Context context)
		{
			long beginTime = System.DateTime.Now.Ticks / 10000;
			context.blackboard.SetLong(context.tree.guid, this.guid, "beginTime", beginTime);
		}

		protected override RunningStatus OnTick(Context context)
		{
			long beginTime = context.blackboard.GetLong(context.tree.guid, this.guid, "beginTime");
			if (System.DateTime.Now.Ticks / 10000 > beginTime + m_millseconds)
			{
				return RunningStatus.Success;
			}

			return RunningStatus.Running;
		}

	}

}