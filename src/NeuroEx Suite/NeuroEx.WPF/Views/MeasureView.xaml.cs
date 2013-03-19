using System.ComponentModel.Composition;
using NeuroEx.WPF.Behaviors;

namespace NeuroEx.WPF.Views
{
	[ViewExport(RegionName = RegionNames.MainManagerRegion)]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	public partial class MeasureView
	{
		public MeasureView()
		{
			InitializeComponent();
		}

		[Import]
		public MeasureViewModel ViewModel { set { DataContext = value; } }
	}
}
