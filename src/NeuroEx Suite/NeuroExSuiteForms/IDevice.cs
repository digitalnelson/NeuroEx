using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileMedicine.MovementStudioForms
{
	public interface IDevice
	{
		void Connect();
		void Start();
		void Stop();
		void Disconnect();
		object GetSample();

		void Calibrate();
	}
}
