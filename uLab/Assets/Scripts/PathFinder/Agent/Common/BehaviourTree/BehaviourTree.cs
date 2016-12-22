
using System.Collections;
using System.Collections.Generic;


namespace Lite.BevTree
{
	public class Context
	{
		public BehaviourTree tree;
		public object data;

		public Context(BehaviourTree tree)
		{
			this.tree = tree;
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

	class GuidGen
	{
		public static long NextLong()
		{
			byte[] buffer = System.Guid.NewGuid().ToByteArray();
			return System.BitConverter.ToInt64(buffer, 0);
		}
	}

	public class Blackborad
	{
		private BehaviourTree m_tree;
		private Dictionary<string, System.Object> m_dataDic;
		private Dictionary<long, Dictionary<string, System.Object>> m_nodeDataDic;

		public Blackborad(BehaviourTree tree)
		{
			m_tree = tree;
			m_dataDic = new Dictionary<string, object>();
			m_nodeDataDic = new Dictionary<long, Dictionary<string, object>>();
		}

		public T Get<T>(string key)
		{
			return (T)m_dataDic[key];
		}

		public void Set(string key, System.Object value)
		{
			if (m_dataDic.ContainsKey(key))
				m_dataDic[key] = value;
			else
				m_dataDic.Add(key, value);
		}

		public T Get<T>(long nodeGuid, string key)
		{
			return (T)m_nodeDataDic[nodeGuid][key];
		}

		public void Set(long nodeGuid, string key, System.Object value)
		{
			Dictionary<string, object> dic;
			if (m_nodeDataDic.ContainsKey(nodeGuid))
			{
				dic = m_nodeDataDic[nodeGuid];
			}
			else
			{
				dic = new Dictionary<string, object>();
				m_nodeDataDic.Add(nodeGuid, dic);
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
		public Context context;
		public Blackborad balckboard;

		private Dictionary<long, BehaviourNode> m_lastOpenNodes;
		private Dictionary<long, BehaviourNode> m_openNodes;
		//private Dictionary<long, BehaviourNode> m_openNodes;
		
		public BehaviourTree()
		{
			guid = GuidGen.NextLong();
			context = new Context(this);
			balckboard = new Blackborad(this);
			m_lastOpenNodes = new Dictionary<long, BehaviourNode>();
			m_openNodes = new Dictionary<long, BehaviourNode>();
		}

		public RunningState Tick()
		{
			RunningState ret = RunningState.Running;

			if (root != null)
			{
				m_openNodes.Clear();
				ret = root._Tick(context);
			}

			IDictionaryEnumerator itor = m_lastOpenNodes.GetEnumerator();

			while (itor.MoveNext())
			{
				long guid = (long)itor.Entry.Key;
				if (!m_openNodes.ContainsKey(guid))
				{
					m_lastOpenNodes[guid]._Close(context);
				}
			}

			UnityEngine.Debug.Log(string.Format("{0}, {1}", m_lastOpenNodes.Count, m_openNodes.Count));

			m_lastOpenNodes.Clear();

			itor = m_openNodes.GetEnumerator();

			while (itor.MoveNext())
			{
				m_lastOpenNodes.Add((long)itor.Entry.Key, (BehaviourNode)itor.Entry.Value);
			}

			return ret;
		}

		public bool _IsNodeOpen(BehaviourNode node)
		{
			return m_lastOpenNodes.ContainsKey(node.guid);
		}

		public void _AddOpenNode(BehaviourNode node)
		{
			m_openNodes.Add(node.guid, node);
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

		public void _Close(Context context)
		{
			UnityEngine.Debug.Log("_close");
			OnClose(context);
		}
		
		public RunningState _Tick(Context context) 
		{
			bool newOpen = false;
			if (!context.tree._IsNodeOpen(this))
			{
				newOpen = true;
				OnOpen(context);
			}

			OnEnter(context);

			RunningState ret = OnTick(context);

			OnExit(context);

			if (ret != RunningState.Running)
			{
				newOpen = false;
			}

			if (newOpen)
				context.tree._AddOpenNode(this);

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
				RunningState ret = m_children[i]._Tick(context);
				if (ret != RunningState.Failure)
					return ret;
			}

			return RunningState.Running;
		}
	}

	public class Sequence : Composite
	{
		private int currentNodeIndex = 0;

		public Sequence(params BehaviourNode[] nodes)
		{
			AddChildren(nodes);
		}

		protected override void OnOpen(Context context)
		{
			currentNodeIndex = 0;
		}

		protected override RunningState OnTick(Context context)
		{
			for (int i = currentNodeIndex; i < m_children.Count; ++i)
			{
				RunningState ret = m_children[i]._Tick(context);
				if (ret != RunningState.Success || currentNodeIndex == m_children.Count - 1)
					return ret;
				currentNodeIndex++;
			}

			return RunningState.Running;
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
				RunningState ret = m_children[i]._Tick(context);
				if (ret != RunningState.Running)
					return ret;
			}

			return RunningState.Running;
		}
	}

	public class RandomSelector : Composite
	{
		private int randomIndex = 0;

		public RandomSelector(params BehaviourNode[] nodes)
		{
			AddChildren(nodes);
		}

		protected override void OnOpen(Context context)
		{
			System.Random rnd = new System.Random();
			randomIndex = rnd.Next(0, m_children.Count - 1);
		}

		protected override RunningState OnTick(Context context)
		{
			return m_children[randomIndex]._Tick(context);
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
			RunningState ret = m_child._Tick(context);
			if (ret == RunningState.Success)
				return RunningState.Failure;
			else if (ret == RunningState.Failure)
				return RunningState.Success;

			return RunningState.Running;
		}

	}

	public class Repeater : Decorator
	{
		private uint m_targetCount = 0;

		private uint m_count = 0;

		public Repeater(BehaviourNode node, uint count) : base(node)
		{
			m_targetCount = count;
		}

		protected override void OnOpen(Context context)
		{
			m_count = 0;
			UnityEngine.Debug.Log("repeat open");
		}

		protected override RunningState OnTick(Context context)
		{
			RunningState ret = m_child._Tick(context);
			if (ret == RunningState.Success)
			{
				m_count++;
				if (m_count >= m_targetCount)
					return RunningState.Success;
			}
			
			return ret;
		}

	}

	public class Wait : Decorator
	{
		private uint m_millseconds = 0;

		private long m_beginTime = 0;

		public Wait(BehaviourNode node, float seconds) : base(node)
		{
			if (seconds < 0)
				seconds = 0;
			m_millseconds = (uint)(1000 * seconds);
		}

		protected override void OnOpen(Context context)
		{
			UnityEngine.Debug.Log("wait open");
			m_beginTime = System.DateTime.Now.Ticks / 10000;
		}

		protected override RunningState OnTick(Context context)
		{
			if (System.DateTime.Now.Ticks / 10000 > m_beginTime + m_millseconds)
			{
				return m_child._Tick(context);
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


}