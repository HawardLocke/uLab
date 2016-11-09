using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;


namespace Locke
{
	using PacketPair = KeyValuePair<ushort, Packet>;

	public class NetworkManager : Manager
	{
		private SocketClient mSocketClient;
		static readonly object mLockObject = new object();
		static Queue<PacketPair> mMessageQueue = new Queue<PacketPair>();

		SocketClient SocketClient
		{
			get
			{
				if (mSocketClient == null)
					mSocketClient = new SocketClient();
				return mSocketClient;
			}
		}

		public override void Initialize()
		{
			SocketClient.Init();
		}

		public override void Start()
		{
			Util.CallMethod("Network", "Start");
		}

		public override void Destroy()
		{
			Util.CallMethod("Network", "Destroy");
			SocketClient.Destroy();
			Debug.Log("~NetworkManager was destroy");
		}

		public static void PushPacket(ushort msgId, Packet packet)
		{
			lock (mLockObject)
			{
				mMessageQueue.Enqueue(new PacketPair(msgId, packet));
			}
		}

		public override void Update()
		{
			if (mMessageQueue.Count > 0)
			{
				while (mMessageQueue.Count > 0)
				{
					PacketPair pair = mMessageQueue.Dequeue();
					//App.eventManager.SendMessage(MessageDefine.DISPATCH_MESSAGE, pair);
					Packet packet = pair.Value;
					Util.CallMethod("Network", "onMessage", packet.msgId, packet.data);
				}
			}
		}

		public void SendConnect()
		{
			SocketClient.SendConnect();
		}

		public void SendMessage(ushort msgId, LuaByteBuffer buffer)
		{
			var bb = new ByteBuffer();
			bb.WriteShort(msgId);
			bb.WriteBuffer(buffer);
			SocketClient.SendMessage(bb);
		}

	}
}