

using System;
using UnityEngine;

using LuaInterface;


namespace Locke
{

	public class LuaManager : SingletonComponent<LuaManager>, IManager
	{
		private LuaState luaState = null;
		private LuaLooper loop = null;

		public bool Initialize()
		{
			luaState = new LuaState();

			OpenLibs();
			luaState.LuaSetTop(0);

			LuaBinder.Bind(luaState);
			LuaCoroutine.Register(luaState, this);


			luaState.Start();
			StartLooper();
			StartMain();

			return true;
		}

		public void Destroy()
		{
			if (luaState != null)
			{
				LuaState state = luaState;
				luaState = null;

				if (loop != null)
				{
					loop.Destroy();
					loop = null;
				}

				state.Dispose();
			}
		}

		void OpenLibs()
		{
			luaState.OpenLibs(LuaDLL.luaopen_pb);
			luaState.OpenLibs(LuaDLL.luaopen_sproto_core);
			luaState.OpenLibs(LuaDLL.luaopen_protobuf_c);
			luaState.OpenLibs(LuaDLL.luaopen_lpeg);
			luaState.OpenLibs(LuaDLL.luaopen_bit);
			luaState.OpenLibs(LuaDLL.luaopen_socket_core);

			//cjson 比较特殊，只new了一个table，没有注册库，这里注册一下
			luaState.LuaGetField(LuaIndexes.LUA_REGISTRYINDEX, "_LOADED");
			luaState.OpenLibs(LuaDLL.luaopen_cjson);
			luaState.LuaSetField(-2, "cjson");
			luaState.OpenLibs(LuaDLL.luaopen_cjson_safe);
			luaState.LuaSetField(-2, "cjson.safe");
		}

		protected virtual void StartMain()
		{
			luaState.DoFile("Main.lua");
		}

		protected void StartLooper()
		{
			loop = gameObject.AddComponent<LuaLooper>();
			loop.luaState = luaState;
		}

	}

}