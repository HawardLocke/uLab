
using UnityEngine;

namespace Locke
{
	public class Log
	{
		public static void Info(string text)
		{
			Debug.Log(text);
		}

		public static void Warning(string text)
		{
			Debug.LogWarning(text);
		}

		public static void Error(string text)
		{
			Debug.LogError(text);
		}
	}

}
