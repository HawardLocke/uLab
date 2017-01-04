using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using Lite;
using Lite.Strategy;
using Lite.Goap;


public class GoapPlannerTest : MonoBehaviour
{
	GoapMap graph;
	GoapAction[] path = null;
	GoapAStarPlanner pathFinder;


	void Start()
	{
		graph = new GoapMap();

		pathFinder = new GoapAStarPlanner();
		pathFinder.Setup(graph);

	}

	long mills = 0;
	void OnGUI()
	{
		
		if (GUI.Button(new Rect(10, 10, 40, 20), "T"))
		{
			Stopwatch watch = new Stopwatch();
			watch.Start();
			path = pathFinder.Plan(null);
			watch.Stop();
			mills = watch.ElapsedMilliseconds;
		}
		GUI.Label(new Rect(50, 0, 100, 30), "ms " + mills);

	}

}