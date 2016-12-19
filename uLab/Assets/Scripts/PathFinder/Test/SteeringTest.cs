using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using Lite;
using Lite.AStar;
using Lite.Graph;


public class SteeringTest : MonoBehaviour
{
	Agent testBot = null;

	void OnGUI()
	{
		if (GUI.Button(new Rect(20, 20, 60, 30), "spawn"))
		{
			if (testBot == null)
				testBot = AddBot();
		}

		if (GUI.Button(new Rect(20, 60, 60, 30), "wander"))
		{
			bool isOn = testBot.GetSteering().IsSteeringOn(SteeringType.Wander);
			testBot.GetSteering().TurnSteeringOn(SteeringType.Wander, !isOn);
		}

		if (GUI.Button(new Rect(20, 100, 60, 30), "seek"))
		{
			testBot.GetKinematic().targetPosition = new Vector3(0, 1, 0);
			bool isOn = testBot.GetSteering().IsSteeringOn(SteeringType.Seek);
			testBot.GetSteering().TurnSteeringOn(SteeringType.Seek, !isOn);
		}

	}

	Agent AddBot()
	{
		var prefab = Resources.Load("Prefabs/Bot1");
		GameObject go = GameObject.Instantiate(prefab) as GameObject;
		Agent agent = go.AddComponent<Agent>();
		SteeringComponent steer = go.AddComponent<SteeringComponent>();
		KinematicComponent kinm = go.AddComponent<KinematicComponent>();
		Locomotion loco = go.AddComponent<Locomotion>();

		float x = MathUtil.RandFloat() * 10;
		float y = 1;
		float z = MathUtil.RandFloat() * 10;
		kinm.SetPosition(x, y, z);

		return agent;
	}


}