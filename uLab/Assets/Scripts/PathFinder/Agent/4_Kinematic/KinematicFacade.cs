
using System.Collections;
using System.Collections.Generic;


namespace Lite
{
	public class KinematicFacade : LayeredFacade<KinematicAgent>
	{
		private static KinematicFacade _inst;
		public static KinematicFacade Instance
		{
			get
			{
				if (_inst == null)
					_inst = new KinematicFacade();
				return _inst;
			}
			
		}

	}

}
