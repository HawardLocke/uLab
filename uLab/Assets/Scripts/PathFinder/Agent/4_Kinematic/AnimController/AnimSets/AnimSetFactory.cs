

using System.Collections.Generic;


namespace Lite.Anim
{

	public class AnimSetFactory
	{
		private static Dictionary<uint, AnimSet> animSetDic = new Dictionary<uint, AnimSet>();

		public static AnimSet GetAnimSet(uint id)
		{
			AnimSet animSet = null;
			animSetDic.TryGetValue(id, out animSet);
			if (animSet == null)
			{
				animSet = CreateAnimSet(id);
				animSetDic.Add(id, animSet);
			}
			return animSet;
		}

		private static AnimSet CreateAnimSet(uint id)
		{
			AnimSet animSet = null;

			switch (id)
			{
				case 0:
				case 1:
					animSet = new AnimSetSimple();
					break;
				default:
					animSet = null;
					break;
			}

			return animSet;
		}

	}

}