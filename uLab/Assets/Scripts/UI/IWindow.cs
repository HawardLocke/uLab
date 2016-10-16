

using UnityEngine;


namespace Locke
{

	public class IWindow : MonoBehaviour
	{
		void Awake()
		{
			OnAwake();
		}

		void Start()
		{
			OnStart();
		}

		void Update()
		{
			OnUpdate();
		}
		public void OnAwake() { }

		public void OnStart() { }

		public void OnUpdate() { }

	}

}