using FirstFloor.ModernUI.Windows;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EBana.WpfUI.Core
{
    class MyContentLoader : IContentLoader
    {
        private readonly PageComposer composer;

        public MyContentLoader(PageComposer composer)
        {
            if (composer == null)
                throw new ArgumentNullException(nameof(composer));

            this.composer = composer;
        }

        public async Task<object> LoadContentAsync(Uri uri, CancellationToken cancellationToken)
        {
            Page view = await Task<Page>.Factory.StartNew(() =>
            {
                return composer.CreatePage(uri.OriginalString);
            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            return view;
        }
    }
}
