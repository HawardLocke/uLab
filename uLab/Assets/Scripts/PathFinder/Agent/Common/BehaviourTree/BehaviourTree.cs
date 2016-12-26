
using System.Collections;
using System.Collections.Generic;


namespace Lite.BevTree
{
	using NodeStack = Stack<BehaviourNode>;


	public enum RunningStatus
	{
		None,
		Running,
		Success,
		Failure
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

		public RunningStatus Tick(Context context)
		{
			context.EnsureTreeEnvSetup(this);

			context._openNodes[guid].Clear();
			context._travelNodes[guid].Clear();

			RunningStatus ret = RunningStatus.Running;
			if (root != null)
				ret = root._tick(context);

			UpdateOpenNodes(context);

			return ret;
		}

		private void UpdateOpenNodes(Context context)
		{
			NodeStack openNodes = context._openNodes[guid];
			NodeStack tmpNodes = context._tempNodes[guid];
			NodeStack oldOpenNodes = context._oldOpenNodes[guid];
			while (openNodes.Count > 0 && oldOpenNodes.Count > 0)
			{
				if (openNodes.Count > oldOpenNodes.Count)
				{
					tmpNodes.Push(openNodes.Pop());
				}
				else if (openNodes.Count < oldOpenNodes.Count)
				{
					oldOpenNodes.Pop()._close(context);
				}
				else if (openNodes.Peek().guid != oldOpenNodes.Peek().guid)
				{
					tmpNodes.Push(openNodes.Pop());
					oldOpenNodes.Pop()._close(context);
				}
				else
					break;
			}
			while (tmpNodes.Count > 0)
				oldOpenNodes.Push(tmpNodes.Pop());
		}

		public string Dump(Context context)
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();

			string[] statusColors = { "grey", "yellow", "green", "red" };

			NodeStack nodeStack = new NodeStack();
			nodeStack.Push(root);
			while (nodeStack.Count > 0)
			{
				BehaviourNode node = nodeStack.Pop();
				int depth = 0;
				BehaviourNode tmpNode = node;
				while (tmpNode.parent != null)
				{
					tmpNode = tmpNode.parent;
					depth++;
				}
				while (depth-- > 0) builder.Append("    ");
				string color = statusColors[(int)node.lastRet];
				if (!context._travelNodes[this.guid].Contains(node))
					color = statusColors[0];
				builder.Append(string.Format("<color={0}>{1}</color>\n", color, node.GetType().Name));
				var childrenList = node._getChildren();
				for (int i = childrenList.Count - 1; i >= 0; --i)
				{
					nodeStack.Push(childrenList[i]);
				}
			}

			return builder.ToString();
		}

	}


	/// <summary>
	/// Runtime context data shared between different behaviour trees
	/// </summary>
	public class Context
	{
		private BehaviourTree _tree;
		public BehaviourTree tree { get { return _tree; } }

		private Blackboard _blackboard;
		public Blackboard blackboard { get { return _blackboard; } }

		// internal members
		private HashSet<long> treeSet;
		public Dictionary<long, NodeStack> _openNodes;
		public Dictionary<long, NodeStack> _tempNodes;
		public Dictionary<long, NodeStack> _oldOpenNodes;
		public Dictionary<long, NodeStack> _travelNodes;

		public Context()
		{
			this._blackboard = new Blackboard();
			this.treeSet = new HashSet<long>();
			this._openNodes = new Dictionary<long, NodeStack>();
			this._tempNodes = new Dictionary<long, NodeStack>();
			this._oldOpenNodes = new Dictionary<long, NodeStack>();
			this._travelNodes = new Dictionary<long, NodeStack>();
		}

		public void EnsureTreeEnvSetup(BehaviourTree tree)
		{
			this._tree = tree;
			if (!treeSet.Contains(tree.guid))
			{
				treeSet.Add(tree.guid);
				_openNodes.Add(tree.guid, new NodeStack());
				_tempNodes.Add(tree.guid, new NodeStack());
				_oldOpenNodes.Add(tree.guid, new NodeStack());
				_travelNodes.Add(tree.guid, new NodeStack());
			}
		}

	}


}