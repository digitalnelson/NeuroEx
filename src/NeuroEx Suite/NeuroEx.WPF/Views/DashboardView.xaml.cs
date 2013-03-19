using NeuroEx.WPF.Behaviors;
using System.ComponentModel.Composition;

namespace NeuroEx.WPF.Views
{
	[ViewExport(RegionName = RegionNames.MainManagerRegion)]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	public partial class DashboardView
	{
		public DashboardView()
		{
			InitializeComponent();
		}

		[Import]
		public DashboardViewModel ViewModel { set { DataContext = value; } }
	}
}
