

using UnityEngine;



namespace Locke.ui
{

	public abstract class IWindow : MonoBehaviour
	{
		public void Enter(IContext context = null)
		{
			this.gameObject.SetActive(true);
			this.OnEnter(context);
		}

		public void Exit(IContext context = null)
		{
			this.OnExit(context);
			Destroy(this.gameObject);
		}

		public void Pause(IContext context = null)
		{
			this.gameObject.SetActive(false);
			this.OnPause(context);
		}

		public void Resume(IContext context = null)
		{
			this.gameObject.SetActive(true);
			this.OnResume(context);
		}

		protected virtual void OnEnter(IContext context) { }

		protected virtual void OnExit(IContext context) { }

		protected virtual void OnPause(IContext context) { }

		protected virtual void OnResume(IContext context) { }

		public T FindWidget<T>(string path) where T : Component
		{
			var child = this.transform.FindChild(path);
			if (child == null)
			{
				Debug.LogError(string.Format("cannot find child at {0}", path));
				return null;
			}
			var com = child.GetComponent<T>();
			if (com == null)
			{
				Debug.LogError(string.Format("cannot find component named {0}", typeof(T).Name));
				return null;
			}
			return com;
		}

	}

}