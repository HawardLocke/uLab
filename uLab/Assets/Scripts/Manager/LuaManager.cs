

using System;
using UnityEngine;

using LuaInterface;


namespace Locke
{

	public class LuaManager : Manager
	{
		private LuaState luaState = null;
		private LuaLooper loop = null;
		private LuaLoader loader;

		public bool Initialize()
		{
			luaState = new LuaState();
			loader = new LuaLoader();

			OpenLibs();
			luaState.LuaSetTop(0);

			LuaBinder.Bind(luaState);
			LuaCoroutine.Register(luaState, this);

			InitLuaPath();
			InitLuaBundle();
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

		void InitLuaPath()
		{
			if (AppConst.DebugMode)
			{
				string rootPath = AppConst.FrameworkRoot;
				luaState.AddSearchPath(rootPath + "/Lua");
				luaState.AddSearchPath(rootPath + "/ToLua/Lua");
			}
			else
			{
				luaState.AddSearchPath(Util.DataPath + "lua");
			}
		}

		void InitLuaBundle()
		{
			if (loader.beZip)
			{
				loader.AddBundle("lua/lua.unity3d");
				loader.AddBundle("lua/lua_math.unity3d");
				loader.AddBundle("lua/lua_system.unity3d");
				loader.AddBundle("lua/lua_system_reflection.unity3d");
				loader.AddBundle("lua/lua_unityengine.unity3d");
				loader.AddBundle("lua/lua_common.unity3d");
				loader.AddBundle("lua/lua_logic.unity3d");
				loader.AddBundle("lua/lua_view.unity3d");
				loader.AddBundle("lua/lua_controller.unity3d");
				loader.AddBundle("lua/lua_misc.unity3d");

				loader.AddBundle("lua/lua_protobuf.unity3d");
				loader.AddBundle("lua/lua_3rd_cjson.unity3d");
				loader.AddBundle("lua/lua_3rd_luabitop.unity3d");
				loader.AddBundle("lua/lua_3rd_pbc.unity3d");
				loader.AddBundle("lua/lua_3rd_pblua.unity3d");
				loader.AddBundle("lua/lua_3rd_sproto.unity3d");
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

		public void LuaGC()
		{
			luaState.LuaGC(LuaGCOptions.LUA_GCCOLLECT);
		}

		public object[] CallFunction(string funcName, params object[] args)
		{
			LuaFunction func = luaState.GetFunction(funcName);
			if (func != null)
			{
				return func.Call(args);
			}
			return null;
		}

		public object[] DoFile(string filename)
		{
			return luaState.DoFile(filename);
		}

	}

}