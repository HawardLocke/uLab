
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

		public ContextStatus(int stateCount)
		{
			maxStateCount = stateCount;
			stateBits = new BitArray(stateCount);
			status = new Dictionary<uint, AtomValue>();
		}

	}


}