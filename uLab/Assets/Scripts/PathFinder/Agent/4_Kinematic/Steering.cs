using UnityEngine;
using System.Collections;

public class Steering : MonoBehaviour
{

	public float weight = 1;

	void Start()
	{

	}

	void Update()
	{

	}

	public virtual Vector3 Force()
	{
		Vector3 force = new Vector3();
		return force;
	}

}