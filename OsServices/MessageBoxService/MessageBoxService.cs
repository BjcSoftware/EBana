using FirstFloor.ModernUI.Windows.Controls;

namespace OsServices.Dialog
{
    public class MessageBoxService : IMessageBoxService
    {
        public void Show(string title, string message, System.Windows.MessageBoxButton button)
        {
            ModernDialog.ShowMessage(message, title, button);
        }
    }
}
