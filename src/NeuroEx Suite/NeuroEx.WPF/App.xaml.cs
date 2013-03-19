using System;
using System.Windows;
using NeuroEx.WPF.Utilities;

namespace NeuroEx.WPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

#if (DEBUG)
			RunInDebugMode();
#else
            RunInReleaseMode();
#endif
			ShutdownMode = ShutdownMode.OnMainWindowClose;
		}

		private static void RunInDebugMode()
		{
			var bootstrapper = new NeuroExWPFBootstrapper();
			bootstrapper.Run();
		}

// ReSharper disable UnusedMember.Local
		private static void RunInReleaseMode()
// ReSharper restore UnusedMember.Local
		{
			AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
			try
			{
				var bootstrapper = new NeuroExWPFBootstrapper();
				bootstrapper.Run();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			HandleException(e.ExceptionObject as Exception);
		}

		private static void HandleException(Exception ex)
		{
			if (ex == null)
				return;

			//ExceptionPolicy.HandleException(ex, "Default Policy");
			//MessageBox.Show(StockTraderRI.Properties.Resources.UnhandledException);
			Environment.Exit(1);
		}
	}
}
