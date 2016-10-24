

using UnityEngine;


namespace Locke
{

	public class GameManager : Manager
	{
		public enum State
		{
			CheckVersion,
			Update,
			LoadResource,
			ReadyForPlay,
		}

		public void Initialize()
		{
			OnResourceLoaded();
		}

		public void Destroy()
		{

		}

		void Update()
		{

		}

		public void OnResourceLoaded()
		{
			App.Instance.GetManager<LuaManager>().Initialize();
		}

	}

}