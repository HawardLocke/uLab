
using Lite.AStar;


namespace Lite.Goap
{

	public class GoapAStarPlanner : AStarPathPlanner
	{
		WorldState targetState;

		public GoapAction[] Plan(WorldState from, WorldState to)
		{
			targetState = to;

			GoapAStarMap goapMap = map as GoapAStarMap;
			AStarNode startNode = goapMap.CreateGoapNode(from, null);
			AStarNode endNode = DoAStar(startNode);

			// build action list.
			endNode = ReverseNodeList(endNode) as GoapAStarNode;
			endNode = endNode.prev;
			int nodeCount = 0;
			AStarNode pathNode = endNode;
			while (pathNode != null)
			{
				nodeCount++;
				pathNode = pathNode.prev as GoapAStarNode;
			}
			GoapAction[] actionArray = new GoapAction[nodeCount];
			pathNode = endNode;
			int index = 0;
			while (pathNode != null)
			{
				actionArray[index++] = ((GoapAStarNode)pathNode).fromAction;
				pathNode = pathNode.prev;
			}
			
			return actionArray;
		}

		private AStarNode ReverseNodeList(AStarNode head)
		{
			AStarNode node = head;
			AStarNode prevNode = null;
			while (node != null)
			{
				AStarNode tmpNode = node;
				node = node.prev;
				tmpNode.prev = prevNode;
				prevNode = tmpNode;
			}
			return prevNode;
		}

		protected override bool CheckArrived(AStarNode node)
		{
			GoapAStarNode goapNode = node as GoapAStarNode;
			return goapNode.state.Contains(targetState);
		}

		protected override int CalCostG(AStarNode prevNode, AStarNode currentNode)
		{
			return prevNode.g + ((GoapAStarNode)currentNode).fromAction.cost;
		}

		protected override int CalCostH(AStarNode node)
		{
			return targetState.CountDifference(((GoapAStarNode)node).state);
		}

	}

}