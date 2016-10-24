
using System.Collections.Generic;

using UnityEngine;


namespace Locke
{
	public class App : Singleton<App>
	{
		private Dictionary<string, Manager> mManagerDic = new Dictionary<string, Manager>();

		public void Inittialize()
		{
			InitManagers();
		}
		private void InitManagers()
		{
			GameObject go = GameObject.Find("GameManagers");
			if (go == null)
				go = new GameObject("GameManagers");

			this.AddManager<ResourceManager>();
			this.AddManager<LuaManager>();
		}
		public T GetManager<T>() where T : Manager
		{
			string name = typeof(T).ToString();
			Manager mgr = null;
			mManagerDic.TryGetValue(name, out mgr);
			return mgr as T;
		}

		private void AddManager<T>() where T : Manager
		{
			string name = typeof(T).ToString();
			if (!mManagerDic.ContainsKey(name))
			{
				GameObject go = GameObject.Find("GameManagers");
				T mgr = go.AddComponent<T>();
				mManagerDic.Add(name, mgr);
			}
		}

	}
}