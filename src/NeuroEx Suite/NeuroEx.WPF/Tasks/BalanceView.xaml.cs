using System.ComponentModel.Composition;
using NeuroEx.WPF.Behaviors;

namespace NeuroEx.WPF.Tasks
{
	[ViewExport(RegionName = RegionNames.TaskDetailRegion)]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	public partial class BalanceView
	{
		public BalanceView()
		{
			InitializeComponent();
		}

		[Import]
		public BalanceViewModel ViewModel { set { DataContext = value; } }
	}
}