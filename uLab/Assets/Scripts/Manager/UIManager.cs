

using Locke.ui;


namespace Locke
{

	public class UIManager : Manager
	{
		WindowManager mWindowMgr = null;

		public override void Initialize()
		{
			mWindowMgr = new WindowManager();
			mWindowMgr.Init();
		}

		public override void Destroy()
		{
			this.Cleanup();
		}

		public IWindow GetWindow(string windowName)
		{
			WindowInfo info = this.GetWindowInfo(windowName);
			if (info != null)
				return mWindowMgr.GetWindow(info);
			return null;
		}

		public IWindow OpenWindow(string windowName)
		{
			WindowInfo info = this.GetWindowInfo(windowName);
			if (info != null)
				return this.OpenWindow(info, null);
			else
				return null;
		}

		public IWindow OpenWindow(WindowInfo windowInfo, IContext context = null)
		{
			return mWindowMgr.OpenWindow(windowInfo, context);
		}

		public void CloseWindow(string windowName)
		{
			WindowInfo info = this.GetWindowInfo(windowName);
			if (info != null)
				mWindowMgr.CloseWindow(info);
		}

		public void CloseWindow(IWindow script)
		{
			mWindowMgr.CloseWindow(script);
		}

		public void SetMainWindow(WindowInfo windowInfo)
		{
			mWindowMgr.mainWindowInfo = windowInfo;
		}

		public void BackToMainWindow()
		{
			mWindowMgr.BackToMainWindow();
		}

		public void Cleanup()
		{
			mWindowMgr.Cleanup();
		}

		private WindowInfo GetWindowInfo(string windowName)
		{
			WindowInfo info = GameUI.GetWindowInfo(windowName);
			if (info == null)
			{
				Log.Error(string.Format("Cannot find WindowInfo named : %s", windowName));
			}
			return info;
		}

	}

}