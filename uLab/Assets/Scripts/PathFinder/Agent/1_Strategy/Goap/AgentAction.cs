
using ProtoBuf;

using Lite.Cmd;


namespace Lite.Strategy
{
	/*[ProtoContract]
	[ProtoInclude(1, typeof(ChopFirewood))]
	[ProtoInclude(2, typeof(ChopTree))]
	[ProtoInclude(3, typeof(DropOffFirewood))]
	[ProtoInclude(4, typeof(DropOffLogs))]
	[ProtoInclude(5, typeof(DropOffOre))]
	[ProtoInclude(6, typeof(DropOffTools))]
	[ProtoInclude(7, typeof(ForgeTool))]
	[ProtoInclude(8, typeof(MineOre))]
	[ProtoInclude(9, typeof(PickUpLogs))]
	[ProtoInclude(10, typeof(PickUpOre))]
	[ProtoInclude(11, typeof(PickUpTool))]*/
	public abstract class AgentAction : Lite.Goap.GoapAction
	{
		public int actionType;

		protected Agent owner;

		private bool isFinished = false;

		private bool isFailed = false;

		public AgentAction(Agent agent) :
			base(GoapDefines.STATE_COUNT)
		{
			owner = agent;
		}

		public void ApplyEffects()
		{
			owner.worldState.Merge(this.effects);
		}

		public virtual void OnActive()
		{
			AppFacade.Instance.commandManager.PushCommand(this);
		}

		public virtual void OnDeactive()
		{
			;
		}

		public void SetFinished()
		{
			isFinished = true;
			ApplyEffects();
		}

		public bool IsFinished()
		{
			return isFinished;
		}

		public virtual void Update() { }

		public byte[] _ToBytes()
		{
			ByteBuffer bb = new ByteBuffer();
			bb.WriteInt((int)actionType);
			bb.WriteLong(owner.Guid);
			bb.WriteBytes(ToBytes());
			return bb.ToBytes();
		}

		protected abstract byte[] ToBytes();

	}

}