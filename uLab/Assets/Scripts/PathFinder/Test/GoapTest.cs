using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using Lite;
using Lite.Strategy;
using Lite.Goap;


public class GoapTest : MonoBehaviour
{
	TestAppFacade app;
	Agent blacksmith;
	Agent logger;
	Agent miner;
	Agent woodCutter;

	void Start()
	{
		app = new TestAppFacade();
		app.Init();

		blacksmith = new Agent(GuidGenerator.NextLong(), Career.Blacksmith);
		app.stgAgentManager.AddAgent(blacksmith);

		logger = new Agent(GuidGenerator.NextLong(), Career.Logger);
		app.stgAgentManager.AddAgent(logger);

		miner = new Agent(GuidGenerator.NextLong(), Career.Miner);
		app.stgAgentManager.AddAgent(miner);

		woodCutter = new Agent(GuidGenerator.NextLong(), Career.WoodCutter);
		app.stgAgentManager.AddAgent(woodCutter);

	}

	void Update()
	{
		app.Update((long)(Time.timeSinceLevelLoad*1000));
	}

	/*long mills = 0;
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

	}*/

}