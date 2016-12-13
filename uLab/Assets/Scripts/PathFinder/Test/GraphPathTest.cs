using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using Lite;
using Lite.AStar;


public class GraphPathTest : MonoBehaviour
{
	GraphAStarMap map;
	Point2D[] path = null;
	GraphPathPlanner pathFinder;

	public int startIndex = 0;
	public int endIndex = 0;

	int width = 50;
	int height = 30;

	int gw = 20;
	int gh = 20;

	void Start()
	{
		map = new GraphAStarMap();

		for (int x = 0; x < width; ++x)
		{
			for (int y = 0; y < height; ++y)
			{
				GraphAStarNode node = map.AddNode<GraphAStarNode>();
				node.x = x;
				node.y = y;
			}
		}
		int[,] nodeMarkList = new int[width, height];
		MapEditor.Load(Application.dataPath + "/../map.txt", nodeMarkList);
		for (int x = 0; x < width; ++x)
		{
			for (int y = 0; y < height; ++y)
			{
				GraphAStarNode node = map.AddNode<GraphAStarNode>();
				//map.SetNodePassable(x, y, nodeMarkList[x, y] == 0);
			}
		}

		pathFinder = new GraphPathPlanner();
		pathFinder.Setup(map);

	}

	
	void OnGUI()
	{
		if (GUI.Button(new Rect(10, 10, 20, 20), "T"))
		{
			Stopwatch watch = new Stopwatch();
			watch.Start();
			path = pathFinder.FindPath(startIndex, endIndex);
			watch.Stop();
			GUI.Label(new Rect(50, 0, 100, 30), "" + watch.ElapsedMilliseconds);
		}

		
		for (int i = 0; i < width; ++i)
		{
			for (int j = 0; j < height; ++j)
			{
				//if (!map.IsNodePassable(i, j))
				//	GUI.Box(new Rect(i * gw + 0.05f * gw, j * gh + 0.05f * gh, 0.9f * gw, 0.9f * gh), "");
			}
		}

		if (path != null)
		{
			for (int i = 0; i < path.Length; ++i)
			{
				GUI.Box(new Rect(path[i].x * gw + 0.3f * gw, path[i].y * gh + 0.3f * gh, 0.4f * gw, 0.4f * gh), "");
			}
		}

	}
}