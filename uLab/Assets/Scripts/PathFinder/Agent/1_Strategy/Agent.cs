
using UnityEngine;


namespace Lite.Strategy
{
	public class Agent : IAgent
	{
		public enum Career
		{
			Miner,
			Logger,
			WoodCutter,
			Blacksmith
		}

		public Career career
		{
			private set;
			get;
		}

		public Agent(long guid, Career career) :
			base(guid)
		{
			this.career = career;
		}

	}

}