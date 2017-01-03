
using System;
using System.Collections;
using System.Collections.Generic;

using Lite.AStar;


namespace Lite.Goap
{
	// worldstate is node, action is edge

	public class GoapAStarMap : AStarMap
	{
		private List<GoapAction> actionTable = new List<GoapAction>();

		private List<GoapAction> neighbourList = new List<GoapAction>();

		public GoapAStarNode CreateGoapNode(ContextStatus status)
		{
			GoapAStarNode node = new GoapAStarNode();
			node.currentStatus.Copy(status);
			return node;
		}

		public void BuildActionTable(Lite.Agent agent)
		{

		}

		public override int GetNeighbourNodeCount(AStarNode node)
		{
			neighbourList.Clear();
			GoapAStarNode goapNode = node as GoapAStarNode;
			for (int i = 0; i < actionTable.Count; ++i)
			{
				GoapAction action = actionTable[i];
				if (goapNode.currentStatus.Contains(action.preconditon))
					neighbourList.Add(action);
			}
			return neighbourList.Count;
		}

		public override AStarNode GetNeighbourNode(AStarNode node, int index)
		{
			GoapAStarNode goapNode = node as GoapAStarNode;
			ContextStatus status = new ContextStatus();
			status.Copy(goapNode.currentStatus);
			status.Merge(neighbourList[index].preconditon);
			return CreateGoapNode(status);
		}

	}
}

