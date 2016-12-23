
using System.Collections;
using System.Collections.Generic;


namespace Lite.BevTree
{
	using ValueDic = Dictionary<string, System.Object>;
	using ScopedValueDic = Dictionary<long, Dictionary<string, System.Object>>;
	using NodeStack = Stack<BehaviourNode>;

	class GuidGen
	{
		public static long NextLong()
		{
			byte[] buffer = System.Guid.NewGuid().ToByteArray();
			return System.BitConverter.ToInt64(buffer, 0);
		}
	}

	class RandomGen
	{
		private static System.Random rnd = new System.Random(System.DateTime.Now.Millisecond);

		public static int RandInt(int min, int max)
		{
			return rnd.Next(min, max);
		}

		public static float RandFloat()
		{
			return (rnd.Next(0, int.MaxValue)) / (int.MaxValue + 1.0f);
		}

		public static float RandClamp()
		{
			return RandFloat() - RandFloat();
		}
	}


	public enum RunningState
	{
		Running,
		Success,
		Failure
	}

	public enum NodeType
	{
		Default,
		Composite,
		Decorator,
		Action
	}

	public class Context
	{
		private BehaviourTree _tree;
		public BehaviourTree tree { get { return _tree; } }

		private Blackboard _blackboard;
		public Blackboard blackboard { get { return _blackboard; } }

		public object data;

		private HashSet<long> treeSet;

		public Context()
		{
			this._blackboard = new Blackboard();
			this.treeSet = new HashSet<long>();
		}

		public void EnsureTreeEnvSetup(BehaviourTree tree)
		{
			this._tree = tree;
			if(!treeSet.Contains(tree.guid))
			{
				treeSet.Add(tree.guid);
				_blackboard.Set(tree.guid, "enterNodes", new NodeStack());
				_blackboard.Set(tree.guid, "tempEnterNodes", new NodeStack());
				_blackboard.Set(tree.guid, "openNodes", new NodeStack());
			}
		}

	}

	public class Blackboard
	{
		private ScopedValueDic m_treeDic;
		private Dictionary<long, ScopedValueDic> m_nodeDic;

		public Blackboard()
		{
			m_treeDic = new ScopedValueDic();
			m_nodeDic = new Dictionary<long, ScopedValueDic>();
		}

		public T Get<T>(long treeId, string key)
		{
			return (T)m_treeDic[treeId][key];
		}

		public void Set(long treeId, string key, System.Object value)
		{
			_setScopeValue(ref m_treeDic, treeId, key, value);
		}

		public T Get<T>(long treeId, long nodeId, string key)
		{
			return (T)m_nodeDic[treeId][nodeId][key];
		}

		public void Set(long treeId, long nodeId, string key, System.Object value)
		{
			ScopedValueDic sdic;
			if (m_nodeDic.ContainsKey(treeId))
			{
				sdic = m_nodeDic[treeId];
			}
			else
			{
				sdic = new ScopedValueDic();
				m_nodeDic.Add(treeId, sdic);
			}
			_setScopeValue(ref sdic, nodeId, key, value);
		}

		private void _setScopeValue(ref ScopedValueDic sdic, long scope, string key, System.Object value)
		{
			ValueDic dic;
			if (sdic.ContainsKey(scope))
			{
				dic = sdic[scope];
			}
			else
			{
				dic = new ValueDic();
				sdic.Add(scope, dic);
			}
			if (dic.ContainsKey(key))
				dic[key] = value;
			else
				dic.Add(key, value);
		}

	}

	public class BehaviourTree
	{
		public long guid;
		public string title;
		public string description;
		public BehaviourNode root;

		public BehaviourTree()
		{
			guid = GuidGen.NextLong();
		}

		public RunningState Tick(Context context)
		{
			context.EnsureTreeEnvSetup(this);

			var enterNodes = context.blackboard.Get<NodeStack>(guid, "enterNodes");
			enterNodes.Clear();

			RunningState ret = RunningState.Running;
			if (root != null)
				ret = root._tick(context);

			var tmpEnterNodes = context.blackboard.Get<NodeStack>(guid, "tempEnterNodes");
			var openNodes = context.blackboard.Get<NodeStack>(guid, "openNodes");
			UnityEngine.Debug.Log(string.Format("{0}, {1}", openNodes.Count, enterNodes.Count));
			while(enterNodes.Count > 0 && openNodes.Count > 0)
			{
				if (enterNodes.Peek().guid == openNodes.Peek().guid)
				{
					tmpEnterNodes.Push(enterNodes.Pop());
					openNodes.Pop();
				}
				else
					break;
			}
			while (openNodes.Count > 0)
				openNodes.Pop()._close(context);
			while (enterNodes.Count > 0)
				tmpEnterNodes.Push(enterNodes.Pop());
			while (tmpEnterNodes.Count > 0)
				openNodes.Push(tmpEnterNodes.Pop());

			return ret;
		}

		public void Dump()
		{

		}

	}


	public abstract class BehaviourNode
	{
		public long guid;
		public NodeType nodeType;

		public BehaviourNode()
		{
			guid = GuidGen.NextLong();
			nodeType = NodeType.Default;
		}

