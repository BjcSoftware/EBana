using System.Windows;
using System.Windows.Threading;
using EBana.WindowsServices.Dialog;
using EBana.Services.Dialog;
using EBana.WpfUI.Views;
using FirstFloor.ModernUI.Windows.Controls;
using EBana.WpfUI.Core;
using EBana.PresentationLogic.Core;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace EBana
{
	public partial class App : Application
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ModernWindow mainWindow;
        private INavigationService navigator;

		protected override void OnStartup(StartupEventArgs e)
		{
			SetupLogger();

			base.OnStartup(e);

            mainWindow = new Window1();
            navigator = new NavigationService(mainWindow);
            var composer = new PageComposer(navigator);
            mainWindow.ContentLoader = new MyContentLoader(composer);
            mainWindow.Show();

            navigator.NavigateTo("MainMenu");
		}

        private void SetupLogger()
		{
			log4net.Config.XmlConfigurator.Configure();
			DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
		}

		private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			ProcessUnhandledException(e);
		}

		private void ProcessUnhandledException(DispatcherUnhandledExceptionEventArgs e)
		{
			log.Fatal(e.Exception);
			NotifyUserUnhandledExceptionOccurred();
			Shutdown();
		}

		private static void NotifyUserUnhandledExceptionOccurred()
		{
			var messageBoxService = new MessageBoxDialogService();
			messageBoxService.Show(
				"Petit problème", 
				"Un problème a eu lieu et n'a pas pu être géré, l'application va se fermer.\nSi le problème persiste, veuillez contacter la personne en charge du développement d'eBana.", 
				DialogButton.Ok);
		}
    }
}