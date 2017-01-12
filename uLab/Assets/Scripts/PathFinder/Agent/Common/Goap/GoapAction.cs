

namespace Lite.Goap
{
	public abstract class GoapAction : Lite.Graph.GraphEdge
	{
		protected IAgent owner;

		public uint actionType;
		public WorldState preconditons;
		public WorldState effects;
		public int cost;

		public GoapAction(IAgent owner, int maxStateCount):
			base(0,0,0)
		{
			this.owner = owner;
			actionType = 0;
			preconditons = new WorldState(maxStateCount);
			effects = new WorldState(maxStateCount);
			cost = 0;
			OnSetupPreconditons();
			OnSetupEffects();
		}

		protected virtual void OnSetupPreconditons() { }

		protected virtual void OnSetupEffects() { }

	}

}