
using ProtoBuf;

namespace Lite
{
	[ProtoContract]
	[ProtoInclude(1, typeof(sb_CreateEntity))]
	public abstract class IMessage
	{
		public int id;

		public abstract byte[] ToBytes();

	}

}
