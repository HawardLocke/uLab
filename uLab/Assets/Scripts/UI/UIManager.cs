

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Locke.ui
{
	public class UIManager : Singleton<UIManager>
	{

		private Dictionary<WindowInfo, IWindow> shownWindows = new Dictionary<WindowInfo, IWindow>();

		private Stack<WindowStackData> windowsStack = new Stack<WindowStackData>();

		private WindowInfo currentWindowInfo = null;

		private GameObject normalRoot;
		private GameObject fixedRoot;
		private GameObject popupRoot;


		public void ShowWindow(WindowInfo info, IContext context = null)
		{
			if (currentWindowInfo == info)
			{
				Log.Error("already shown.");
				return;
			}

			GameObject go = null;
			IWindow script = null;

			if (!shownWindows.ContainsKey(info))
			{
				var rootObj = this.GetRoot(info.showMode);
				var prefab = Resources.Load(info.prefabPath) as GameObject;
				go = GameObject.Instantiate(prefab);
				if (go == null)
				{
					Log.Error(string.Format("file {0} does not exist.", info.prefabPath));
					return;
				}

				script = go.GetComponent<IWindow>();
				script.Enter(context);

				shownWindows.Add(info, script);

				var rectTran = go.GetComponent<RectTransform>();
				rectTran.SetParent(rootObj.transform);
				rectTran.localPosition = Vector3.zero;
				//rectTran.
				
				script = go.GetComponent<IWindow>();

				// set z to max

				
			}
			else
			{
				script = shownWindows[info];
				script.Resume(context);

				// set z to max
			}

			// make stack data
			WindowStackData newStackData = new WindowStackData();
			newStackData.windowInfo = info;
			newStackData.windowScript = script;
			foreach (var wnd in shownWindows)
			{
				if (wnd.Key == info)
					continue;
				wnd.Value.Pause();
				newStackData.hiddenWindows.Add(wnd.Key);
			}
			windowsStack.Push(newStackData);

			// save current status
			currentWindowInfo = info;
		}

		public void CloseWindow(WindowInfo info, IContext context = null)
		{

		}


		public void CloseCurrentWindow()
		{
			if (currentWindowInfo != null)
			{
				shownWindows[currentWindowInfo].Exit();
				shownWindows.Remove(currentWindowInfo);
				currentWindowInfo = null;

				WindowStackData stackdata = windowsStack.Peek();
				for(int i = 0; i < stackdata.hiddenWindows.Count; ++i)
				{
					var info = stackdata.hiddenWindows[i];
					var script = shownWindows[info];
					script.Resume();
					// as current
					if ( i == stackdata.hiddenWindows.Count - 1)
					{
						currentWindowInfo = info;
					}
				}
			}
		}


		public void Init()
		{
			SetupCanvas();
		}

		public GameObject GetRoot(ShowMode mode)
		{
			switch (mode)
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

		private void SetupCanvas()
		{
			GameObject uiRoot = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
			uiRoot.transform.localPosition = Vector3.zero;
			uiRoot.transform.localScale = Vector3.one;

			//var tran = uiRoot.GetComponent<RectTransform>();

			var canvas = uiRoot.GetComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;

			var scaler = uiRoot.GetComponent<CanvasScaler>();
			scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			scaler.referenceResolution = new Vector2(1136,640);
			scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
			scaler.matchWidthOrHeight = 1;

			var raycaster = uiRoot.GetComponent<GraphicRaycaster>();
			raycaster.ignoreReversedGraphics = true;
			raycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;

			GameObject eventRoot = new GameObject("EventSystem", typeof(UnityEngine.EventSystems.EventSystem), typeof(UnityEngine.EventSystems.StandaloneInputModule));
			eventRoot.transform.localPosition = Vector3.zero;
			eventRoot.transform.localScale = Vector3.one;

			// sebling
			if (normalRoot == null)
			{
				normalRoot = new GameObject("normal");
				normalRoot.transform.parent = uiRoot.transform;
				normalRoot.transform.localPosition = Vector3.zero;
				normalRoot.transform.localScale = Vector3.one;
			}
			if (fixedRoot == null)
			{
				fixedRoot = new GameObject("fixed");
				fixedRoot.transform.parent = uiRoot.transform;
				fixedRoot.transform.localPosition = Vector3.zero;
				fixedRoot.transform.localScale = Vector3.one;
			}
			if (popupRoot == null)
			{
				popupRoot = new GameObject("popup");
				popupRoot.transform.parent = uiRoot.transform;
				popupRoot.transform.localPosition = Vector3.zero;
				popupRoot.transform.localScale = Vector3.one;
			}
		}

	}

}