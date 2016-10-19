

using System.Collections;
using System.Collections.Generic;


namespace Locke.ui
{

	public enum ShowMode
	{
		Normal = 0,
		Fixed,		// always exist, cannot be closed.
		Popup,
	}

	public enum OpenAction
	{
		DoNothing,
		HideNormals,
		HideAll,
	}

	public class WindowInfo
	{
		public string prefabPath;
		public string name;
		public ShowMode showMode = ShowMode.Normal;
		public OpenAction openAction = OpenAction.DoNothing;

		public WindowInfo(string path, ShowMode showMode, OpenAction openAct)
		{
			this.prefabPath = path;
			this.name = path.Substring(path.LastIndexOf('/') + 1);
			this.showMode = showMode;
			this.openAction = openAct;
		}

	}


	public class WindowStackData
	{
		public WindowInfo windowInfo;
		public IWindow windowScript;

		public Dictionary<WindowInfo, IWindow> recordedWindows = null;
		public WindowInfo recordedCurrentWindow = null;
	}

	public abstract class IContext
	{

	}

}
