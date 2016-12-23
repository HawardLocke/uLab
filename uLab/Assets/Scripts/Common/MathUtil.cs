
using System;

namespace Lite
{
	public class MathUtil
	{
		public const double PI = Math.PI;

		private static Random rnd = new Random(System.DateTime.Now.Millisecond);

		public static int RandInt(int min, int max)
		{
			return rnd.Next(min, max);
		}

		public static float RandFloat()
		{
			return (rnd.Next(0, int.MaxValue)) / (int.MaxValue + 1.0f);
		}

		public static float RandClamp()
		{
			return RandFloat() - RandFloat();
		}

	}

}