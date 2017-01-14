

namespace Lite.Strategy
{

	public abstract class GoapAgentAction : Lite.Goap.GoapAction
	{
		protected Agent owner;

		public GoapAgentAction(Agent agent) :
			base(GoapDefines.STATE_COUNT)
		{

		}

		public override void ApplyEffects()
		{
			owner.worldState.Merge(this.effects);
		}

	}

}