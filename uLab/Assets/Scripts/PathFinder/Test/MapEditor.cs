using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using Lite;
using Lite.AStar;
using Lite.Graph;


public class MapEditor : MonoBehaviour
{
	int width = 50;
	int height = 30;
	int[,] nodeMarkList;
	string savePath;
	bool floodMode;
	GraphAStarMap graph;

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
		floodMode = false;
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
				if (floodMode)
				{
					if (nodeMarkList[x, y] == 1)
						GUI.Button(new Rect(offsetX + x * gw + 0.05f * gw, offsetY + y * gh + 0.05f * gh, 0.9f * gw, 0.9f * gh), "1");
				}
				else
				{
					if (GUI.Button(new Rect(offsetX + x * gw + 0.05f * gw, offsetY + y * gh + 0.05f * gh, 0.9f * gw, 0.9f * gh), nodeMarkList[x, y] == 0 ? "" : "1"))
					{
						nodeMarkList[x, y] = nodeMarkList[x, y] == 0 ? 1 : 0;
					}
				}
				
			}
		}

		if (floodMode)
		{
			var list = graph.GetNodeList();
			for (int i = 0; i < list.Count; ++i)
			{
				GraphAStarNode node = list[i] as GraphAStarNode;
				GUI.Box(new Rect(offsetX + node.x, offsetY + node.y, 4, 4), "");
			}
		}

		if (GUI.Button(new Rect(5, 10, 40, 20), "save"))
		{
			floodMode = false;
			Save();
		}
		if (GUI.Button(new Rect(5, 35, 40, 20), "fill"))
		{
			floodMode = true;
			Fill();
		}

		//Graphics.DrawTexture(new Rect(200, 100, 128, 128), tex, new Rect(0.0f, 0.5f, 0.5f, 0.5f), 0, 0, 0, 0, null);
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


	#region Flood Graph

	private int gridWidth = 20;
	private int gridHeight = 20;

	private int stepx = 20;
	private int stepy = 20;

	void Fill()
	{
		int seedx = 0;
		int seedy = 0;
		graph = new GraphAStarMap();
		DoFlood(graph, seedx, seedy);

		UnityEngine.Debug.Log("node count " + graph.GetNodeCount());
	}

	private void DoFlood(GraphAStarMap graph, int x, int y)
	{
		if (IsInBlock(x,y))
			return;
		GraphAStarNode node = graph.GetNodeAt(x, y);
		if (node != null)
			return;
		node = graph.AddNode<GraphAStarNode>();
		node.x = x;
		node.y = y;
		// 4 dir
		int x1 = x - stepx;
		int y1 = y;
		GraphAStarNode neighbour = graph.GetNodeAt(x, y);
		if (neighbour != null)
		{
			graph.AddEdge(node.id, neighbour.id, 10);
		}
		DoFlood(graph, x1, y1);
		int x2 = x;
		int y2 = y + stepy;
		DoFlood(graph, x2, y2);
		int x3 = x + stepx;
		int y3 = y;
		DoFlood(graph, x3, y3);
		int x4 = x;
		int y4 = y - stepy;
		DoFlood(graph, x4, y4);
	}

	private bool IsInBlock(int posx, int posy)
	{
		int x = posx / gridWidth;
		int y = posy / gridHeight;
		if (x < 0 || x >= width || y < 0 || y >= height)
			return true;
		return nodeMarkList[x,y] == 1;
	}

	#endregion

}