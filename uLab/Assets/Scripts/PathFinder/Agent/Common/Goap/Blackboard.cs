
using System.Collections.Generic;


namespace Lite.Common
{

	public class Blackboard
	{
		private Dictionary<string,AtomValue> valueDic = new Dictionary<string,AtomValue>();

		private AtomValue Find(string key)
		{
			AtomValue atom;
			valueDic.TryGetValue(key, out atom);
			return atom;
		}

		public int GetInt(string key)
		{
			IntValue atom = (IntValue)this.Find(key);
			return atom != null ? atom.ToInt() : 0;
		}

		public void SetInt(string key, int value)
		{
			IntValue atom = (IntValue)this.Find(key);
			if (atom == null)
			{
				atom = new IntValue();
				valueDic.Add(key, atom);
			}
			atom.SetInt(value);
		}

	}

}