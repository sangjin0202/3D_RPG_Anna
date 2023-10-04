using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Step12
{

	public class SUtil2
	{
		public int x, y;
		public static int Add(int _x, int _y)
		{
			return _x + _y;
		}

		public static int Add(SUtil2 s)
		{
			return s.x + s.y;
		}
	}
}

