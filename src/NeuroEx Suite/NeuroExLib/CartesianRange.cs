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
	public class CartesianRange
	{
		public CartesianPair MinX = new CartesianPair() { X = Double.MaxValue, Y = 0 };
		public CartesianPair MaxX = new CartesianPair() { X = Double.MinValue, Y = 0 };
		public CartesianPair MinY = new CartesianPair() { X = 0, Y = Double.MaxValue };
		public CartesianPair MaxY = new CartesianPair() { X = 0, Y = Double.MinValue };

		public void UpdatePair(double X, double Y)
		{
			if (X < MinX.X)
				MinX = new CartesianPair(X, Y);
			if (X > MaxX.X)
				MaxX = new CartesianPair(X, Y);
			if (Y < MinY.Y)
				MinY = new CartesianPair(X, Y);
			if (Y > MaxY.Y)
				MaxY = new CartesianPair(X, Y);
		}
	}

	public class CartesianPair
	{
		public double X;
		public double Y;

		public CartesianPair() { }

		public CartesianPair(double x, double y)
		{
			X = x;
			Y = y;
		}
	}
}
