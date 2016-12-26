using System.Collections.Generic;


namespace Lite.BevTree
{
	class GuidGen
	{
		public static long NextLong()
		{
			byte[] buffer = System.Guid.NewGuid().ToByteArray();
			return System.BitConverter.ToInt64(buffer, 0);
		}
	}

	class RandomGen
	{
		private static System.Random rnd = new System.Random(System.DateTime.Now.Millisecond);

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
