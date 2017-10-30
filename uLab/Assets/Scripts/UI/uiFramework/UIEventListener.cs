using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


namespace Lite.ui
{


	public class UIEventListener : UnityEngine.EventSystems.EventTrigger
	{
		public delegate void VoidDelegate(GameObject go);
		public VoidDelegate onClick;
		public VoidDelegate onDown;
		public VoidDelegate onEnter;
		public VoidDelegate onExit;
		public VoidDelegate onUp;
		public VoidDelegate onSelect;
		public VoidDelegate onUpdateSelect;

		static public UIEventListener Get(GameObject go)
		{
			UIEventListener listener = go.GetComponent<UIEventListener>();
			if (listener == null)
				listener = go.AddComponent<UIEventListener>();
			return listener;
		}
		public override void OnPointerClick(PointerEventData eventData)
		{
			if (onClick != null) onClick(gameObject);
		}
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (onDown != null) onDown(gameObject);
		}
		public override void OnPointerEnter(PointerEventData eventData)
		{
			if (onEnter != null) onEnter(gameObject);
		}
		public override void OnPointerExit(PointerEventData eventData)
		{
			if (onExit != null) onExit(gameObject);
		}
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (onUp != null) onUp(gameObject);
		}
		public override void OnSelect(BaseEventData eventData)
		{
			if (onSelect != null) onSelect(gameObject);
		}
		public override void OnUpdateSelected(BaseEventData eventData)
		{
			if (onUpdateSelect != null) onUpdateSelect(gameObject);
		}

		// for lua
		public static void SetOnClick(GameObject go, LuaInterface.LuaFunction luafunc, LuaInterface.LuaTable instance)
		{
			if (go == null || luafunc == null)
			{
				Debug.LogError("UIEventListener SetOnClick: " + (go == null ? "param GameObject is null." : "param LuaFunction is null."));
				return;
			}
			UnityEngine.UI.Button btn = go.GetComponent<UnityEngine.UI.Button>();
			if (btn != null)
			{
				//btn.onClick.RemoveAllListeners(); why??
				btn.onClick.AddListener(delegate () { if (instance != null) luafunc.Call(instance, go); else luafunc.Call(go); });
			}
			else
			{
				Get(go).onClick += delegate (GameObject obj) { if (instance != null) luafunc.Call(instance, obj); else luafunc.Call(obj); };
			}

		}
		
	}


}