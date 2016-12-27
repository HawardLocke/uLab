using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using Lite;
using Lite.AStar;
using Lite.Graph;
using Lite.Bev;


public class SteeringTest : MonoBehaviour
{
	TestAppFacade app;

	long bot1_id;

	long bot2_id;

	long bot3_id;

	string[] botFilePath = { "Prefabs/Dwarf/dwarf_01", "Prefabs/Dwarf/dwarf_02", "Prefabs/Dwarf/dwarf_03" };

	Material lineMat;

	

	void Start()
	{
		app = new TestAppFacade();
		app.Init();

		lineMat = new Material(Shader.Find("Diffuse"));
		lineMat.color = Color.green;
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(20, 20, 60, 30), "bot"))
		{
			if (bot1_id == 0)
				bot1_id = AddBot().Guid;
			else if (bot2_id == 0)
				bot2_id = AddBot().Guid;
			else if (bot3_id == 0)
				bot3_id = AddBot().Guid;
		}

		if (GUI.Button(new Rect(20, 60, 60, 30), "mvt"))
		{
			KinematicAgent agent = app.kinematicFacade.FindAgent(bot1_id);
			var target = new Vector3(MathUtil.RandFloat() * 15, 0, MathUtil.RandFloat() * 15);
			MoveTo mvt = new MoveTo(target, MoveTo.Speed.Slow);
			agent.PushAction(mvt);
			agent.steering.TurnSteeringOn(SteeringType.Seek, true);
		}

		if (GUI.Button(new Rect(20, 100, 60, 30), "stp"))
		{
		}

		/*if (GUI.Button(new Rect(20, 100, 60, 30), "seek"))
		{
			KinematicAgent kinAgent = app.kinematicFacade.FindAgent(bot1_id);
			kinAgent.GetKinematic().targetPosition = new Vector3(0, 1, 0);
			bool isOn = kinAgent.steering.IsSteeringOn(SteeringType.Seek);
			kinAgent.steering.TurnSteeringOn(SteeringType.Seek, !isOn);
		}

		if (GUI.Button(new Rect(20, 140, 60, 30), "arrive"))
		{
			KinematicAgent kinAgent = app.kinematicFacade.FindAgent(bot1_id);
			kinAgent.GetKinematic().targetPosition = new Vector3(0, 1, 0);
			bool isOn = kinAgent.steering.IsSteeringOn(SteeringType.Arrive);
			kinAgent.steering.TurnSteeringOn(SteeringType.Arrive, !isOn);
		}*/

	}

	KinematicAgent AddBot()
	{
		KinematicAgent agent = new KinematicAgent(GuidGenerator.NextLong());
		app.kinematicFacade.AddAgent(agent);

		var prefab = Resources.Load(botFilePath[MathUtil.RandInt(0,2)]);
		GameObject go = GameObject.Instantiate(prefab) as GameObject;
		AgentComponent agentCom = go.AddComponent<AgentComponent>();
		agentCom.agent = agent;
		agent.agentComponent = agentCom;
		var steer = go.AddComponent<SteeringComponent>();
		agent.steering = steer;
		var loco = go.AddComponent<LocomotionComponent>();
		var animCom = go.AddComponent<AnimationComponent>();
		animCom.Init(agent);
		agent.animComponent = animCom;

		float x = MathUtil.RandFloat() * 10;
		float y = 0;
		float z = MathUtil.RandFloat() * 10;
		loco.SetPosition(x, y, z);

		return agent;
	}

}