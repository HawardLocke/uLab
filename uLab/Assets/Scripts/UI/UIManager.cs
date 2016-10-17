

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Locke.ui
{
	public class UIManager : Singleton<UIManager>
	{

		private Dictionary<WindowInfo, GameObject> shownWindows = new Dictionary<WindowInfo, GameObject>();

		private Stack<IContext> backWindows = new Stack<IContext>();

		private GameObject normalRoot;
		private GameObject fixedRoot;
		private GameObject popupRoot;

		public void Init()
		{
			Transform uiRoot = null;

			if (normalRoot == null)
			{
				normalRoot = new GameObject("normal");
				normalRoot.transform.parent = uiRoot;
			}
			if (fixedRoot == null)
			{
				fixedRoot = new GameObject("fixed");
				fixedRoot.transform.parent = uiRoot;
			}
			if (popupRoot == null)
			{
				popupRoot = new GameObject("popup");
				popupRoot.transform.parent = uiRoot;
			}
		}

		public GameObject GetRoot(ShowMode mode)
		{
			switch(mode)
			{
				case ShowMode.Normal:
					return normalRoot;
				case ShowMode.Fixed:
					return fixedRoot;
				case ShowMode.Popup:
					return popupRoot;
			}
			return null;
		}

		public void ShowWindow(WindowInfo info)
		{
			GameObject go = null;
			if (!shownWindows.ContainsKey(info))
			{
				go = Resources.Load(info.prefabPath) as GameObject;
				if (go == null)
				{
					Log.Error(string.Format("ui prefab {0} does not exist.", info.prefabPath));
					return;
				}
				shownWindows.Add(info, go);
			}
			else
			{
				go = shownWindows[info];
			}
			IWindow script = go.GetComponent<IWindow>();
			//script.
		}

	}

}