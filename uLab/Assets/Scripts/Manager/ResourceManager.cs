

using System;
using UnityEngine;


namespace Locke
{

	public class ResourceManager : Singleton<ResourceManager>
	{

		public GameObject LoadRes(string pathName)
		{
			var go = Resources.Load(pathName) as GameObject;
			return go;
		}

	}

}