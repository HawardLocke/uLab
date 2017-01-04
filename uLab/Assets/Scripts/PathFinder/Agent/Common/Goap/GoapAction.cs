

namespace Lite.Goap
{

	public abstract class GoapAction
	{
		protected IAgent owner;

		public uint actionType;
		public WorldState preconditons;
		public WorldState effects;
		public int cost;

		public GoapAction(IAgent owner, int maxStateCount)
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