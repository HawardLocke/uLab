
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace Locke
{
	public class App : SingletonMono<App>
	{
		private Dictionary<string, Manager> mManagerDic = new Dictionary<string, Manager>();

		private bool canUpdate = false;

		// for quick access
		public static GameManager		gameManager = null;
		public static EventManager		eventManager = null;
		public static ResourceManager	resManager = null;
		public static LuaManager		luaManager = null;
		public static UIManager			uiManager = null;
		public static ThreadManager		threadManager = null;


		public void Initialize()
		{
			InitManagers();

			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			Application.targetFrameRate = AppDefine.GameFrameRate;
		}

		public void StartManagers()
		{
			IDictionaryEnumerator itor = mManagerDic.GetEnumerator();
			while (itor.MoveNext())
			{
				Manager mgr = (Manager)(itor.Entry.Value);
				mgr.Start();
			}
			canUpdate = true;
		}

		private void InitManagers()
		{
			gameManager = this.AddManager<GameManager>();
			eventManager = this.AddManager<EventManager>();
			resManager = this.AddManager<ResourceManager>();
			luaManager = this.AddManager<LuaManager>();
			uiManager = this.AddManager<UIManager>();
			threadManager = this.AddManager<ThreadManager>();

			foreach(var mgr in mManagerDic.Values)
			{
				mgr.Initialize();
			}
		}

		private void DestroyManagers()
		{
			foreach (var mgr in mManagerDic.Values)
			{
				mgr.Destroy();
			}
		}

		/*public T GetManager<T>() where T : Manager
		{
			string name = typeof(T).ToString();
			Manager mgr = null;
			mManagerDic.TryGetValue(name, out mgr);
			return mgr as T;
		}*/

		private T AddManager<T>() where T : Manager, new()
		{
			T mgr = null;
			string name = typeof(T).ToString();
			if (!mManagerDic.ContainsKey(name))
			{
				mgr = new T();
				mManagerDic.Add(name, mgr);
			}
			return mgr;
		}

		void OnDestroy()
		{
			DestroyManagers();
		}

		void Update()
		{
			if (!canUpdate)
				return;

			IDictionaryEnumerator itor = mManagerDic.GetEnumerator();
			while (itor.MoveNext())
			{
				Manager mgr = (Manager)(itor.Entry.Value);
				mgr.Update();
			}
		}

	}
}