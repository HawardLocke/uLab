
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
		protected List<BehaviourNode> m_children;

		public BehaviourNode()
		{
			guid = GuidGen.NextLong();
			nodeType = NodeType.Default;
			m_children = new List<BehaviourNode>();
		}

		protected virtual void OnOpen(Context context) { }

		protected virtual void OnClose(Context context) { }

		protected virtual void OnEnter(Context context) { }

		protected virtual void OnExit(Context context) { }

		protected abstract RunningState OnTick(Context context);

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

		public RunningState _tick(Context context)
		{
			if (!context.blackboard.GetBool(context.tree.guid, guid, "isOpen"))
			{
				_open(context);
			}

			context._enterNodes[context.tree.guid].Push(this);
			_enter(context);

			RunningState ret = OnTick(context);

			_exit(context);

			if (ret != RunningState.Running)
			{
				context._enterNodes[context.tree.guid].Pop();
				_close(context);
			}

			return ret;
		}

		protected void AddChildren(params BehaviourNode[] nodes)
		{
			for (int i = 0; i < nodes.Length; ++i)
			{
				AddChild(nodes[i]);
			}
		}

		protected void AddChild(BehaviourNode node)
		{
			if (!m_children.Contains(node))
				m_children.Add(node);
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

		protected override RunningState OnTick(Context context)
		{
			for (int i = 0; i < m_children.Count; ++i)
			{
				RunningState ret = m_children[i]._tick(context);
				if (ret != RunningState.Failure)
					return ret;
			}

			return RunningState.Running;
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

		protected override RunningState OnTick(Context context)
		{
			RunningState ret = RunningState.Running;

			int currentNodeIndex = context.blackboard.GetInt(context.tree.guid, this.guid, "childIndex");

			for (int i = currentNodeIndex; i < m_children.Count; ++i)
			{
				RunningState retChild = m_children[i]._tick(context);
				if (retChild != RunningState.Success || currentNodeIndex == m_children.Count - 1)
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

		protected override RunningState OnTick(Context context)
		{
			for (int i = 0; i < m_children.Count; ++i)
			{
				RunningState ret = m_children[i]._tick(context);
				if (ret != RunningState.Running)
					return ret;
			}

			return RunningState.Running;
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

		protected override RunningState OnTick(Context context)
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

		protected override RunningState OnTick(Context context)
		{
			RunningState ret = m_children[0]._tick(context);
			if (ret == RunningState.Success)
				return RunningState.Failure;
			else if (ret == RunningState.Failure)
				return RunningState.Success;

			return RunningState.Running;
		}

	}

	public class Repeater : Decorator
	{
		private int m_targetCount = 0;

		public Repeater(BehaviourNode node, int count)
			: base(node)
		{
			m_targetCount = count;
		}

		protected override void OnOpen(Context context)
		{
			context.blackboard.SetInt(context.tree.guid, this.guid, "count", 0);
			UnityEngine.Debug.Log("repeat open");
		}

		protected override void OnClose(Context context)
		{
			UnityEngine.Debug.Log("repeat close");
		}

		protected override RunningState OnTick(Context context)
		{
			int m_count = context.blackboard.GetInt(context.tree.guid, this.guid, "count");
			if (m_targetCount >= 0 && m_count >= m_targetCount)
				return RunningState.Success;

			RunningState ret = m_children[0]._tick(context);
			if (m_targetCount >= 0 && ret == RunningState.Success)
			{
				m_count++;
				if (m_count >= m_targetCount)
					return RunningState.Success;
			}

			return RunningState.Running;
		}

	}

	public class Delay : Decorator
	{
		private uint m_millseconds = 0;

		public Delay(BehaviourNode node, float seconds)
			: base(node)
		{
			if (seconds < 0)
				seconds = 0;
			m_millseconds = (uint)(1000 * seconds);
		}

		protected override void OnOpen(Context context)
		{
			UnityEngine.Debug.Log("delay open");
			long beginTime = System.DateTime.Now.Ticks / 10000;
			context.blackboard.SetLong(context.tree.guid, this.guid, "beginTime", beginTime);
		}

		protected override void OnClose(Context context)
		{
			UnityEngine.Debug.Log("delay close");
		}

		protected override RunningState OnTick(Context context)
		{
			long beginTime = context.blackboard.GetLong(context.tree.guid, this.guid, "beginTime");
			if (System.DateTime.Now.Ticks / 10000 > beginTime + m_millseconds)
			{
				return m_children[0]._tick(context);
			}

			return RunningState.Running;
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

		protected override RunningState OnTick(Context context)
		{
			long beginTime = context.blackboard.GetLong(context.tree.guid, this.guid, "beginTime");
			if (System.DateTime.Now.Ticks / 10000 > beginTime + m_millseconds)
			{
				return RunningState.Success;
			}

			return RunningState.Running;
		}

	}

}