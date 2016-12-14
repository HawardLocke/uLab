using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using Lite;
using Lite.AStar;
using Lite.Graph;


public class GraphPathTest : MonoBehaviour
{
	GraphAStarMap graph;
	Point2D[] path = null;
	GraphPathPlanner pathFinder;

	int[,] nodeMarkList;

	public int startID = 0;
	public int endID = 1000;

	int width = 50;
	int height = 30;
	int gw = 20;
	int gh = 20;
	int offsetX = 120;
	int offsetY = 10;

	Texture lineTex;
	Texture dotBlueTex;
	Texture dotRedTex;


	void Start()
	{
		nodeMarkList = new int[width, height];
		MapEditor.LoadMap(Application.dataPath + "/../map.txt", nodeMarkList);

		graph = new GraphAStarMap();
		MapEditor.LoadNav(Application.dataPath + "/../nav.txt", graph);

		pathFinder = new GraphPathPlanner();
		pathFinder.Setup(graph);

		lineTex = Resources.Load("Textures/line") as Texture;
		dotBlueTex = Resources.Load("Textures/dotBlue") as Texture;
		dotRedTex = Resources.Load("Textures/dotRed") as Texture;

	}

	long mills = 0;
	void OnGUI()
	{
		
		if (GUI.Button(new Rect(10, 10, 40, 20), "T"))
		{
			Stopwatch watch = new Stopwatch();
			watch.Start();
			path = pathFinder.FindPath(startID, endID);
			watch.Stop();
			mills = watch.ElapsedMilliseconds;
		}
		GUI.Label(new Rect(50, 0, 100, 30), "ms " + mills);

		DrawBlock();
		DrawGraph();

		if (path != null)
		{
			for (int i = 0; i < path.Length; ++i)
			{
				Rect screenRect = new Rect(offsetX + path[i].x, offsetY + path[i].y, 4, 4);
				Graphics.DrawTexture(screenRect, dotRedTex, new Rect(0.0f, 0.0f, 1f, 1f), 0, 0, 0, 0, null);
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

	private int stepx = 20;
	private int stepy = 20;
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
				int minx = Mathf.Min(toNode.x, node.x) + offsetX;
				int miny = Mathf.Min(toNode.y, node.y) + offsetY;
				Rect screenRect = new Rect(minx, miny, stepx, stepy);
				if (dx < 0 && dy < 0)
					Graphics.DrawTexture(screenRect, lineTex, new Rect(0.0f, 0.0f, 0.49f, 0.49f), 0, 0, 0, 0, null);
				else if (dx < 0 && dy == 0)
					Graphics.DrawTexture(screenRect, lineTex, new Rect(0.0f, 0.5f, 0.49f, 0.49f), 0, 0, 0, 0, null);
				else if (dx < 0 && dy > 0)
					Graphics.DrawTexture(screenRect, lineTex, new Rect(0.5f, 0.0f, 0.49f, 0.49f), 0, 0, 0, 0, null);
				else if (dx == 0 && dy > 0)
					Graphics.DrawTexture(screenRect, lineTex, new Rect(0.5f, 0.5f, 0.49f, 0.49f), 0, 0, 0, 0, null);
				else if (dx > 0 && dy > 0)
					Graphics.DrawTexture(screenRect, lineTex, new Rect(0.0f, 0.0f, 0.49f, 0.49f), 0, 0, 0, 0, null);
				else if (dx > 0 && dy == 0)
					Graphics.DrawTexture(screenRect, lineTex, new Rect(0.0f, 0.5f, 0.49f, 0.49f), 0, 0, 0, 0, null);
				else if (dx > 0 && dy < 0)
					Graphics.DrawTexture(screenRect, lineTex, new Rect(0.5f, 0.0f, 0.49f, 0.49f), 0, 0, 0, 0, null);
				else if (dx == 0 && dy < 0)
					Graphics.DrawTexture(screenRect, lineTex, new Rect(0.5f, 0.5f, 0.49f, 0.49f), 0, 0, 0, 0, null);
			}
			Graphics.DrawTexture(new Rect(offsetX + node.x, offsetY + node.y - 2, 4, 4), dotBlueTex, new Rect(0.0f, 0.0f, 1f, 1f), 0, 0, 0, 0, null);
		}
	}

}