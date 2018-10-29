using Data.Repository;
using EBana.Models;
using EBana.Security;
using EBana.Security.Hash;
using EBana.ViewModels;
using OsServices.Dialog;
using System.Data.Entity;
using System.Windows.Controls;

namespace EBana.Views
{
	public partial class MaintenanceConnexion : Page
	{
		public MaintenanceConnexion()
		{
			InitializeComponent();
            DataContext = CreateViewModel();
		}

        private MaintenanceConnexionViewModel CreateViewModel()
        {
            IMessageBoxService messageBoxService = new MessageBoxService();
            Authenticator authenticator = CreateAuthenticator();

            return new MaintenanceConnexionViewModel(messageBoxService, authenticator);
        }

        private Authenticator CreateAuthenticator()
        {
            ICredentialsReader credentialsReader = CreateCredentialsReader();
            IHash hash = CreateHash();

            return new Authenticator(credentialsReader, hash);
        }

        private ICredentialsReader CreateCredentialsReader()
        {
            DbContext context = new EBanaContext();
            var efCredentialsReader = new EfReader<Credentials>(context);

            // il faut ici utiliser un Reader sans système de cache au cas où l'utilisateur change de mot de passe et se reconnecte sans quitter l'application entre deux.
            // Si le cache était actif, l'ancien mot de passe resterait en cache et remplacerait le nouveau tant que l'utilisateur ne redémarre pas l'application.
            IReader<Credentials> credentialsReaderWithoutCaching = new EfReaderWithoutCaching<Credentials>(efCredentialsReader);

            var credentialsReader = new CredentialsReader(credentialsReaderWithoutCaching);
            return credentialsReader;
        }

        private IHash CreateHash()
        {
            return new BCryptHash();
        }
    }
}