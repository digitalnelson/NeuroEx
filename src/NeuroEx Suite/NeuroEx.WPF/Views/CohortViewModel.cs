using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using NeuroEx.Storage.Models;
using NeuroEx.Storage;

namespace NeuroEx.WPF.Views
{
	[Export]
	public class CohortViewModel
	{
		[ImportingConstructor]
		public CohortViewModel(INeuroExStorageService researchService)
		{
			_researchService = researchService;

			_saveCommand = new DelegateCommand(Save);
			_addPopulationCommand = new DelegateCommand(AddPopulation);
			_delPopulationCommand = new DelegateCommand(DelPopulation);
			_addPersonCommand = new DelegateCommand(AddPerson);
			_delPersonCommand = new DelegateCommand(DelPerson);
			_addTaskCommand = new DelegateCommand(AddTask);
			_delTaskCommand = new DelegateCommand(DelTask);

			LoadPopulations();
		}

		public void Save()
		{
			_researchService.Save();
		}

		public void LoadPopulations()
		{
			Populations = _researchService.GetPopulations();
		}

		public void AddPopulation()
		{
			_researchService.AddPopulation(new Population());
		}
		public void DelPopulation()
		{
			if (SelectedPopulation != null)
				_researchService.DelPopulation(SelectedPopulation);
		}

		public void AddPerson()
		{
			if(SelectedPopulation != null)
				SelectedPopulation.People.Add(new Person());
		}
		public void DelPerson()
		{
			if (SelectedPerson != null)
				_researchService.DelPerson(SelectedPerson);
		}

		public void AddTask()
		{
			if (SelectedPopulation != null)
				SelectedPopulation.Tasks.Add(new Task());
		}
		public void DelTask()
		{
			if (SelectedTask != null)
				_researchService.DelTask(SelectedTask);
		}

		public ICommand SaveCommand { get { return _saveCommand; } }
		private readonly ICommand _saveCommand;

		public ICommand AddPopulationCommand { get { return _addPopulationCommand; } }
		private readonly ICommand _addPopulationCommand;

		public ICommand DelPopulationCommand { get { return _delPopulationCommand; } }
		private readonly ICommand _delPopulationCommand;

		public ICommand AddPersonCommand { get { return _addPersonCommand; } }
		private readonly ICommand _addPersonCommand;

		public ICommand DelPersonCommand { get { return _delPersonCommand; } }
		private readonly ICommand _delPersonCommand;

		public ICommand AddTaskCommand { get { return _addTaskCommand; } }
		private readonly ICommand _addTaskCommand;

		public ICommand DelTaskCommand { get { return _delTaskCommand; } }
		private readonly ICommand _delTaskCommand;

		private readonly INeuroExStorageService _researchService;

		public ObservableCollection<Population> Populations { get; private set; }
		public Population SelectedPopulation { get; set; }
		public Person SelectedPerson { get; set; }
		public Task SelectedTask { get; set; }
	}
}
