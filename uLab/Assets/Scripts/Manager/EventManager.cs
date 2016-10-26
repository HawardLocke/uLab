

using System;
using System.Collections;
using System.Collections.Generic;


namespace Locke
{

	public class EventManager : Manager
	{

		private Dictionary<string, List<IListener>> mListenerMap = new Dictionary<string, List<IListener>>();

		public override void Initialize()
		{
			mListenerMap.Clear();
		}

		public override void Destroy()
		{

		}

		public override void Update()
		{
			
		}

		public void AddListener(string eventName, IListener listener)
		{
			List<IListener> listenerList = null;
			if (!mListenerMap.TryGetValue(eventName, out listenerList))
			{
				listenerList = new List<IListener>();
				mListenerMap.Add(eventName, listenerList);
			}
			listenerList.Add(listener);
		}

		public void RemoveListener(string eventName, IListener listener)
		{
			List<IListener> listenerList = null;
			if (mListenerMap.TryGetValue(eventName, out listenerList))
			{
				listenerList.Remove(listener);
			}
		}

		public void PushEvent(Event evnt)
		{
			List<IListener> listenerList = null;
			if (mListenerMap.TryGetValue(evnt.name, out listenerList))
			{
				for (int i = 0; i < listenerList.Count; ++i)
				{
					listenerList[i].OnEvent(evnt);
				}
			}
		}


	}

}