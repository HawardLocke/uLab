using UnityEngine;
using LuaInterface;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace Locke
{
	public class LuaBehaviour : MonoBehaviour
	{
		//private string data = null;
		private Dictionary<string, LuaFunction> buttons = new Dictionary<string, LuaFunction>();

		protected void Awake()
		{
			Util.CallMethod(name, "Awake", gameObject);
		}

		protected void Start()
		{
			Util.CallMethod(name, "Start");
		}

		protected void OnClick()
		{
			Util.CallMethod(name, "OnClick");
		}

		protected void OnClickEvent(GameObject go)
		{
			Util.CallMethod(name, "OnClick", go);
		}

		public void AddClick(GameObject go, LuaFunction luafunc)
		{
			if (go == null || luafunc == null)
				return;
			buttons.Add(go.name, luafunc);
			go.GetComponent<Button>().onClick.AddListener(
				delegate()
				{
					luafunc.Call(go);
				}
			);
		}

		public void RemoveClick(GameObject go)
		{
			if (go == null) return;
			LuaFunction luafunc = null;
			if (buttons.TryGetValue(go.name, out luafunc))
			{
				luafunc.Dispose();
				luafunc = null;
				buttons.Remove(go.name);
			}
		}

		public void ClearClick()
		{
			foreach (var de in buttons)
			{
				if (de.Value != null)
				{
					de.Value.Dispose();
				}
			}
			buttons.Clear();
		}

		//-----------------------------------------------------------------
		protected void OnDestroy()
		{
			ClearClick();
#if ASYNC_MODE
            string abName = name.ToLower().Replace("panel", "");
            ResManager.UnloadAssetBundle(abName + AppDefine.ExtName);
#endif
			Util.ClearMemory();
			Debug.Log("~" + name + " was destroy!");
		}
	}
}