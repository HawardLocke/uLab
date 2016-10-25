

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;
using UnityEditor;

using LuaInterface;
using Locke;


public class LuaBundle
{

	static List<string> paths = new List<string>();
	static List<string> files = new List<string>();
	static List<AssetBundleBuild> maps = new List<AssetBundleBuild>();


	[MenuItem("Locke/Lua/Build for Windows")]
	public static void Build_for_Windows()
	{
		BuildLuaBundles(BuildTarget.StandaloneWindows);
	}

	[MenuItem("Locke/Lua/Build for IOS")]
	public static void Build_for_IOS()
	{
		BuildLuaBundles(BuildTarget.iOS);
	}

	[MenuItem("Locke/Lua/Build for Android")]
	public static void Build_for_Android()
	{
		BuildLuaBundles(BuildTarget.Android);
	}


	private static void BuildLuaBundles(BuildTarget target)
	{
		// delete directories in streamingasset
		if (Directory.Exists(Util.DataPath))
		{
			Directory.Delete(Util.DataPath, true);
		}

		string outputPath = "Assets/" + AppConst.AssetDir;
		BuildAssetBundleOptions opt = BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.UncompressedAssetBundle;
		BuildPipeline.BuildAssetBundles(outputPath, null, opt, target);
		BuildFileIndex();

		string streamDir = Application.dataPath + "/" + AppConst.LuaTempDir;
		if (Directory.Exists(streamDir)) Directory.Delete(streamDir, true);
		AssetDatabase.Refresh();
	}

	static void AddBuildMap(string bundleName, string pattern, string path)
	{
		string[] files = Directory.GetFiles(path, pattern);
		if (files.Length == 0) return;

		for (int i = 0; i < files.Length; i++)
		{
			files[i] = files[i].Replace('\\', '/');
		}
		AssetBundleBuild build = new AssetBundleBuild();
		build.assetBundleName = bundleName;
		build.assetNames = files;
		maps.Add(build);
	}


	static void HandleLuaBundle()
	{
		string streamDir = Application.dataPath + "/" + AppConst.LuaTempDir;
		if (!Directory.Exists(streamDir)) Directory.CreateDirectory(streamDir);

		string[] srcDirs = { Application.dataPath + "/Lua", Application.dataPath + "/ToLua/Lua" };
		for (int i = 0; i < srcDirs.Length; i++)
		{
			if (AppConst.LuaByteMode)
			{
				string sourceDir = srcDirs[i];
				string[] files = Directory.GetFiles(sourceDir, "*.lua", SearchOption.AllDirectories);
				int len = sourceDir.Length;

				if (sourceDir[len - 1] == '/' || sourceDir[len - 1] == '\\')
				{
					--len;
				}
				for (int j = 0; j < files.Length; j++)
				{
					string str = files[j].Remove(0, len);
					string dest = streamDir + str + ".bytes";
					string dir = Path.GetDirectoryName(dest);
					Directory.CreateDirectory(dir);
					EncodeLuaFile(files[j], dest);
				}
			}
			else
			{
				ToLuaMenu.CopyLuaBytesFiles(srcDirs[i], streamDir);
			}
		}
		string[] dirs = Directory.GetDirectories(streamDir, "*", SearchOption.AllDirectories);
		for (int i = 0; i < dirs.Length; i++)
		{
			string name = dirs[i].Replace(streamDir, string.Empty);
			name = name.Replace('\\', '_').Replace('/', '_');
			name = "lua/lua_" + name.ToLower() + AppConst.ExtName;

			string path = "Assets" + dirs[i].Replace(Application.dataPath, "");
			AddBuildMap(name, "*.bytes", path);
		}
		AddBuildMap("lua/lua" + AppConst.ExtName, "*.bytes", "Assets/" + AppConst.LuaTempDir);

		//-------------------------------处理非Lua文件----------------------------------
		string luaPath = AppDataPath + "/StreamingAssets/lua/";
		for (int i = 0; i < srcDirs.Length; i++)
		{
			paths.Clear(); files.Clear();
			string luaDataPath = srcDirs[i].ToLower();
			Recursive(luaDataPath);
			foreach (string f in files)
			{
				if (f.EndsWith(".meta") || f.EndsWith(".lua")) continue;
				string newfile = f.Replace(luaDataPath, "");
				string path = Path.GetDirectoryName(luaPath + newfile);
				if (!Directory.Exists(path)) Directory.CreateDirectory(path);

				string destfile = path + "/" + Path.GetFileName(f);
				File.Copy(f, destfile, true);
			}
		}
		AssetDatabase.Refresh();
	}

