using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using Lite;
using Lite.AStar;
using Lite.Graph;
using Lite.BevTree;

public class Action1 : Action
{
	protected override RunningState OnTick(Context context)
	{
		UnityEngine.Debug.Log("action 1");
		return RunningState.Success;
	}

}

public class Action2 : Action
{
	protected override RunningState OnTick(Context context)
	{
		UnityEngine.Debug.Log("action 2");
		return RunningState.Success;
	}

}

public class Action3 : Action
{
	protected override RunningState OnTick(Context context)
	{
		UnityEngine.Debug.Log("action 3");
		return RunningState.Success;
	}

}

public class BehaviourTreeTest : MonoBehaviour
{
	BehaviourTree tree;

	void Start()
	{
		tree = new BehaviourTree();
		tree.title = "test tree";
		tree.description = "just for test..";
		tree.root = new Repeater(
			new Selector(
				new Action1(),
				new Action2(),
				new Action3()),
			5);
		tree.context.data = 2333;
	}

	void Update()
	{
		tree.Tick();
	}

	void OnGUI()
	{
		/*if (GUI.Button(new Rect(20, 20, 60, 30), "spawn"))
		{
			
		}

		if (GUI.Button(new Rect(20, 60, 60, 30), "wander"))
		{
			
		}

		if (GUI.Button(new Rect(20, 100, 60, 30), "seek"))
		{
			
		}

		if (GUI.Button(new Rect(20, 140, 60, 30), "arrive"))
		{
			
		}*/

	}

}