using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

namespace Locke
{
	public class NetworkManager : Manager
	{
		private SocketClient socket;
		static readonly object m_lockObject = new object();
		static Queue<KeyValuePair<int, ByteBuffer>> mEvents = new Queue<KeyValuePair<int, ByteBuffer>>();

		SocketClient SocketClient
		{
			get
			{
				if (socket == null)
					socket = new SocketClient();
				return socket;
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

		public static void AddEvent(int _event, ByteBuffer data)
		{
			lock (m_lockObject)
			{
				mEvents.Enqueue(new KeyValuePair<int, ByteBuffer>(_event, data));
			}
		}

		public override void Update()
		{
			if (mEvents.Count > 0)
			{
				while (mEvents.Count > 0)
				{
					KeyValuePair<int, ByteBuffer> _event = mEvents.Dequeue();
					App.eventManager.SendMessage(MessageDefine.DISPATCH_MESSAGE, _event);
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