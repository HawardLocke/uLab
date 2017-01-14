

namespace Lite.Goap
{
	public abstract class GoapAction : Lite.Graph.GraphEdge
	{
		public uint actionType;
		public WorldState preconditons;
		public WorldState effects;
		public int cost;

		private bool isFinished;

		public GoapAction(int maxStateCount):
			base(0,0,0)
		{
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
		public void SetFinished() { isFinished = true; ApplyEffects(); }
		public bool IsFinished() { return isFinished; }
		public virtual void Update() { }
		public abstract void ApplyEffects();

	}

}