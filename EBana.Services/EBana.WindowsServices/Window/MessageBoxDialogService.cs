using EBana.Services.Dialog;
using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.Generic;
using System.Windows;

namespace EBana.WindowsServices.Dialog
{
    public class MessageBoxDialogService : IMessageBoxDialogService
    {
        public void Show(string title, string message, DialogButton button)
        {
            ModernDialog.ShowMessage(message, title, dialogButtonMapper[button]);
        }

        readonly Dictionary<DialogButton, MessageBoxButton> dialogButtonMapper =
            new Dictionary<DialogButton, MessageBoxButton>()
        {
            { DialogButton.Ok, MessageBoxButton.OK },
            { DialogButton.OkCancel, MessageBoxButton.OKCancel },
            { DialogButton.YesNo, MessageBoxButton.YesNo },
            { DialogButton.YesNoCancel, MessageBoxButton.YesNoCancel }
        };
    }
}
