

using UnityEngine;


namespace Locke.ui
{

	public abstract class IWindow : MonoBehaviour
	{

		public void Show()
		{
			this.gameObject.SetActive(true);
		}

		public void Hide()
		{
			this.gameObject.SetActive(false);
		}

		public void Destroy()
		{
			Destroy(this.gameObject);
		}

		public virtual void OnEnter(IContext context) { }

		public virtual void OnExit(IContext context) { }

		public virtual void OnPause(IContext context) { }

		public virtual void OnResume(IContext context) { }

	}

}