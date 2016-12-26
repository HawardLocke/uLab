
using System.Collections;
using System.Collections.Generic;


namespace Lite.BevTree
{
	using NodeStack = Stack<BehaviourNode>;


	public enum RunningState
	{
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

		public RunningState Tick(Context context)
		{
			context.EnsureTreeEnvSetup(this);

			context._enterNodes[guid].Clear();

			RunningState ret = RunningState.Running;
			if (root != null)
				ret = root._tick(context);

			UpdateOpenNodes(context);

			return ret;
		}

		private void UpdateOpenNodes(Context context)
		{
			NodeStack enterNodes = context._enterNodes[guid];
			NodeStack tmpEnterNodes = context._tempEnterNodes[guid];
			NodeStack openNodes = context._openNodes[guid];
			/*while(enterNodes.Count > 0 && openNodes.Count > 0)
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
				openNodes.Peek()._close(context);
			while (enterNodes.Count > 0)
				tmpEnterNodes.Push(enterNodes.Pop());
			while (tmpEnterNodes.Count > 0)
				openNodes.Push(tmpEnterNodes.Pop());
			 */
			while (enterNodes.Count > 0 && openNodes.Count > 0)
			{
				if (enterNodes.Count > openNodes.Count)
				{
					tmpEnterNodes.Push(enterNodes.Pop());
				}
				else if (enterNodes.Count < openNodes.Count)
				{
					openNodes.Pop()._close(context);
				}
				else if (enterNodes.Peek().guid != openNodes.Peek().guid)
				{
					tmpEnterNodes.Push(enterNodes.Pop());
					openNodes.Pop()._close(context);
				}
				else
					break;
			}

			while (tmpEnterNodes.Count > 0)
				openNodes.Push(tmpEnterNodes.Pop());

		}

		public string Dump()
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();

			NodeStack nodeStack = new NodeStack();
			nodeStack.Push(root);
			while (nodeStack.Count > 0)
			{
				BehaviourNode node = nodeStack.Pop();
				builder.Append(string.Format("{0}", node.nodeType.ToString()));
				foreach (BehaviourNode child in node._getChildren())
				{
					nodeStack.Push(child);
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

		public object data;

		private HashSet<long> treeSet;

		public Dictionary<long, NodeStack> _enterNodes;

		public Dictionary<long, NodeStack> _tempEnterNodes;

		public Dictionary<long, NodeStack> _openNodes;

		public Context()
		{
			this._blackboard = new Blackboard();
			this.treeSet = new HashSet<long>();
			this._enterNodes = new Dictionary<long, NodeStack>();
			this._tempEnterNodes = new Dictionary<long, NodeStack>();
			this._openNodes = new Dictionary<long, NodeStack>();
		}

		public void EnsureTreeEnvSetup(BehaviourTree tree)
		{
			this._tree = tree;
			if (!treeSet.Contains(tree.guid))
			{
				treeSet.Add(tree.guid);
				_enterNodes.Add(tree.guid, new NodeStack());
				_tempEnterNodes.Add(tree.guid, new NodeStack());
				_openNodes.Add(tree.guid, new NodeStack());
			}
		}

	}


}