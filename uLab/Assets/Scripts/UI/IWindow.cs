

using UnityEngine;



namespace Locke.ui
{

	public abstract class IWindow : MonoBehaviour
	{
		private WindowInfo _windowInfo = null;
		public WindowInfo windowInfo { set { _windowInfo = value; } }

		#region Module internal functions, do not call them manually !

		/// <summary>
		/// Do not call this manually ! Only called by UIManager.
		/// </summary>
		/// <param name="context"></param>
		public void _Enter(IContext context = null)
		{
			this.gameObject.SetActive(true);
			this.OnEnter(context);
		}

		/// <summary>
		/// Do not call this manually ! Only called by UIManager.
		/// </summary>
		/// <param name="context"></param>
		public void _Exit(IContext context = null)
		{
			this.OnExit(context);
			Destroy(this.gameObject);
		}

		/// <summary>
		/// Do not call this manually ! Only called by UIManager.
		/// </summary>
		/// <param name="context"></param>
		public void _Pause(IContext context = null)
		{
			this.gameObject.SetActive(false);
			this.OnPause(context);
		}

		/// <summary>
		/// Do not call this manually ! Only called by UIManager.
		/// </summary>
		/// <param name="context"></param>
		public void _Resume(IContext context = null)
		{
			this.gameObject.SetActive(true);
			this.OnResume(context);
		}

		#endregion

		#region handlers to be override

		protected virtual void OnEnter(IContext context) { }

		protected virtual void OnExit(IContext context) { }

		protected virtual void OnPause(IContext context) { }

		protected virtual void OnResume(IContext context) { }

		#endregion

		/// <summary>
		/// Close this window. Should only be called by self.
		/// </summary>
		protected void CloseMe()
		{
			UIManager.Instance.CloseWindow(this._windowInfo);
		}
		/// <summary>
		/// Get a component in a child of the window.
		/// </summary>
		/// <param name="context"></param>
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