
using System;
using System.Collections;
using System.Collections.Generic;

using Lite.AStar;


namespace Lite.Goap
{
	// worldstate is node, action is edge

	public abstract class GoapAStarMap : AStarMap
	{
		private List<GoapAction> actionTable = new List<GoapAction>();

		private List<GoapAction> neighbourList = new List<GoapAction>();

		public GoapAStarNode CreateGoapNode(WorldState status)
		{
			GoapAStarNode node = new GoapAStarNode();
			node.nodeStatus.Copy(status);
			return node;
		}

		public void AddAction(GoapAction action)
		{
			actionTable.Add(action);
		}

		public abstract void BuildActionTable(IAgent agent);

		public override int GetNeighbourNodeCount(AStarNode node)
		{
			neighbourList.Clear();
			GoapAStarNode goapNode = node as GoapAStarNode;
			for (int i = 0; i < actionTable.Count; ++i)
			{
				GoapAction action = actionTable[i];
				if (goapNode.nodeStatus.Contains(action.preconditons))
					neighbourList.Add(action);
			}
			return neighbourList.Count;
		}

		public override AStarNode GetNeighbourNode(AStarNode node, int index)
		{
			GoapAStarNode goapNode = node as GoapAStarNode;
			WorldState status = new WorldState(goapNode.nodeStatus.MaxStateCount);
			status.Copy(goapNode.nodeStatus);
			status.Merge(neighbourList[index].preconditons);
			return CreateGoapNode(status);
		}

	}
}

