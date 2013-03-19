using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace NeuroEx.WPF
{
	[Export]
	public class ShellViewModel
	{
		public ShellViewModel()
		{
			_dashboardCommand = new DelegateCommand(ShowDashboard);
			_editPeopleCommand = new DelegateCommand(EditPeople);
			_runTasksCommand = new DelegateCommand(RunTasks);
		}

		public void ShowDashboard()
		{
			RegionManager.RequestNavigate(RegionNames.MainManagerRegion, new Uri(ViewNames.DashboardView, UriKind.Relative));
		}

		public void EditPeople()
		{
			RegionManager.RequestNavigate(RegionNames.MainManagerRegion, new Uri(ViewNames.PeopleMgrView, UriKind.Relative));
		}

		public void RunTasks()
		{
			RegionManager.RequestNavigate(RegionNames.MainManagerRegion, new Uri(ViewNames.TaskRunnerView, UriKind.Relative));
		}

		[Import(AllowRecomposition = false)]
		public IModuleManager ModuleManager;

		[Import(AllowRecomposition = false)]
		public IRegionManager RegionManager;

		public ICommand DashboardCommand { get { return _dashboardCommand; } }
		private readonly ICommand _dashboardCommand;

		public ICommand EditPeopleCommand { get { return _editPeopleCommand; } }
		private readonly ICommand _editPeopleCommand;

		public ICommand RunTasksCommand { get { return _runTasksCommand; } }
		private readonly ICommand _runTasksCommand;
	}
}
