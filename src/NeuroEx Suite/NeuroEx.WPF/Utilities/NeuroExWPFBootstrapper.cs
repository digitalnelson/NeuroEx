using System;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.Practices.Prism.MefExtensions;
using NeuroEx.WPF.Behaviors;
using NeuroEx.Storage;

namespace NeuroEx.WPF.Utilities
{
    [CLSCompliant(false)]
	public class NeuroExWPFBootstrapper : MefBootstrapper
    {
        protected override void ConfigureAggregateCatalog()
        {
			AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(NeuroExWPFBootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(StorageModule).Assembly));
        }

    	protected override void InitializeShell()
        {
            base.InitializeShell();

#if SILVERLIGHT
            Application.Current.RootVisual = (Shell)this.Shell;            
#else
            Application.Current.MainWindow = (Shell)Shell;
            Application.Current.MainWindow.Show();
#endif
        }

        protected override Microsoft.Practices.Prism.Regions.IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();
            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior)); // This will associate regions and views
            return factory;
        }

        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<Shell>();
        }
    }
}
