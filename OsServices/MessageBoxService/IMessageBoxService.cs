namespace OsServices.Dialog
{
    public interface IMessageBoxService
    {
        /// <summary>
        /// Afficher une Message Box.
        /// </summary>
        /// <param name="title">Titre de la message box</param>
        /// <param name="message">Message contenu dans la message box</param>
        /// <param name="button">Type de bouton présent dans la message box</param>
        void Show(string title, string message, System.Windows.MessageBoxButton button);
    }
}
