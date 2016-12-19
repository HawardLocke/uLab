
using UnityEngine;


namespace Lite
{

	public class Agent : MonoBehaviour
	{
		public Blackborad blackboard;

		public Agent()
		{
			blackboard = new Blackborad(this);
		}

		void Update()
		{
			
		}

	}

}