		protected virtual void OnOpen(Context context) { }

		protected virtual void OnClose(Context context) { }

		protected virtual void OnEnter(Context context) { }

		protected virtual void OnExit(Context context) { }

		protected abstract RunningState OnTick(Context context);

		public void _enter(Context context)
		{
			context.blackboard.Get<NodeStack>(guid, "enterNodes").Push(this);
			OnEnter(context);
		}

		public void _exit(Context context)
		{
			OnExit(context);
		}

		public void _open(Context context)
		{
			context.blackboard.Set(context.tree.guid, guid, "isOpen", true);
			OnOpen(context);
		}

		public void _close(Context context)
		{
			context.blackboard.Set(context.tree.guid, guid, "isOpen", false);
			context.blackboard.Get<NodeStack>(guid, "enterNodes").Pop();
			OnClose(context);
		}
		
		public RunningState _tick(Context context) 
		{
			if (!context.blackboard.Get<bool>(context.tree.guid, guid, "isOpen"))
			{
				_open(context);
			}

			_enter(context);

			RunningState ret = OnTick(context);

			_exit(context);

			if (ret != RunningState.Running)
			{
				_close(context);
			}

			return ret;
		}

	}

	public abstract class Composite : BehaviourNode
	{
		protected List<BehaviourNode> m_children;
		
		public Composite() : base()
		{
			m_children = new List<BehaviourNode>();
			nodeType = NodeType.Composite;
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
				m_children.Add(node);
		}

	}

	public class Selector : Composite
	{
		public Selector(params BehaviourNode[] nodes)
		{
			AddChildren(nodes);
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
		public Sequence(params BehaviourNode[] nodes)
		{
			AddChildren(nodes);
		}

		protected override void OnOpen(Context context)
		{
			context.blackboard.Set(context.tree.guid, this.guid, "childIndex", 0);
		}

		protected override RunningState OnTick(Context context)
		{
			RunningState ret = RunningState.Running;

			int currentNodeIndex = context.blackboard.Get<int>(context.tree.guid, this.guid, "childIndex");

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

			context.blackboard.Set(context.tree.guid, this.guid, "childIndex", currentNodeIndex);

			return ret;
		}
	}

	public class Parallel : Composite
	{
		public Parallel(params BehaviourNode[] nodes)
		{
			AddChildren(nodes);
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
		public RandomSelector(params BehaviourNode[] nodes)
		{
			AddChildren(nodes);
		}

		protected override void OnOpen(Context context)
		{
			int randomIndex = RandomGen.RandInt(0, m_children.Count - 1);
			context.blackboard.Set(context.tree.guid, this.guid, "randomIndex", randomIndex);
		}

		protected override RunningState OnTick(Context context)
		{
			int randomIndex = context.blackboard.Get<int>(context.tree.guid, this.guid, "randomIndex");
			return m_children[randomIndex]._tick(context);
		}

	}

	public abstract class Decorator : BehaviourNode
	{
		protected BehaviourNode m_child;

		public Decorator(BehaviourNode node) : base()
		{
			m_child = node;
			nodeType = NodeType.Decorator;
		}

	}

	public class Inverter : Decorator
	{
		public Inverter(BehaviourNode node) : base(node) { }

		protected override RunningState OnTick(Context context)
		{
			RunningState ret = m_child._tick(context);
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

		public Repeater(BehaviourNode node, int count) : base(node)
		{
			m_targetCount = count;
		}

		protected override void OnOpen(Context context)
		{
			context.blackboard.Set(context.tree.guid, this.guid, "count", 0);
			UnityEngine.Debug.Log("repeat open");
		}

		protected override RunningState OnTick(Context context)
		{
			int m_count = context.blackboard.Get<int>(context.tree.guid, this.guid, "count");
			if (m_targetCount >= 0 && m_count >= m_targetCount)
				return RunningState.Success;

			RunningState ret = m_child._tick(context);
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

		public Delay(BehaviourNode node, float seconds) : base(node)
		{
			if (seconds < 0)
				seconds = 0;
			m_millseconds = (uint)(1000 * seconds);
		}

		protected override void OnOpen(Context context)
		{
			UnityEngine.Debug.Log("wait open");
			long beginTime = System.DateTime.Now.Ticks / 10000;
			context.blackboard.Set(context.tree.guid, this.guid, "beginTime", beginTime);
		}

		protected override RunningState OnTick(Context context)
		{
			long beginTime = context.blackboard.Get<long>(context.tree.guid, this.guid, "beginTime");
			if (System.DateTime.Now.Ticks / 10000 > beginTime + m_millseconds)
			{
				return m_child._tick(context);
			}

			return RunningState.Running;
		}

	}

	public abstract class Action : BehaviourNode
	{
		public Action() : base()
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
			context.blackboard.Set(context.tree.guid, this.guid, "beginTime", beginTime);
		}

		protected override RunningState OnTick(Context context)
		{
			long beginTime = context.blackboard.Get<long>(context.tree.guid, this.guid, "beginTime");
			if (System.DateTime.Now.Ticks / 10000 > beginTime + m_millseconds)
			{
				return RunningState.Success;
			}

			return RunningState.Running;
		}

	}


}