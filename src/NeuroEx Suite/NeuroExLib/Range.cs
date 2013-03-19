using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace NeuroExLib
{
	public class Range
	{
		public float Max = float.MinValue;
		public float Min = float.MaxValue;
		public float Avg = 0;

		private float total = 0;
		private float count = 0;

		public void Update(float val)
		{
			if (val > Max)
				Max = val;
			if (val < Min)
				Min = val;

			total += val;
			count++;

			Avg = total / count;
		}
	}
}
