using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using Lite;
using Lite.AStar;


public class MapEditor : MonoBehaviour
{
	int width = 50;
	int height = 30;
	int[,] nodeMarkList;
	string savePath;

	void Start()
	{
		nodeMarkList = new int[width, height];
		for (int x = 0; x < width; ++x)
		{
			for (int y = 0; y < height; ++y)
			{
				nodeMarkList[x, y] = 0;
			}
		}
		savePath = Application.dataPath + "/../map.txt";
		Load(savePath, nodeMarkList);
	}

	
	void OnGUI()
	{
		int offsetX = 50;
		int offsetY = 10;
		int gw = 20;
		int gh = 20;
		for (int x = 0; x < width; ++x)
		{
			for (int y = 0; y < height; ++y)
			{
				if (GUI.Button(new Rect(offsetX + x * gw + 0.05f * gw, offsetY + y * gh + 0.05f * gh, 0.9f * gw, 0.9f * gh), nodeMarkList[x, y]==0?"":"1"))
				{
					nodeMarkList[x, y] = nodeMarkList[x, y]==0?1:0;
				}
			}
		}

		if (GUI.Button(new Rect(5, 10, 40, 20), "save"))
		{
			Save();
		}
	}

	static public void Load(string path, int[,] data)
	{
		StreamReader sr = new StreamReader(path, Encoding.ASCII);
		string line;
		int y = 0;
		while ((line = sr.ReadLine()) != null)
		{
			char[] arr = line.ToCharArray();
			for (int x = 0; x < arr.Length; ++x)
				data[x, y] = int.Parse(arr[x].ToString());
			y++;
		}
		sr.Close();
	}

	void Save()
	{
		StringBuilder builder = new StringBuilder();
		for (int y = 0; y < height; ++y)
		{
			for (int x = 0; x < width; ++x)
			{
				builder.Append(nodeMarkList[x, y] == 0 ? "0" : "1");
			}
			if (y != height-1) builder.Append("\n");
		}
		
		FileStream fs = new FileStream(savePath, FileMode.Create);
		StreamWriter sw = new StreamWriter(fs, Encoding.ASCII);
		sw.Write(builder.ToString());
		sw.Close();
		fs.Close();
		UnityEngine.Debug.Log("Saved to " + savePath);
	}
}