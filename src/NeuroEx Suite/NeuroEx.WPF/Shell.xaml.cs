using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace NeuroEx.WPF
{
	[Export]
	public partial class Shell : IPartImportsSatisfiedNotification
	{
		public Shell()
		{
			InitializeComponent();
		}
		
		public void OnImportsSatisfied()
		{
			RegionManager.RequestNavigate(RegionNames.MainManagerRegion, new Uri(ViewNames.DashboardView, UriKind.Relative));
		}

		[Import]
		public ShellViewModel ViewModel { set { DataContext = value; } }

		[Import(AllowRecomposition = false)]
		public IModuleManager ModuleManager;

		[Import(AllowRecomposition = false)]
		public IRegionManager RegionManager;
	}
}
