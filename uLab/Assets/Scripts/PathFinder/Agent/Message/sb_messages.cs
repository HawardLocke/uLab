
using ProtoBuf;

namespace Lite
{
	[ProtoContract]
	public class sb_CreateEntity : IMessage
	{
		[ProtoMember(1)]
		public int career;
		[ProtoMember(2)]
		public int x;
		[ProtoMember(3)]
		public int y;
		[ProtoMember(4)]
		public int z;

		public override byte[] ToBytes()
		{
			return ProtobufUtil.Serialize<sb_CreateEntity>(this);
		}

	}

}