	static void HandleLuaFile()
	{
		string resPath = AppDataPath + "/StreamingAssets/";
		string luaPath = resPath + "/lua/";

		//----------复制Lua文件----------------
		if (!Directory.Exists(luaPath))
		{
			Directory.CreateDirectory(luaPath);
		}
		string[] luaPaths = { AppDataPath + "/LuaFramework/lua/", 
                              AppDataPath + "/LuaFramework/Tolua/Lua/" };

		for (int i = 0; i < luaPaths.Length; i++)
		{
			paths.Clear(); files.Clear();
			string luaDataPath = luaPaths[i].ToLower();
			Recursive(luaDataPath);
			int n = 0;
			foreach (string f in files)
			{
				if (f.EndsWith(".meta")) continue;
				string newfile = f.Replace(luaDataPath, "");
				string newpath = luaPath + newfile;
				string path = Path.GetDirectoryName(newpath);
				if (!Directory.Exists(path)) Directory.CreateDirectory(path);

				if (File.Exists(newpath))
				{
					File.Delete(newpath);
				}
				if (AppConst.LuaByteMode)
				{
					EncodeLuaFile(f, newpath);
				}
				else
				{
					File.Copy(f, newpath, true);
				}
				UpdateProgress(n++, files.Count, newpath);
			}
		}
		EditorUtility.ClearProgressBar();
		AssetDatabase.Refresh();
	}

	static string AppDataPath
	{
		get { return Application.dataPath.ToLower(); }
	}

	static void BuildFileIndex()
	{
		string resPath = AppDataPath + "/StreamingAssets/";
		///----------------------创建文件列表-----------------------
		string newFilePath = resPath + "/files.txt";
		if (File.Exists(newFilePath)) File.Delete(newFilePath);

		paths.Clear(); files.Clear();
		Recursive(resPath);

		FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
		StreamWriter sw = new StreamWriter(fs);
		for (int i = 0; i < files.Count; i++)
		{
			string file = files[i];
			string ext = Path.GetExtension(file);
			if (file.EndsWith(".meta") || file.Contains(".DS_Store")) continue;

			string md5 = Util.md5file(file);
			string value = file.Replace(resPath, string.Empty);
			sw.WriteLine(value + "|" + md5);
		}
		sw.Close(); fs.Close();
	}

	static void Recursive(string path)
	{
		string[] names = Directory.GetFiles(path);
		string[] dirs = Directory.GetDirectories(path);
		foreach (string filename in names)
		{
			string ext = Path.GetExtension(filename);
			if (ext.Equals(".meta")) continue;
			files.Add(filename.Replace('\\', '/'));
		}
		foreach (string dir in dirs)
		{
			paths.Add(dir.Replace('\\', '/'));
			Recursive(dir);
		}
	}

	static void UpdateProgress(int progress, int progressMax, string desc)
	{
		string title = "Processing...[" + progress + " - " + progressMax + "]";
		float value = (float)progress / (float)progressMax;
		EditorUtility.DisplayProgressBar(title, desc, value);
	}

	public static void EncodeLuaFile(string srcFile, string outFile)
	{
		if (!srcFile.ToLower().EndsWith(".lua"))
		{
			File.Copy(srcFile, outFile, true);
			return;
		}
		bool isWin = true;
		string luaexe = string.Empty;
		string args = string.Empty;
		string exedir = string.Empty;
		string currDir = Directory.GetCurrentDirectory();
		if (Application.platform == RuntimePlatform.WindowsEditor)
		{
			isWin = true;
			luaexe = "luajit.exe";
			args = "-b " + srcFile + " " + outFile;
			exedir = AppDataPath.Replace("assets", "") + "LuaEncoder/luajit/";
		}
		else if (Application.platform == RuntimePlatform.OSXEditor)
		{
			isWin = false;
			luaexe = "./luac";
			args = "-o " + outFile + " " + srcFile;
			exedir = AppDataPath.Replace("assets", "") + "LuaEncoder/luavm/";
		}
		Directory.SetCurrentDirectory(exedir);
		ProcessStartInfo info = new ProcessStartInfo();
		info.FileName = luaexe;
		info.Arguments = args;
		info.WindowStyle = ProcessWindowStyle.Hidden;
		info.ErrorDialog = true;
		info.UseShellExecute = isWin;
		Util.Log(info.FileName + " " + info.Arguments);

		Process pro = Process.Start(info);
		pro.WaitForExit();
		Directory.SetCurrentDirectory(currDir);
	}

}
