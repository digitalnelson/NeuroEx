using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

namespace AgileMedicine.MovementStudioForms
{
	public class TimeManager
	{
		public static long GetTicks()
		{
			return Stopwatch.GetTimestamp();
		}
	}
}
