﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;


namespace Lite
{

	public class GameManager : Manager
	{

		public enum State
		{
			CheckVersion,
			Update,
			LoadResource,
			ReadyForPlay,
		}

		public override void Initialize()
		{
			
		}

		public override void Destroy()
		{

		}

		public void CheckResource()
		{
			// 解压、更新
			CheckExtractResource();
		}

		private void CheckExtractResource()
		{
			bool isExist = Directory.Exists(Util.DataPath) && Directory.Exists(Util.DataPath + "lua") && File.Exists(Util.DataPath + "files.txt");
			if (!isExist && AppDefine.LuaBundleMode)
			{
				//App.eventManager.SendMessage(MessageDefine.UPDATE_MESSAGE, "Extract Resource...");
				App.Instance.StartCoroutine(ExtractResource());
			}
			else
			{
				//App.eventManager.SendMessage(MessageDefine.UPDATE_MESSAGE, "Update Resource...");
				App.Instance.StartCoroutine(UpdateResource());
			}
		}

		private IEnumerator ExtractResource()
		{
			string dataPath = Util.DataPath;  //数据目录
			string resPath = Util.AppContentPath(); //游戏包资源目录

			if (Directory.Exists(dataPath))
				Directory.Delete(dataPath, true);
			Directory.CreateDirectory(dataPath);

			string infile = resPath + "files.txt";
			string outfile = dataPath + "files.txt";
			//if (File.Exists(outfile))
			//	File.Delete(outfile);

			string message = "正在解包文件:>files.txt";
			Debug.Log(infile);
			Debug.Log(outfile);
			if (Application.platform == RuntimePlatform.Android)
			{
				WWW www = new WWW(infile);
				yield return www;

				if (www.isDone)
				{
					File.WriteAllBytes(outfile, www.bytes);
				}
				yield return 0;
			}
			else
				File.Copy(infile, outfile, true);

			yield return new WaitForEndOfFrame();

			//释放所有文件到数据目录
			string[] files = File.ReadAllLines(outfile);
			foreach (var file in files)
			{
				string[] fs = file.Split('|');
				infile = resPath + fs[0];
				outfile = dataPath + fs[0];

				message = "正在解包文件:>" + outfile;
				//Debug.Log("正在解包文件:>" + infile);
				App.eventManager.SendMessage(MessageDefine.UPDATE_MESSAGE, message);

				string dir = Path.GetDirectoryName(outfile);
				if (!Directory.Exists(dir))
					Directory.CreateDirectory(dir);

				if (Application.platform == RuntimePlatform.Android)
				{
					WWW www = new WWW(infile);
					yield return www;

					if (www.isDone)
					{
						File.WriteAllBytes(outfile, www.bytes);
					}
					yield return 0;
				}
				else
				{
					if (File.Exists(outfile))
					{
						File.Delete(outfile);
					}
					File.Copy(infile, outfile, true);
				}
				yield return new WaitForEndOfFrame();
			}
			message = "解包完成!!!";
			App.eventManager.SendMessage(MessageDefine.UPDATE_MESSAGE, message);
			yield return new WaitForSeconds(0.1f);

			message = string.Empty;

			App.Instance.StartCoroutine(UpdateResource());
		}

		private IEnumerator UpdateResource()
		{
			if (!AppDefine.UpdateMode)
			{
				OnResourceReady();
				yield break;
			}
			string dataPath = Util.DataPath;
			string url = AppDefine.WebUrl;
			string message = string.Empty;
			//string random = DateTime.Now.ToString("yyyymmddhhmmss");
			string listUrl = url + "files.txt";//?v=" + random;
			Debug.LogWarning("LoadUpdate---->>>" + listUrl);

			WWW www = new WWW(listUrl);
			yield return www;
			if (www.error != null)
			{
				OnUpdateResourceFailed(www.error, listUrl);
				yield break;
			}

			if (!Directory.Exists(dataPath))
			{
				Directory.CreateDirectory(dataPath);
			}
			File.WriteAllBytes(dataPath + "files.txt", www.bytes);

			string filesText = www.text;
			string[] files = filesText.Split('\n');
			for (int i = 0; i < files.Length; i++)
			{
				if (string.IsNullOrEmpty(files[i]))
					continue;

				string[] keyValue = files[i].Split('|');
				string f = keyValue[0];
				string localfile = (dataPath + f).Trim();
				string path = Path.GetDirectoryName(localfile);
				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);

				string fileUrl = url + f;// +"?v=" + random;
				bool canUpdate = false;
				if (File.Exists(localfile))
				{
					string remoteMd5 = keyValue[1].Trim();
					string localMd5 = Util.md5file(localfile);
					canUpdate = !remoteMd5.Equals(localMd5);
					if (canUpdate)
						File.Delete(localfile);
				}
				else
				{
					canUpdate = true;
				}
				if (canUpdate)
				{
					Debug.Log(fileUrl);
					message = "downloading>>" + localfile;
					App.eventManager.SendMessage(MessageDefine.UPDATE_MESSAGE, message);

					www = new WWW(fileUrl);
					yield return www;
					if (www.error != null)
					{
						OnUpdateResourceFailed(www.error, fileUrl);
						yield break;
					}
					File.WriteAllBytes(localfile, www.bytes);
				}
			}
			yield return new WaitForEndOfFrame();

			message = "更新完成!!";
			App.eventManager.SendMessage(MessageDefine.UPDATE_MESSAGE, message);

			OnResourceReady();
		}

		void OnUpdateResourceFailed(string error, string url)
		{
			string message = "更新失败!>" + error + ": " + url;
			App.eventManager.SendMessage(MessageDefine.UPDATE_MESSAGE, message);
		}

		public void OnResourceReady()
		{
			try
			{
				App.luaManager.RequireFile("Main");

				App.Instance.StartManagers();

				App.luaManager.DoFile("Main.lua");
				Util.CallMethod("CSharpPort.Game_OnInitialize");
			}
			catch (System.Exception e)
			{
				App.eventManager.SendMessage(MessageDefine.UPDATE_MESSAGE, e.ToString());
			}
		}

	}

}