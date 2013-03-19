using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NeuroEx.Storage.Models
{
	public class Population
	{
		public int Id { get; set; }
		public string ExtIdent { get; set; }  // TODO: Get rid of this
		public List<string> ExternalIds { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public virtual ObservableCollection<Person> People
		{ get { return _people; } }
		private readonly ObservableCollection<Person> _people = new ObservableCollection<Person>();

		public virtual ObservableCollection<Task> Tasks
		{ get { return _tasks; } }
		private readonly ObservableCollection<Task> _tasks = new ObservableCollection<Task>();
	}
}
