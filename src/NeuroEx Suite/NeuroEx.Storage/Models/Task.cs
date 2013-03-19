using System.Collections.Generic;

namespace NeuroEx.Storage.Models
{
	public class Task
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Seconds { get; set; }
		public string Instructions { get; set; }

		public ICollection<Population> Populations { get; set; }
	}
}
