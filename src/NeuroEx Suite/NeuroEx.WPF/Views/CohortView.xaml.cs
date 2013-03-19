using System.ComponentModel.Composition;
using NeuroEx.WPF.Behaviors;

namespace NeuroEx.WPF.Views
{
	[ViewExport(RegionName = RegionNames.MainManagerRegion)]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	public partial class CohortView
	{
		public CohortView()
		{
			InitializeComponent();
		}

		[Import]
		public CohortViewModel ViewModel { set { DataContext = value; } }
	}
}