
using System;
using System.IO;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;


namespace Lite
{
	[Serializable]
	public class GoToRequest : Request
	{
		public int ID;
	}

}