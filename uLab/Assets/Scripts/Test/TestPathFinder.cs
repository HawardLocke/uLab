using UnityEngine;
using System.Collections;
using System.Diagnostics;

using Lite;
using AStar;


public class TestPathFinder : MonoBehaviour
{
	GridMap map;
	Point2D[] path = null;
	GridPathPlanner pathFinder;

	void Start()
	{
		map = new GridMap();
		map.BuildMap();
		pathFinder = new GridPathPlanner();
		pathFinder.Setup(map);

		/*Stopwatch watch = new Stopwatch();
		watch.Start();
		for (int i = 0; i < 1; ++i)
			path = pathFinder.FindPath(0, 0, 99, 99);
		watch.Stop();
		UnityEngine.Debug.LogError("cost/ms : " + watch.ElapsedMilliseconds);*/
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(10, 10, 20, 20), "T"))
		{
			System.Random rand = new System.Random(System.DateTime.Now.Millisecond);
			int x1 = rand.Next(0, 48) * 2 + 1;
			int y1 = rand.Next(0, 48) * 2 + 1;
			int x2 = rand.Next(0, 48) * 2 + 1;
			int y2 = rand.Next(0, 48) * 2 + 1;
			path = pathFinder.FindPath(x1, y1, x2, y2);
		}

		Stopwatch watch = new Stopwatch();
		watch.Start();
		for (int i = 0; i < 1; ++i)
			path = pathFinder.FindPath(0, 0, 29, 29);
		watch.Stop();
		//GUI.Label(new Rect(50, 0, 100, 30), "" + watch.ElapsedMilliseconds);

		int gw = 20;
		int gh = 20;
		int w = map.GetWidth();
		int h = map.GetHeight();
		for (int i = 0; i < w; ++i)
		{
			for (int j = 0; j < h; ++j)
			{
				var node = map.GetNode(i, j);
				if (node.blockValue > 0.9)
					GUI.Box(new Rect(i * gw + 0.05f * gw, j * gh + 0.05f * gh, 0.9f * gw, 0.9f * gh), "");
			}
		}

		if (path != null)
		{
			for (int i = 0; i < path.Length; ++i)
			{
				GUI.Box(new Rect(path[i].x * gw + 0.3f * gw, path[i].y * gh + 0.3f * gh, 0.4f * gw, 0.4f * gh), "");
			}
		}

		GUI.Label(new Rect(50, 0, 100, 30), "" + watch.ElapsedMilliseconds);
	}
}