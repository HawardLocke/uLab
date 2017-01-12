
using System;
using System.Collections;
using System.Collections.Generic;

using Lite.AStar;


namespace Lite.Goap
{
	// worldstate is node, action is edge
	// ��Ϊworldstate��Ͻ����Ŀ�Ƚϴ�����map������ͼ�ṹ������������ʱ������ʱ�Ľڵ㡣

	public abstract class GoapAStarMap : AStarMap
	{
		private List<GoapAction> actionTable = new List<GoapAction>();
		private List<GoapAction> neighbourEdgeList = new List<GoapAction>();
		private Queue<GoapAStarNode> nodePool = new Queue<GoapAStarNode>();


		public void AddAction(GoapAction action)
		{
			actionTable.Add(action);
		}

		public abstract void BuildActionTable(IAgent agent);

		public override int GetNeighbourNodeCount(AStarNode node)
		{
			neighbourEdgeList.Clear();
			GoapAStarNode goapNode = node as GoapAStarNode;
			for (int i = 0; i < actionTable.Count; ++i)
			{
				GoapAction action = actionTable[i];
				if (goapNode.state.Contains(action.preconditons))
					neighbourEdgeList.Add(action);
			}
			return neighbourEdgeList.Count;
		}

		public override AStarNode GetNeighbourNode(AStarNode node, int index)
		{
			GoapAStarNode goapNode = node as GoapAStarNode;
			GoapAction action = neighbourEdgeList[index];
			GoapAStarNode neighbour = CreateGoapNode(goapNode.state, action);
			neighbour.state.Merge(action.preconditons);
			return neighbour;
		}

		public override void RecycleNode(AStarNode node)
		{
			node.Reset();
			nodePool.Enqueue((GoapAStarNode)node);
		}

		public GoapAStarNode CreateGoapNode(WorldState copyState, GoapAction fromAction)
		{
			GoapAStarNode node = GetNodeFromPool(copyState.MaxStateCount);
			node.state.Copy(copyState);
			node.fromAction = fromAction;
			return node;
		}

		private GoapAStarNode GetNodeFromPool(int stateCount)
		{
			GoapAStarNode node;
			if (nodePool.Count > 0)
			{
				node = nodePool.Dequeue();
				node.Reset();
				Log.Info("Get Node From Pool...");
			}
			else
			{
				node = new GoapAStarNode(stateCount);
			}
			return node;
		}

	}
}

