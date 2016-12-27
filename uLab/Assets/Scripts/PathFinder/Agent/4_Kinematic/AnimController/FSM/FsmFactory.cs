

using System.Collections.Generic;


namespace Lite.Anim
{
	public enum FsmType
	{
		Default = 0,
		SimpleFsm,

		Count
	}

	public class FsmFactory
	{
		private static Dictionary<uint, Fsm> fsmDic = new Dictionary<uint, Fsm>();

		public static Fsm GetFsm(FsmType type)
		{
			Fsm fsm = null;
			fsmDic.TryGetValue((uint)type, out fsm);
			if (fsm == null)
			{
				fsm = CreateFsm(type);
				fsmDic.Add((uint)type, fsm);
			}
			return fsm;
		}

		private static Fsm CreateFsm(FsmType type)
		{
			Fsm fsm = null;

			switch (type)
			{
				case FsmType.Default:
				case FsmType.SimpleFsm:
					fsm = new FsmSimple();
					break;
				default:
					fsm = null;
					break;
			}

			return fsm;
		}

	}

}