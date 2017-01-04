
namespace Lite.Strategy
{
	public enum StateType
	{
		HasTool,
		HasFirewood,
		HasOre,
		HasLogs,
		HasNewTools,
		CollectTools,
		CollectOre,
		CollectLogs,
		CollectFirewood,

		Count
	}

	public enum ActionType
	{
		Default,
		ChopFirewood,
		chopLog,
		DropOffFirewood,
		DropOffLogs,
		DropOffOre,
		DropOffTools,
		MineOre,
		PickupLogs,
		PickupOre,
		PickupTools,
	}

	public class GoapDefines
	{
		public static readonly int STATE_COUNT = (int)StateType.Count;
	}

}