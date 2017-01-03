
using System.Collections;
using System.Collections.Generic;

using Lite.Common;


namespace Lite.Goap
{
	
	// collection of context status
	public class ContextStatus
	{
		private int maxStateCount;
		private BitArray stateBits;
		private Dictionary<uint, AtomValue> status;

		public const int stateCount = 64;

		public ContextStatus()
		{
			maxStateCount = stateCount;
			stateBits = new BitArray(stateCount);
			status = new Dictionary<uint, AtomValue>();
		}

		public void Copy(ContextStatus from)
		{

		}

		public void Merge(ContextStatus from)
		{

		}

		public bool IsSame(ContextStatus from)
		{
			return false;
		}

		public bool Contains(ContextStatus other)
		{
			return false;
		}

	}


}