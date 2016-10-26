

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

		public override void Initialize()
		{
			OnResourceLoaded();
		}

		public override void Destroy()
		{

		}
		public void OnResourceLoaded()
		{
			
		}

	}

}