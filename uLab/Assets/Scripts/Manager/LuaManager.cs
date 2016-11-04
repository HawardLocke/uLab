

using System;
using System.IO;
using UnityEngine;

using LuaInterface;


namespace Locke
{

	public class LuaManager : Manager
	{
		private LuaState luaState = null;
		private LuaLooper loop = null;

		public override void Initialize()
		{
			luaState = new LuaState();
			OpenLibs();
			luaState.LuaSetTop(0);
			LuaBinder.Bind(luaState);
			LuaCoroutine.Register(luaState, App.Instance);
		}

		/// <summary>
		/// start lua, must be called when resources updating finished.
		/// </summary>
		public override void Start()
		{
			InitLuaPath();
			InitLuaBundle();
			luaState.Start();
			StartLooper();
		}

		public override void Destroy()
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
			if (AppDefine.LuaBundleMode)
			{
				luaState.AddSearchPath(Util.DataPath + "lua");
			}
			else
			{
				luaState.AddSearchPath(Application.dataPath + "/ToLua/Lua");
				luaState.AddSearchPath(Application.dataPath + "/Lua");
				luaState.AddSearchPath(Application.dataPath + "/Lua/3rd/protobuf");
			}
		}

		void InitLuaBundle()
		{
			LuaFileUtils.Instance.beZip = AppDefine.LuaBundleMode;
			if (LuaFileUtils.Instance.beZip)
			{
				this.AddBundle("lua/lua.unity3d");
				this.AddBundle("lua/lua_math.unity3d");
				this.AddBundle("lua/lua_system.unity3d");
				this.AddBundle("lua/lua_system_reflection.unity3d");
				this.AddBundle("lua/lua_unityengine.unity3d");
				this.AddBundle("lua/lua_common.unity3d");
				this.AddBundle("lua/lua_logic.unity3d");
				this.AddBundle("lua/lua_view.unity3d");
				this.AddBundle("lua/lua_controller.unity3d");
				this.AddBundle("lua/lua_misc.unity3d");

				this.AddBundle("lua/lua_protobuf.unity3d");
				this.AddBundle("lua/lua_3rd_cjson.unity3d");
				this.AddBundle("lua/lua_3rd_luabitop.unity3d");
				this.AddBundle("lua/lua_3rd_pbc.unity3d");
				this.AddBundle("lua/lua_3rd_pblua.unity3d");
				this.AddBundle("lua/lua_3rd_sproto.unity3d");
				
				// Locke
				this.AddBundle("lua/lua_ui.unity3d");
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

		void AddBundle(string bundleName)
		{
			string url = Util.DataPath + bundleName.ToLower();
			if (File.Exists(url))
			{
				AssetBundle bundle = AssetBundle.LoadFromFile(url);
				if (bundle != null)
				{
					bundleName = bundleName.Replace("lua/", "").Replace(".unity3d", "");
					LuaFileUtils.Instance.AddSearchBundle(bundleName.ToLower(), bundle);
				}
			}
		}

		protected void StartLooper()
		{
			var gameObject = Util.GetGlobalGameObject();
			loop = gameObject.AddComponent<LuaLooper>();
			loop.luaState = luaState;
		}

		public void LuaGC()
		{
			if (luaState!= null)
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

		/// <summary>
		/// do lua file
		/// </summary>
		/// <param name="filename"> path/*lua </param>
		/// <returns></returns>
		public object[] DoFile(string filename)
		{
			return luaState.DoFile(filename);
		}

		/// <summary>
		/// require lua file
		/// </summary>
		/// <param name="filename"> path/file, without file extension </param>
		public void RequireFile(string filename)
		{
			luaState.Require(filename);
		}

	}

}