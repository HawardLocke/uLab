
using System;
using System.IO;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;


namespace Lite
{
	[Serializable]
	public abstract class Request
	{
		public int typeID;

		public long ownerGuid;

		public byte[] Serialize()
		{
			MemoryStream stream = new MemoryStream();
			BinaryFormatter b = new BinaryFormatter();
			b.Serialize(stream, this);
			byte[] buffer = stream.ToArray();
			stream.Close();
			return buffer;
		}

		public Request Deserialize(byte[] buffer)
		{
			MemoryStream stream = new MemoryStream(buffer);
			BinaryFormatter b = new BinaryFormatter();
			Request inst = b.Deserialize(stream) as Request;
			stream.Close();
			return inst;
		}
		
	}

}