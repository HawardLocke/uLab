using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using Lite;
using Lite.AStar;
using Lite.Graph;


public class SteeringTest : MonoBehaviour
{
	KinematicAgent bot1 = null;

	KinematicAgent bot2 = null;

	KinematicAgent bot3 = null;

	void OnGUI()
	{
		if (GUI.Button(new Rect(20, 20, 60, 30), "spawn"))
		{
			if (bot1 == null)
				bot1 = AddBot();
		}

		if (GUI.Button(new Rect(20, 60, 60, 30), "wander"))
		{
			bool isOn = bot1.GetSteering().IsSteeringOn(SteeringType.Wander);
			bot1.GetSteering().TurnSteeringOn(SteeringType.Wander, !isOn);
		}

		if (GUI.Button(new Rect(20, 100, 60, 30), "seek"))
		{
			bot1.GetKinematic().targetPosition = new Vector3(0, 1, 0);
			bool isOn = bot1.GetSteering().IsSteeringOn(SteeringType.Seek);
			bot1.GetSteering().TurnSteeringOn(SteeringType.Seek, !isOn);
		}

		if (GUI.Button(new Rect(20, 140, 60, 30), "arrive"))
		{
			bot1.GetKinematic().targetPosition = new Vector3(0, 1, 0);
			bool isOn = bot1.GetSteering().IsSteeringOn(SteeringType.Arrive);
			bot1.GetSteering().TurnSteeringOn(SteeringType.Arrive, !isOn);
		}

	}

	void Start()
	{
		KinematicFacade.Instance.Init();
	}

	KinematicAgent AddBot()
	{
		KinematicAgent kinAgent = new KinematicAgent(GuidGenerator.NextLong());
		KinematicFacade.Instance.AddAgent(kinAgent);

		var prefab = Resources.Load("Prefabs/Bot1");
		GameObject go = GameObject.Instantiate(prefab) as GameObject;
		AgentComponent agentCom = go.AddComponent<AgentComponent>();
		agentCom.agent = kinAgent;
		kinAgent.agentComponent = agentCom;
		SteeringComponent steer = go.AddComponent<SteeringComponent>();
		LocomotionComponent loco = go.AddComponent<LocomotionComponent>();

		float x = MathUtil.RandFloat() * 10;
		float y = 1;
		float z = MathUtil.RandFloat() * 10;
		loco.SetPosition(x, y, z);

		return kinAgent;
	}


}