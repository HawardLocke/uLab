
using Lite.AStar;


namespace Lite.Goap
{

	public class GoapAStarPlanner : AStarPathPlanner
	{
		GoapGoal currentGoal;

		public GoapAction[] Plan(GoapGoal goal)
		{
			currentGoal = goal;

			GoapAStarMap goapMap = map as GoapAStarMap;
			GoapAStarNode startNode = goapMap.CreateGoapNode(goal.state) as GoapAStarNode;

			GoapAStarNode endNode = DoAStar(startNode) as GoapAStarNode;

			// build action list.
			int nodeCount = 0;
			GoapAStarNode pathNode = endNode;
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
				actionArray[index++] = null;
				pathNode = pathNode.prev as GoapAStarNode;
			}
			Cleanup();
			return actionArray;
		}

		protected override bool CheckArrived(AStarNode node)
		{
			GoapAStarNode goapNode = node as GoapAStarNode;
			return goapNode.nodeStatus.Contains(currentGoal.state);
		}

		protected override int CalCostG(AStarNode prevNode, AStarNode currentNode)
		{
			return prevNode.g + map.GetEdge(prevNode.id, currentNode.id).cost;
		}

		protected override int CalCostH(AStarNode node)
		{
			int dist = 0;
			return dist;
		}

	}

}