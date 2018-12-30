namespace EBana.Services.Dialog
{
    public enum DialogButton { Ok, OkCancel, YesNo, YesNoCancel };

    public interface IMessageBoxDialogService
    {
        /// <summary>
        /// Afficher une Message Box.
        /// </summary>
        /// <param name="title">Titre de la message box</param>
        /// <param name="message">Message contenu dans la message box</param>
        /// <param name="button">Type de bouton présent dans la message box</param>
        void Show(string title, string message, DialogButton button);
    }
}
