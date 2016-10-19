

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Locke.ui
{
	public class UIManager : Singleton<UIManager>
	{
		private Dictionary<WindowInfo, IWindow> allWindows = new Dictionary<WindowInfo, IWindow>();
		private Dictionary<WindowInfo, IWindow> shownWindows = new Dictionary<WindowInfo, IWindow>();

		private Stack<WindowStackData> windowsStack = new Stack<WindowStackData>();

		private WindowInfo currentWindowInfo = null;

		private GameObject normalRoot;
		private GameObject fixedRoot;
		private GameObject popupRoot;


		public void ShowWindow(WindowInfo targetWindowInfo, IContext context = null)
		{
			if (currentWindowInfo == targetWindowInfo)
			{
				Log.Error("already shown.");
				return;
			}

			GameObject go = null;
			IWindow script = null;

			if (!shownWindows.ContainsKey(targetWindowInfo))
			{
				var rootObj = this.GetRoot(targetWindowInfo.showMode);
				var prefab = Resources.Load(targetWindowInfo.prefabPath) as GameObject;
				go = GameObject.Instantiate(prefab);
				if (go == null)
				{
					Log.Error(string.Format("file {0} does not exist.", targetWindowInfo.prefabPath));
					return;
				}

				script = go.GetComponent<IWindow>();
				script.windowInfo = targetWindowInfo;
				script._Enter(context);

				allWindows.Add(targetWindowInfo, script);
				shownWindows.Add(targetWindowInfo, script);

				var rectTran = go.GetComponent<RectTransform>();
				rectTran.SetParent(rootObj.transform);
				rectTran.localPosition = Vector3.zero;
				
				script = go.GetComponent<IWindow>();

				MakeWindowBackground(targetWindowInfo, script);
			}
			else
			{
				script = shownWindows[targetWindowInfo];
				script._Resume(context);
			}

			MakeAsTopWindow(script);

			// do open action
			Dictionary<WindowInfo, IWindow> recordWindows = new Dictionary<WindowInfo, IWindow>();
			switch(targetWindowInfo.openAction)
			{
				case OpenAction.HideAll:
					foreach (var wnd in shownWindows)
					{
						if (wnd.Key == targetWindowInfo)
							continue;
						wnd.Value._Pause();
						recordWindows.Add(wnd.Key, wnd.Value);
					}
					foreach (var wnd in recordWindows)
					{
						shownWindows.Remove(wnd.Key);
					}
					break;
				case OpenAction.HideNormals:
					foreach (var wnd in shownWindows)
					{
						if (wnd.Key == targetWindowInfo || wnd.Key.showMode != ShowMode.Normal)
							continue;
						wnd.Value._Pause();
						recordWindows.Add(wnd.Key, wnd.Value);
					}
					foreach (var wnd in recordWindows)
					{
						shownWindows.Remove(wnd.Key);
					}
					break;
				case OpenAction.DoNothing:
				default:
					break;
			}

			// make stack data
			WindowStackData newStackData = new WindowStackData();
			newStackData.windowInfo = targetWindowInfo;
			newStackData.windowScript = script;
			newStackData.recordedWindows = recordWindows;
			newStackData.recordedCurrentWindow = currentWindowInfo;
			// push stack
			windowsStack.Push(newStackData);
			// save current status
			currentWindowInfo = targetWindowInfo;
		}

		public void CloseWindow(WindowInfo targetWindowInfo)
		{
			WindowInfo curInfo = targetWindowInfo;
			IWindow curWndScript = shownWindows[curInfo];
			curWndScript._Exit();

			allWindows.Remove(targetWindowInfo);
			if (shownWindows.ContainsKey(targetWindowInfo))
				shownWindows.Remove(targetWindowInfo);
			//currentWindowInfo = null;

			WindowStackData stackdata = windowsStack.Peek();
			windowsStack.Pop();
			switch (curInfo.openAction)
			{
				case OpenAction.HideAll:
				case OpenAction.HideNormals:
					foreach (var wnd in stackdata.recordedWindows)
					{
						var info = wnd.Key;
						var script = wnd.Value;
						script._Resume();
						shownWindows.Add(info, script);
					}
					currentWindowInfo = stackdata.recordedCurrentWindow;
					break;
				case OpenAction.DoNothing:
				default:
					currentWindowInfo = stackdata.recordedCurrentWindow;
					break;
			}
		}

		private void CloseAllWindowExcept(WindowInfo info)
		{
			foreach (var wnd in shownWindows)
			{
				WindowInfo tmpInfo = wnd.Key;
				if (tmpInfo == info)
					continue;
				if (tmpInfo.showMode == ShowMode.Fixed || tmpInfo.showMode == ShowMode.Popup)
				wnd.Value._Exit();
			}
		}

		private void MakeWindowBackground(WindowInfo targetWindowInfo, IWindow targetWindow)
		{
			GameObject newGo = null;
			Image img = null;
			switch(targetWindowInfo.backgroundMode)
			{
				case BackgroundMode.Transparent:
					newGo = new GameObject("_auto_background", typeof(RectTransform), typeof(Image));
					img = newGo.GetComponent<Image>();
					img.color = new Color(0,0,0,0);
					img.raycastTarget = true;
					break;
				case BackgroundMode.Dark:
					newGo = new GameObject("_auto_background", typeof(RectTransform), typeof(Image));
					img = newGo.GetComponent<Image>();
					img.color = new Color(0,0,0,100/255.0f);
					img.raycastTarget = true;
					break;
				case BackgroundMode.None:
				default:
					newGo = null;
					break;
			}
			if (newGo != null)
			{
				var rectTran = newGo.GetComponent<RectTransform>();
				rectTran.SetParent(targetWindow.transform);
				rectTran.SetSiblingIndex(0);
				rectTran.localPosition = Vector3.zero;
				rectTran.anchorMin = new Vector2(0.5f, 0.5f);
				rectTran.anchorMax = new Vector2(0.5f, 0.5f);
				rectTran.pivot = new Vector2(0.5f, 0.5f);
				rectTran.sizeDelta = new Vector2(9000, 9000);
			}
		}

		private void MakeAsTopWindow(IWindow targetWindow)
		{
			var siblingCount = targetWindow.transform.parent.childCount;
			targetWindow.transform.SetSiblingIndex(siblingCount-1);
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