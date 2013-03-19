using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using NeuroEx.Storage;
using NeuroEx.Storage.Models;

namespace NeuroEx.WPF.Views
{
	[Export]
	public class MeasureViewModel
	{
		[ImportingConstructor]
		public MeasureViewModel(INeuroExStorageService researchService)
		{
			_researchService = researchService;
			Populations = _researchService.GetPopulations();
		}

		private readonly INeuroExStorageService _researchService;

		public ObservableCollection<Population> Populations { get; private set; }
		public Population SelectedPopulation { get; set; }
		public Person SelectedPerson { get; set; }
		public Task SelectedTask { get; set; }
	}
}
