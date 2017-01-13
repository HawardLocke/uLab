

namespace Lite.Goap
{
	public abstract class GoapAction : Lite.Graph.GraphEdge
	{
		protected IAgent owner;

		public uint actionType;
		public WorldState preconditons;
		public WorldState effects;
		public int cost;

		private bool isFinished;

		public GoapAction(IAgent owner, int maxStateCount):
			base(0,0,0)
		{
			this.owner = owner;
			actionType = 0;
			preconditons = new WorldState(maxStateCount);
			effects = new WorldState(maxStateCount);
			cost = 0;
			isFinished = false;
			OnSetupPreconditons();
			OnSetupEffects();
		}

		protected abstract void OnSetupPreconditons();
		protected abstract void OnSetupEffects();
		public void SetFinished() { isFinished = true; }
		public bool IsFinished() { return isFinished; }

		public virtual void Update() { }

	}

}