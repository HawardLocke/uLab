using UnityEngine;
using System.Collections;

using Lite;
using PathFinder;


public class TestPathFinder : MonoBehaviour
{
	AStarMap map;
	Point2D[] path;
	public Texture tex;

	void Start()
	{
		map = new AStarMap();
		map.Create(20, 20);
		AStarEngine engine = new AStarEngine();
		engine.mapData = map;

		path = engine.Search(19, 0, 7, 11);
		//Debug.LogError("length: " + path.Length + ", " + path.ToString());
	}

	void OnGUI()
	{
		int gw = 25;
		int gh = 25;
		int w = map.width;
		int h = map.height;
		for (int i = 0; i < w; ++i)
		{
			for (int j = 0; j < h; ++j)
			{
				var node = map.nodes[i, j];
				if (node.block > 0.9)
					GUI.Box(new Rect(i * gw + 0.05f * gw, j * gh + 0.05f * gh, 0.9f * gw, 0.9f * gh), "");
			}
		}

		for (int i = 0; i < path.Length; ++i)
		{
			GUI.Box(new Rect(path[i].x * gw + 0.3f * gw, path[i].y * gh + 0.3f * gh, 0.4f * gw, 0.4f * gh), "");
		}
			
	}
}