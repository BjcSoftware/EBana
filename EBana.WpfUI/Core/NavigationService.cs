using System;
using EBana.PresentationLogic.Core;
using FirstFloor.ModernUI.Windows.Controls;

namespace EBana.WpfUI.Core
{
    public class NavigationService : INavigationService
    {
        private readonly ModernWindow window;

        public NavigationService(ModernWindow window)
        {
            if (window == null)
                throw new ArgumentNullException(nameof(window));

            this.window = window;
        }

        public void NavigateTo(string destination)
        {
            window.ContentSource = new Uri(destination, UriKind.Relative);
        }
    }
}
