using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using Lite;
using Lite.AStar;


public class GridPathTest : MonoBehaviour
{
	GridAStarMap map;
	Point2D[] path = null;
	GridPathPlanner pathFinder;

	public int x1 = 0;
	public int y1 = 0;
	public int x2 = 0;
	public int y2 = 0;

	Texture dotRedTex;

	void Start()
	{
		map = new GridAStarMap();
		map.InitMap(50, 30);
		int width = map.GetWidth();
		int height = map.GetHeight();
		int[,] nodeMarkList = new int[width, height];
		MapEditor.Load(Application.dataPath + "/../map.txt", nodeMarkList);
		for (int x = 0; x < width; ++x)
			for (int y = 0; y < height; ++y)
				map.SetNodePassable(x, y, nodeMarkList[x,y]==0);

		pathFinder = new GridPathPlanner();
		pathFinder.Setup(map);

		x2 = width - 1;
		y2 = height - 1;

		dotRedTex = Resources.Load("Textures/dotRed") as Texture;
	}

	
	void OnGUI()
	{
		if (GUI.Button(new Rect(10, 10, 20, 20), "T"))
		{
			Stopwatch watch = new Stopwatch();
			watch.Start();
			path = pathFinder.FindPath(x1, y1, x2, y2);
			watch.Stop();
			GUI.Label(new Rect(50, 0, 100, 30), "" + watch.ElapsedMilliseconds);
		}

		int gw = 20;
		int gh = 20;
		int w = map.GetWidth();
		int h = map.GetHeight();
		for (int i = 0; i < w; ++i)
		{
			for (int j = 0; j < h; ++j)
			{
				if (!map.IsNodePassable(i, j))
					GUI.Box(new Rect(i * gw + 0.05f * gw, j * gh + 0.05f * gh, 0.9f * gw, 0.9f * gh), "");
			}
		}

		if (path != null)
		{
			for (int i = 0; i < path.Length; ++i)
			{
				//GUI.Box(new Rect(path[i].x * gw + 0.3f * gw, path[i].y * gh + 0.3f * gh, 0.4f * gw, 0.4f * gh), "");
				Graphics.DrawTexture(new Rect(path[i].x * gw + 0.4f * gw, path[i].y * gh + 0.4f * gh, 0.2f * gw, 0.2f * gh), dotRedTex, new Rect(0.0f, 0.0f, 1f, 1f), 0, 0, 0, 0, null);
			}
		}

	}
}