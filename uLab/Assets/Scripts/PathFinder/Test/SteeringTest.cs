using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using Lite;
using Lite.AStar;
using Lite.Graph;


public class SteeringTest : MonoBehaviour
{
	TestAppFacade app;

	long bot1_id;

	long bot2_id;

	long bot3_id;

	string[] botFilePath = { "Prefabs/Bot1", "Prefabs/Bot2", "Prefabs/Bot3" };

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
		if (GUI.Button(new Rect(20, 20, 60, 30), "spawn"))
		{
			if (bot1_id == 0)
				bot1_id = AddBot().Guid;
			else if (bot2_id == 0)
				bot2_id = AddBot().Guid;
			else if (bot3_id == 0)
				bot3_id = AddBot().Guid;
		}

		if (GUI.Button(new Rect(20, 60, 60, 30), "wander"))
		{
			KinematicAgent kinAgent = app.kinematicFacade.FindAgent(bot1_id);
			bool isOn = kinAgent.GetSteering().IsSteeringOn(SteeringType.Wander);
			kinAgent.GetSteering().TurnSteeringOn(SteeringType.Wander, !isOn);
		}

		if (GUI.Button(new Rect(20, 100, 60, 30), "seek"))
		{
			KinematicAgent kinAgent = app.kinematicFacade.FindAgent(bot1_id);
			kinAgent.GetKinematic().targetPosition = new Vector3(0, 1, 0);
			bool isOn = kinAgent.GetSteering().IsSteeringOn(SteeringType.Seek);
			kinAgent.GetSteering().TurnSteeringOn(SteeringType.Seek, !isOn);
		}

		if (GUI.Button(new Rect(20, 140, 60, 30), "arrive"))
		{
			KinematicAgent kinAgent = app.kinematicFacade.FindAgent(bot1_id);
			kinAgent.GetKinematic().targetPosition = new Vector3(0, 1, 0);
			bool isOn = kinAgent.GetSteering().IsSteeringOn(SteeringType.Arrive);
			kinAgent.GetSteering().TurnSteeringOn(SteeringType.Arrive, !isOn);
		}

	}

	KinematicAgent AddBot()
	{
		KinematicAgent kinAgent = new KinematicAgent(GuidGenerator.NextLong());
		app.kinematicFacade.AddAgent(kinAgent);

		var prefab = Resources.Load(botFilePath[MathUtil.RandInt(0,2)]);
		GameObject go = GameObject.Instantiate(prefab) as GameObject;
		AgentComponent agentCom = go.AddComponent<AgentComponent>();
		agentCom.agent = kinAgent;
		kinAgent.agentComponent = agentCom;
		/*SteeringComponent steer = */go.AddComponent<SteeringComponent>();
		LocomotionComponent loco = go.AddComponent<LocomotionComponent>();

		float x = MathUtil.RandFloat() * 10;
		float y = 1;
		float z = MathUtil.RandFloat() * 10;
		loco.SetPosition(x, y, z);

		return kinAgent;
	}

	void OnPostRender()
	{
		GL.LoadOrtho();
		GL.PushMatrix();
		lineMat.SetPass(0);
		GL.LoadPixelMatrix();
		GL.Color(Color.yellow);
		GL.Begin(GL.LINES);
		GL.Vertex3(0, 0, 0);
		GL.Vertex3(1, 1, 0);
		GL.End();
		GL.PopMatrix();
	}

	/*void OnDrawGizmos()
	{
		Vector3[] points = {new Vector3(0,0,0), new Vector3(10,10,10)};
		GizmoTool.DrawLine(points, Color.cyan);
	}*/


}