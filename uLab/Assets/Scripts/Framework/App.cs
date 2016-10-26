
using System.Collections.Generic;

using UnityEngine;


namespace Locke
{
	public class App : SingletonMono<App>
	{
		private Dictionary<string, Manager> mManagerDic = new Dictionary<string, Manager>();

		// for quick access
		public static EventManager eventManager = null;
		public static ResourceManager resManager = null;
		public static LuaManager luaManager = null;

		public void Initialize()
		{
			AddAndInitManagers();
		}
		private void AddAndInitManagers()
		{
			eventManager = this.AddManager<EventManager>();
			resManager = this.AddManager<ResourceManager>();
			luaManager = this.AddManager<LuaManager>();
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

	}
}