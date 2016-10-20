

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Locke.ui
{
	public class UIManager : Singleton<UIManager>
	{
		private Dictionary<WindowInfo, List<IWindow>> mAllWindowDic = new Dictionary<WindowInfo, List<IWindow>>();

		private Stack<WindowStackData> mWindowStack = new Stack<WindowStackData>();


		public static void OpenWindow(WindowInfo windowInfo, IContext context = null)
		{
			try
			{
				UIManager.Instance._OpenWindow(windowInfo, context);
			}
			catch (UnityException ue)
			{
				Log.Error(ue.ToString());
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}

		public static void CloseWindow(IWindow script)
		{
			try
			{
				UIManager.Instance._CloseWindow(script);
			}
			catch (UnityException ue)
			{
				Log.Error(ue.ToString());
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}

		/*public static IWindow GetLastWindow(WindowInfo windowInfo)
		{
			var mAllWindowDic = UIManager.Instance.mAllWindowDic;
			if (mAllWindowDic.ContainsKey(windowInfo))
			{
				List<IWindow> windowList = mAllWindowDic[windowInfo];
				if (windowList.Count == 0)
					return null;
				IWindow script = windowList[windowList.Count - 1];
				if (script != null)
					return script;
			}
			return null;
		}

		public static IWindow GetLastActivedWindow(WindowInfo windowInfo)
		{
			var mAllWindowDic = UIManager.Instance.mAllWindowDic;
			if (mAllWindowDic.ContainsKey(windowInfo))
			{
				List<IWindow> windowList = mAllWindowDic[windowInfo];
				if (windowList.Count == 0)
					return null;
				IWindow script = windowList[windowList.Count - 1];
				if (script != null && script.IsActived)
					return script;
			}
			return null;
		}*/

		private void _OpenWindow(WindowInfo windowInfo, IContext context = null)
		{
			IWindow script = null;
			List<IWindow> windowList = null;

			if (!mAllWindowDic.ContainsKey(windowInfo))
			{
				windowList = new List<IWindow>();
				mAllWindowDic.Add(windowInfo, windowList);
			}
			else
			{
				if (mWindowStack.Count > 0)
				{
					var stackData = mWindowStack.Peek();
					if (stackData.windowInfo == windowInfo)// current shown window is target window.
						return;
				}
				windowList = mAllWindowDic[windowInfo];
			}

			script = CreateWindowInst(windowInfo);
			windowList.Add(script);
			script._Enter(context);

			MakeAsTopWindow(script.gameObject);

			// do open action
			List<IWindow> recordWindows = null;
			switch(windowInfo.openAction)
			{
				case OpenAction.HideAll:
					recordWindows = new List<IWindow>();
					foreach (var wnd in mAllWindowDic)
					{
						if (wnd.Key == windowInfo)
							continue;
						for (int i = 0; i < wnd.Value.Count; ++i)
						{
							IWindow tmpWindow = wnd.Value[i];
							if (!tmpWindow.IsActived)
								continue;
							tmpWindow._Pause();
							recordWindows.Add(tmpWindow);
						}
					}
					break;
				case OpenAction.HideNormals:
					recordWindows = new List<IWindow>();
					foreach (var wnd in mAllWindowDic)
					{
						if (wnd.Key == windowInfo || wnd.Key.showMode != ShowMode.Normal)
							continue;
						for (int i = 0; i < wnd.Value.Count; ++i)
						{
							IWindow tmpWindow = wnd.Value[i];
							if (!tmpWindow.IsActived)
								continue;
							tmpWindow._Pause();
							recordWindows.Add(tmpWindow);
						}
					}
					break;
				case OpenAction.DoNothing:
				default:
					recordWindows = null;
					break;
			}

			if (windowInfo.openAction != OpenAction.DoNothing)
			{
				WindowStackData newStackData = new WindowStackData();
				newStackData.windowInfo = windowInfo;
				newStackData.windowScript = script;
				newStackData.recordedWindows = recordWindows;
				mWindowStack.Push(newStackData);
			}

		}

		private void _CloseWindow(IWindow script)
		{
			if (script == null)
			{
				Log.Error("Trying to close a null window.");
				return;
			}

			WindowInfo windowInfo = script.windowInfo;

			if (!mAllWindowDic.ContainsKey(windowInfo))
			{
				Log.Error(string.Format("mAllWindowDic does not contains {0}", windowInfo.name));
				return;
			}
			
			if (windowInfo.openAction == OpenAction.DoNothing)
			{
				List<IWindow> windowList = mAllWindowDic[windowInfo];
				windowList.Remove(script);
				script._Exit();
			}
			else
			{
				var topStackdata = mWindowStack.Peek();
				if (topStackdata.windowInfo != windowInfo)
				{
					Log.Error(string.Format("Cannot close {0} in this way !", windowInfo.name));
					return;
				}
				else
				{
					List<IWindow> windowList = mAllWindowDic[windowInfo];
					windowList.Remove(script);
					script._Exit();
					switch (windowInfo.openAction)
					{
						case OpenAction.HideAll:
						case OpenAction.HideNormals:
							for (int i = 0; i < topStackdata.recordedWindows.Count; ++i)
							{
								IWindow tempScript = topStackdata.recordedWindows[i];
								tempScript._Resume();
							}
							break;
						case OpenAction.DoNothing:
						default:
							break;
					}
					mWindowStack.Pop();
				}
			}

		}

		private IWindow CreateWindowInst(WindowInfo windowInfo)
		{
			var prefab = Resources.Load(windowInfo.prefabPath) as GameObject;
			var go = GameObject.Instantiate(prefab);
			if (go == null)
			{
				Log.Error(string.Format("file {0} does not exist.", windowInfo.prefabPath));
				return null;
			}
			IWindow script = go.GetComponent<IWindow>();
			if (script == null)
			{
				Log.Error("Component IWindow does not exist.");
				return null;
			}

			script.windowInfo = windowInfo;

			var modeRoot = this.GetModeRoot(windowInfo.showMode);
			var rectTran = go.GetComponent<RectTransform>();
			rectTran.SetParent(modeRoot.transform);
			rectTran.localPosition = Vector3.zero;

			MakeWindowBackground(windowInfo, go);

			return script;
		}

		private void MakeWindowBackground(WindowInfo windowInfo, GameObject windowObj)
		{
			GameObject newGo = null;
			Image img = null;
			switch(windowInfo.backgroundMode)
			{
				case BackgroundMode.Transparent:
					newGo = new GameObject("_auto_genereted_background_", typeof(RectTransform), typeof(Image));
					img = newGo.GetComponent<Image>();
					img.color = new Color(0,0,0,0);
					img.raycastTarget = true;
					break;
				case BackgroundMode.Dark:
					newGo = new GameObject("_auto_genereted_background_", typeof(RectTransform), typeof(Image));
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
				rectTran.SetParent(windowObj.transform);
				rectTran.SetSiblingIndex(0);
				rectTran.localPosition = Vector3.zero;
				rectTran.anchorMin = new Vector2(0.5f, 0.5f);
				rectTran.anchorMax = new Vector2(0.5f, 0.5f);
				rectTran.pivot = new Vector2(0.5f, 0.5f);
				rectTran.sizeDelta = new Vector2(9000, 9000);
			}
		}

		private void MakeAsTopWindow(GameObject windowObj)
		{
			var siblingCount = windowObj.transform.parent.childCount;
			windowObj.transform.SetSiblingIndex(siblingCount-1);
		}

		public void Init()
		{
			SetupCanvas();
		}

		private void SetupCanvas()
		{
			GameObject uiRoot = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
			uiRoot.transform.localPosition = Vector3.zero;
			uiRoot.transform.localScale = Vector3.one;

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

			GameObject normalRoot = new GameObject("normal");
			normalRoot.transform.parent = uiRoot.transform;
			normalRoot.transform.localPosition = Vector3.zero;
			normalRoot.transform.localScale = Vector3.one;

			GameObject fixedRoot = new GameObject("fixed");
			fixedRoot.transform.parent = uiRoot.transform;
			fixedRoot.transform.localPosition = Vector3.zero;
			fixedRoot.transform.localScale = Vector3.one;

			GameObject popupRoot = new GameObject("popup");
			popupRoot.transform.parent = uiRoot.transform;
			popupRoot.transform.localPosition = Vector3.zero;
			popupRoot.transform.localScale = Vector3.one;
			
		}

		public GameObject GetModeRoot(ShowMode mode)
		{
			switch (mode)
			{
				case ShowMode.Normal:
					return GameObject.Find("Canvas/normal");
				case ShowMode.Fixed:
					return GameObject.Find("Canvas/fixed");
				case ShowMode.Popup:
					return GameObject.Find("Canvas/popup");
			}
			return null;
		}

	}

}