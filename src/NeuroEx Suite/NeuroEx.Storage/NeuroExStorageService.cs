using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using NeuroEx.Storage.Models;

namespace NeuroEx.Storage
{
	public interface INeuroExStorageService
	{
		ObservableCollection<Population> GetPopulations();
		Population AddPopulation(Population pop);
		void DelPopulation(Population pop);
		void DelPerson(Person person);
		void DelTask(Task task);
		void Save();
	}

	[Export(typeof(INeuroExStorageService))]
	public class NeuroExStorageServiceEF : INeuroExStorageService
	{
		private readonly NeuroExStorageRepo _repo;

		public NeuroExStorageServiceEF()
		{
			Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
			//Database.SetInitializer<NeuroExStorageRepo>(new NeuroExStorageRepoInitializer());

			_repo = new NeuroExStorageRepo();
		}

		public ObservableCollection<Population> GetPopulations()
		{
			_repo.Populations.Load();
			return _repo.Populations.Local;
		}

		public Population AddPopulation(Population pop)
		{
			_repo.Populations.Add(pop);
			_repo.SaveChanges();
			return pop;
		}
		public void DelPopulation(Population pop)
		{
			_repo.Populations.Remove(pop);
			_repo.SaveChanges();
		}

		public void DelPerson(Person person)
		{
			foreach (Population pop in person.Populations)
				pop.People.Remove(person);
				
			_repo.People.Remove(person);
			_repo.SaveChanges();
		}

		public void DelTask(Task task)
		{
			foreach (Population pop in task.Populations)
				pop.Tasks.Remove(task);

			_repo.Tasks.Remove(task);
			_repo.SaveChanges();
		}

		public void Save()
		{
			_repo.SaveChanges();
		}
	}

	class NeuroExStorageRepo : DbContext
	{
		public DbSet<Population> Populations { get; set; }
		public DbSet<Person> People { get; set; }
		public DbSet<Task> Tasks { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Task>().Property(p => p.Instructions).HasMaxLength(500);
		}
	}

	class NeuroExStorageRepoInitializer : DropCreateDatabaseAlways<NeuroExStorageRepo>
	{
		protected override void Seed(NeuroExStorageRepo context)
		{
			base.Seed(context);

			_populations.Values.ToList().ForEach(p => context.Populations.Add(p));

			foreach (PersonExample p in _people)
			{
				p.Populations = new List<Population>();

				foreach (string popId in p.PopulationIds)
				{
					Population pop = _populations[popId];
					p.Populations.Add(pop);
				}

				context.People.Add(p);
			}

			foreach(TaskExample te in _tasks)
			{
				Population pop = _populations[te.StudyId];
				pop.Tasks.Add(te);
			}

			_tasks.ToList().ForEach(t => context.Tasks.Add(t));
			context.SaveChanges();
		}

		private readonly Dictionary<string, Population> _populations = new Dictionary<string, Population> {
			{"169", new Population {Name = "Sz White Matter", Description = "A R01 sponsored white matter study of schizophrenia."} },
			{"188", new Population {Name = "Cocaine", Description = "CSIA sponsored examination of addiction connectivity."} }
		};

		private readonly List<Person> _people = new List<Person> {
			new PersonExample { Id = "3111", Name = "John Smith", Groups = new List<string> {"probands", "firstepisode"}, PopulationIds = new List<string>{"169"} },
			new PersonExample { Id = "3112", Name = "Joe Smith", Groups = new List<string> {"controls", "firstepisode"}, PopulationIds = new List<string>{"169", "188"}},
			new PersonExample { Id = "4211", Name = "Bill Smith", Groups = new List<string> {"probands", "firstepisode"}, PopulationIds = new List<string>{"188"}},
			new PersonExample { Id = "4212", Name = "Bob Smith", Groups = new List<string> {"controls", "firstepisode"}, PopulationIds = new List<string>{"188"}}
		};

		private readonly List<Task> _tasks = new List<Task> {
			new TaskExample { StudyId = "169", Name = "Eyes Closed", Seconds = 60, Instructions = "Place feet together and close you eyes.  Try to remain as still as possible."},
			new TaskExample { StudyId = "169", Name = "Inspection", Seconds = 60, Instructions = "Place feet together and keep your eyes focused on the sheet of paper.  Try to remain as still as possible."},
			new TaskExample { StudyId = "169", Name = "Search", Seconds = 60, Instructions = "Place feet together and count the number of occurences of the letter S in the paragraph on the wall.  Try to remain as still as possible."}
		};
	}

	class PersonExample : Person
	{
		public List<string> PopulationIds { get; set; }
	}
	class TaskExample : Task
	{
		public string StudyId { get; set; }
	}
}
