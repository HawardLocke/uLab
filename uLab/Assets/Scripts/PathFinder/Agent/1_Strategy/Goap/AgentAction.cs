
#define STAND_ALONE

using Lite.Cmd;


namespace Lite.Strategy
{
	/*
	using ProtoBuf;
	[ProtoContract]
	class Person
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public string Name { get; set; }
		[ProtoMember(3)]
		public Address Address { get; set; }
	}
	*/
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

		public byte[] Serialize()
		{
			ByteBuffer bb = new ByteBuffer();
			bb.WriteInt((int)actionType);
			bb.WriteLong(owner.Guid);
			bb.WriteBytes(ToBytes());
			return bb.ToBytes();
		}

		public abstract byte[] ToBytes();

	}

}