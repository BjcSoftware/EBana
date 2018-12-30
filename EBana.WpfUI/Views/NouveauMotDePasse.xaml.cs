using EBana.Domain.Security;
using EBana.EfDataAccess;
using EBana.EfDataAccess.Repository;
using EBana.Domain.Models;
using EBana.Security.Hash;
using EBana.WindowsServices.Dialog;
using EBana.WpfUI.ViewModels;
using System.Windows.Controls;
using System.Data.Entity;

namespace EBana.WpfUI.Views
{
    public partial class NouveauMotDePasse : Page
    {
        public NouveauMotDePasse()
        {
            InitializeComponent();
            DataContext = CreateViewModel();
        }

        private NouveauMotDePasseViewModel CreateViewModel()
        {
            return new NouveauMotDePasseViewModel(
                new MessageBoxDialogService(),
                CreateAuthenticator(),
                CreatePasswordUpdater());
        }

        private IAuthenticator CreateAuthenticator()
        {
            return new Authenticator(
                CreateCredentialsReader(),
                CreateHash());
        }

        private ICredentialsReader CreateCredentialsReader()
        {
            // Il faut ici utiliser un Reader sans système de cache au cas où l'utilisateur change de mot de passe et se reconnecte sans quitter l'application entre deux.
            // Si le cache était actif, l'ancien mot de passe resterait en cache et remplacerait le nouveau tant que l'utilisateur ne redémarre pas l'application.
            IReader<Credentials> credentialsReaderWithoutCaching = 
                new EfReaderWithoutCaching<Credentials>(
                    new EfReader<Credentials>(
                        CreateDbContext()));

            return new CredentialsReader(
                    credentialsReaderWithoutCaching);
        }

        private IPasswordUpdater CreatePasswordUpdater()
        {
            return new PasswordUpdater(
                CreateCredentialsUpdater(),
                CreateHash());
        }

        private ICredentialsUpdater CreateCredentialsUpdater()
        {
            return new CredentialsUpdater(
                CreateCredentialsWriter(),
                CreateCredentialsReader());
        }

        private IWriter<Credentials> CreateCredentialsWriter()
        { 
            return new EfWriter<Credentials>(
                    CreateDbContext());
        }

        private DbContext CreateDbContext()
        {
            return new EBanaContext(
                new BCryptHash());
        }

        private IHash CreateHash()
        {
            return new BCryptHash();
        }
    }
}
