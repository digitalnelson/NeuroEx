using System.Collections.Generic;

namespace NeuroEx.Storage.Models
{
	public class Person
	{
		public string Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<Identifier> ExternalIds { get; set; }
		public List<string> Groups { get; set; }
		//public virtual ICollection<Group> Groups { get; set; }

		public ICollection<Population> Populations { get; set; }
	}
}
