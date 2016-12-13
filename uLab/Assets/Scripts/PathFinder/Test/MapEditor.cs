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
	int nodeCount = 0;
	int edgeCount = 0;

	Texture lineTex;
	Texture dotBlueTex;
	Texture dotRedTex;

	private int gridWidth = 20;
	private int gridHeight = 20;

	private int stepx = 20;
	private int stepy = 20;

	int offsetX = 120;
	int offsetY = 10;
	int gw = 20;
	int gh = 20;

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
		lineTex = Resources.Load("Textures/line") as Texture;
		dotBlueTex = Resources.Load("Textures/dotBlue") as Texture;
		dotRedTex = Resources.Load("Textures/dotRed") as Texture;
	}

	
	void OnGUI()
	{
		if (Event.current.type.Equals(EventType.Repaint))
		{
			if (floodMode)
			{
				DrawBlock();
				DrawGraph();
			}
			else
			{
				DrawEditorMode();
			}
		}

		GUI.Label(new Rect(5, 10, 100, 20), "node " + nodeCount);
		GUI.Label(new Rect(5, 30, 100, 20), "edge " + edgeCount);

		if (GUI.Button(new Rect(5, 110, 40, 20), "save"))
		{
			floodMode = false;
			Save();
		}
		if (GUI.Button(new Rect(5, 135, 40, 20), "fill"))
		{
			floodMode = true;
			Fill();
		}

	}

	#region editor

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

	#endregion


	#region generate Graph

	void Fill()
	{
		int seedx = 0;
		int seedy = 0;
		graph = new GraphAStarMap();
		DoFlood(graph, seedx, seedy);

		nodeCount = graph.GetNodeCount();
		edgeCount = 0;
		var list = graph.GetNodeList();
		for (int i = 0; i < list.Count; ++i)
		{
			edgeCount += graph.GetEdgeList(list[i].id).Count;
		}
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
		
		int x1 = x - stepx;
		int y1 = y;
		int x2 = x;
		int y2 = y + stepy;
		int x3 = x + stepx;
		int y3 = y;
		int x4 = x;
		int y4 = y - stepy;

		TryAddEdge(node, x1, y1, 10);
		TryAddEdge(node, x2, y2, 10);
		TryAddEdge(node, x3, y3, 10);
		TryAddEdge(node, x4, y4, 10);
		TryAddEdge(node, x1, y4, 14);
		TryAddEdge(node, x1, y2, 14);
		TryAddEdge(node, x3, y2, 14);
		TryAddEdge(node, x3, y4, 14);

		DoFlood(graph, x1, y1);
		DoFlood(graph, x2, y2);
		DoFlood(graph, x3, y3);
		DoFlood(graph, x4, y4);
	}

	private bool IsInBlock(int posx, int posy)
	{
		int x = posx / gridWidth;
		int y = posy / gridHeight;
		if (x < 0 || x >= width || y < 0 || y >= 1)
			return true;
		return nodeMarkList[x,y] == 1;
	}

	private void TryAddEdge(GraphAStarNode node, int x, int y, int cost)
	{
		GraphAStarNode neighbour = graph.GetNodeAt(x, y);
		if (neighbour != null)
		{
			graph.AddEdge(node.id, neighbour.id, cost);
			graph.AddEdge(neighbour.id, node.id, cost);
		}
	}

	#endregion

	#region draw GUI

	void DrawEditorMode()
	{
		for (int x = 0; x < width; ++x)
		{
			for (int y = 0; y < height; ++y)
			{
				if (GUI.Button(new Rect(offsetX + x * gw + 0.05f * gw, offsetY + y * gh + 0.05f * gh, 0.9f * gw, 0.9f * gh), nodeMarkList[x, y] == 0 ? "" : "1"))
				{
					nodeMarkList[x, y] = nodeMarkList[x, y] == 0 ? 1 : 0;
					}
			}
		}
	}

	void DrawBlock()
	{
		for (int x = 0; x < width; ++x)
		{
			for (int y = 0; y < height; ++y)
			{
				if (nodeMarkList[x, y] == 1)
					GUI.Button(new Rect(offsetX + x * gw + 0.05f * gw, offsetY + y * gh + 0.05f * gh, 0.9f * gw, 0.9f * gh), "");
			}
		}
	}

	void DrawGraph()
	{
		var list = graph.GetNodeList();
		for (int i = 0; i < list.Count; ++i)
		{
			GraphAStarNode node = list[i] as GraphAStarNode;

			List<GraphEdge> edges = graph.GetEdgeList(node.id);
			for (int e = 0; e < edges.Count; ++e)
			{
				GraphEdge edge = edges[e];
				GraphAStarNode toNode = graph.GetNodeByID(edge.to) as GraphAStarNode;
				int dx = toNode.x - node.x;
				int dy = toNode.y - node.y;
				if (dx < 0 && dy < 0)
					Graphics.DrawTexture(new Rect(offsetX + toNode.x, offsetY + node.y, stepx, stepy), lineTex, new Rect(0.5f, 0.0f, 0.5f, 0.5f), 0, 0, 0, 0, null);
				else if (dx < 0 && dy == 0)
					Graphics.DrawTexture(new Rect(offsetX + toNode.x, offsetY + toNode.y, stepx, stepx), lineTex, new Rect(0.0f, 0.5f, 0.5f, 0.5f), 0, 0, 0, 0, null);
				else if (dx < 0 && dy > 0)
					Graphics.DrawTexture(new Rect(offsetX + toNode.x, offsetY + toNode.y, stepx, stepx), lineTex, new Rect(0.0f, 0.0f, 0.5f, 0.5f), 0, 0, 0, 0, null);
				else if (dx == 0 && dy > 0)
					Graphics.DrawTexture(new Rect(offsetX + toNode.x, offsetY + toNode.y, stepx, stepx), lineTex, new Rect(0.5f, 0.5f, 0.5f, 0.5f), 0, 0, 0, 0, null);
				else if (dx > 0 && dy > 0)
					Graphics.DrawTexture(new Rect(offsetX + node.x, offsetY + node.y, stepx, stepx), lineTex, new Rect(0.5f, 0.0f, 0.5f, 0.5f), 0, 0, 0, 0, null);
				else if (dx > 0 && dy == 0)
					Graphics.DrawTexture(new Rect(offsetX + node.x, offsetY + node.y, stepx, stepx), lineTex, new Rect(0.0f, 0.5f, 0.5f, 0.5f), 0, 0, 0, 0, null);
				else if (dx > 0 && dy < 0)
					Graphics.DrawTexture(new Rect(offsetX + node.x, offsetY + node.y, stepx, stepx), lineTex, new Rect(0.0f, 0.0f, 0.5f, 0.5f), 0, 0, 0, 0, null);
				else if (dx == 0 && dy < 0)
					Graphics.DrawTexture(new Rect(offsetX + node.x, offsetY + node.y, stepx, stepx), lineTex, new Rect(0.5f, 0.5f, 0.5f, 0.5f), 0, 0, 0, 0, null);
			}
			Graphics.DrawTexture(new Rect(offsetX + node.x, offsetY + node.y - 2, 4, 4), dotBlueTex, new Rect(0.0f, 0.0f, 1f, 1f), 0, 0, 0, 0, null);
		}
	}

	#endregion
}