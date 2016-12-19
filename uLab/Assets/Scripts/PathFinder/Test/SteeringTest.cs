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
		if (GUI.Button(new Rect(20, 20, 100, 30), "spawn"))
		{
			if (testBot == null)
				testBot = AddBot();
		}

		if (GUI.Button(new Rect(20, 20, 100, 30), "wander"))
		{
			testBot.GetKinematic().tur
		}

		if (GUI.Button(new Rect(20, 20, 100, 30), "seek"))
		{

		}

	}

	Agent AddBot()
	{
		var prefab = Resources.Load("prefabs/bot1");
		GameObject go = GameObject.Instantiate(prefab) as GameObject;
		Agent agent = go.AddComponent<Agent>();
		Locomotion loco = go.AddComponent<Locomotion>();

		return agent;
	}

}