

using System;
using System.Collections;

namespace Locke
{

	public class UIContextManager : Singleton<UIContextManager>
	{

		private Stack contexts = new Stack();

		public void Push(UIType ut)
		{

		}

		public IUIContext Pop()
		{
			return null;
		}

	}

}