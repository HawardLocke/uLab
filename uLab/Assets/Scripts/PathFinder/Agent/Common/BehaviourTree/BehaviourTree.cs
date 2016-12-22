
using System.Collections.Generic;


namespace Lite.BehaviourTree
{
	public class Context
	{
		public Tree tree;
		public object data;

		public Context(Tree tree)
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

	public class Tree
	{
		public long guid;
		public string title;
		public string description;
		public Node root;
		public Context context;
		public Blackborad balckboard;

		private Dictionary<long, Node> m_openNodes;
		
		public Tree()
		{
			guid = GuidGen.NextLong();
			context = new Context(this);
			balckboard = new Blackborad(this);
			m_openNodes = new Dictionary<long, Node>();
		}

		public void Tick()
		{
			RunningState st = RunningState.Running;
			if (root != null)
			{
				st = root.Tick(context);
			}
		}

		public bool IsInOpenNodes(Node node)
		{
			return m_openNodes.ContainsKey(node.guid);
		}

		public void AddToOpenNodes(Node node)
		{
			m_openNodes.Add(node.guid, node);
		}

		public void RemoveFromOpenNodes(Node node)
		{
			m_openNodes.Remove(node.guid);
		}

		public void Dump()
		{

		}

	}

	public class Blackborad
	{
		private Tree m_tree;
		private Dictionary<string, System.Object> m_dataDic;
		private Dictionary<long, Dictionary<string, System.Object>> m_nodeDataDic;

		public Blackborad(Tree tree)
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


	public abstract class Node
	{
		public long guid;
		public NodeType nodeType;

		public Node()
		{
			guid = GuidGen.NextLong();
			nodeType = NodeType.Default;
		}

		protected virtual void OnOpen(Context context) { }
		
		public virtual void OnClose(Context context) { }

		public virtual void OnEnter(Context context) { }

		public virtual void OnExit(Context context) { }

		public abstract RunningState OnTick(Context context);
		
		public virtual RunningState Tick(Context context) 
		{
			if (!context.tree.IsInOpenNodes(this))
			{
				context.tree.AddToOpenNodes(this);
				OnOpen(context);
			}

			OnEnter(context);

			RunningState ret = OnTick(context);

			OnExit(context);

			if (ret != RunningState.Running)
			{
				context.tree.RemoveFromOpenNodes(this);
				OnClose(context);
			}

			return ret;
		}

	}

	public abstract class Composite : Node
	{
		protected List<Node> m_children;
		
		public Composite() : base()
		{
			m_children = new List<Node>();
			nodeType = NodeType.Composite;
		}

		public void AddChildren(params Node[] nodes)
		{
			for (int i = 0; i < nodes.Length; ++i)
			{
				AddChild(nodes[i]);
			}
		}

		public void AddChild(Node node)
		{
			if (!m_children.Contains(node))
				m_children.Add(node);
		}

	}

	public class Selector : Composite
	{
		public Selector(params Node[] nodes)
		{
			AddChildren(nodes);
		}

		protected override RunningState OnTick(Context context)
		{
			for (int i = 0; i < m_children.Count; ++i)
			{
				RunningState ret = m_children[i].Tick(context);
				if (ret != RunningState.Failure)
					return ret;
			}

			return RunningState.Running;
		}
	}

	public class Sequence : Composite
	{
		private int currentNodeIndex = 0;

		public Sequence(params Node[] nodes)
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
				RunningState ret = m_children[i].Tick(context);
				if (ret != RunningState.Success || currentNodeIndex == m_children.Count - 1)
					return ret;
				currentNodeIndex++;
			}

			return RunningState.Running;
		}
	}

	public class Parallel : Composite
	{
		public Parallel(params Node[] nodes)
		{
			AddChildren(nodes);
		}

		protected override RunningState OnTick(Context context)
		{
			for (int i = 0; i < m_children.Count; ++i)
			{
				RunningState ret = m_children[i].Tick(context);
				if (ret != RunningState.Running)
					return ret;
			}

			return RunningState.Running;
		}
	}

	public class RandomSelector : Composite
	{
		private int randomIndex = 0;

		public RandomSelector(params Node[] nodes)
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
			return m_children[randomIndex].Tick(context);
		}

	}

	public abstract class Decorator : Node
	{
		protected Node m_child;

		public Decorator(Node node) : base()
		{
			m_child = node;
			nodeType = NodeType.Decorator;
		}

	}

	public class Inverter : Decorator
	{
		public Inverter(Node node) : base(node) { }

		public override RunningState OnTick(Context context)
		{
			RunningState ret = m_child.Tick(context);
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

		public Repeater(Node node, uint count) : base(node)
		{
			m_targetCount = count;
		}

		protected override void OnOpen(Context context)
		{
			m_count = 0;
		}

		public override RunningState OnTick(Context context)
		{
			RunningState ret = m_child.Tick(context);
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

		public Wait(Node node, float seconds) : base(node)
		{
			if (seconds < 0)
				seconds = 0;
			m_millseconds = (uint)(1000 * seconds);
		}

		protected override void OnOpen(Context context)
		{
			m_beginTime = System.DateTime.Now.Ticks / 10000;
		}

		public override RunningState OnTick(Context context)
		{
			if (System.DateTime.Now.Ticks / 10000 > m_beginTime + m_millseconds)
			{
				return m_child.Tick(context);
			}

			return RunningState.Running;
		}

	}

	public abstract class Action : Node
	{
		public Action() : base()
		{
			nodeType = NodeType.Action;
		}

	}


}