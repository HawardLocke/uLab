using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using Lite;
using Lite.AStar;
using Lite.Graph;
using Lite.BevTree;

// test result:
// tree depth is 9, tree tick 100 times per frame, avarage cost ms is under 10, but ranges between 4 and 70...
// But in practice, tree tick sould be much slower..

public class Action1 : Action
{
	protected override RunningStatus OnTick(Context context)
	{
		return (RunningStatus)MathUtil.RandInt(1, 2);// RunningStatus.Success;
	}

}

public class Action2 : Action
{
	protected override RunningStatus OnTick(Context context)
	{
		return (RunningStatus)MathUtil.RandInt(1, 2);
	}

}

public class Action3 : Action
{
	protected override RunningStatus OnTick(Context context)
	{
		return (RunningStatus)MathUtil.RandInt(1, 2);
	}

}

public class BehaviourTreeTest : MonoBehaviour
{
	BehaviourTree tree;

	Lite.BevTree.Blackboard blackboard;

	Context context;

	

	void Start()
	{
		BehaviourNode root = new Repeater(-1,
			new Parallel(
				new Parallel(
				new Sequence(
						new Action1(),
						new Sequence(
							new Sequence(new Sequence(new Sequence(new Sequence(new Action1(),new Action2(),new Action3())))),
							new Sequence(new Sequence(new Sequence(new Sequence(new Action1(),new Action2(),new Action3()))))
							)
						),
				new Sequence(
						new Action1(),
						new Sequence(
							new Sequence(new Sequence(new Sequence(new Sequence(new Action1(), new Action2(), new Action3())))),
							new Sequence(new Sequence(new Sequence(new Sequence(new Action1(), new Action2(), new Action3()))))
							)
						),
					new Parallel(
				new Sequence(
						new Action1(),
						new Selector(
							new Action2(),
							new Action3()
							)
						),
				new Sequence(
						new Action1(),
						new Sequence(
							new Sequence(new Sequence(new Sequence(new Sequence(new Action1(), new Action2(), new Action3())))),
							new Sequence(new Sequence(new Sequence(new Sequence(new Action1(), new Action2(), new Action3()))))
							)
						)
				)
					)
				)
			);

		tree = new BehaviourTree();
		tree.title = "test tree";
		tree.description = "just for test..";
		tree.root = root;

		context = new Context();

	}

	private RunningStatus lastRet = RunningStatus.Running;
	string treeDumpText = "";
	long mills = 0;
	Stopwatch watch = new Stopwatch();

	void Update()
	{
		watch.Reset();
		watch.Start();

		if (lastRet == RunningStatus.Running)
		{
			for (int i = 0; i < 100; ++i)
			{
				lastRet = tree.Tick(context);
			}
			treeDumpText = tree.Dump(context);
		}

		watch.Stop();
		mills = watch.ElapsedMilliseconds;
		
	}

	
	void OnGUI()
	{
		GUI.Label(new Rect(100, 20, 500, 20), mills.ToString());
		/*GUI.Label(new Rect(100, 40, 500, 500), treeDumpText);

		if (GUI.Button(new Rect(20, 20, 60, 30), "step"))
		{
			watch.Reset();
			watch.Start();

			for (int i = 0; i < 100; ++i)
			{
				lastRet = tree.Tick(context);
			}
			treeDumpText = tree.Dump(context);

			watch.Stop();
			mills = watch.ElapsedMilliseconds;
		}

		if (GUI.Button(new Rect(20, 60, 60, 30), "dump"))
		{
			treeDumpText = tree.Dump();
		}

		if (GUI.Button(new Rect(20, 100, 60, 30), ""))
		{
			
		}

		if (GUI.Button(new Rect(20, 140, 60, 30), ""))
		{
			
		}*/

	}

}