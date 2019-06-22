using EBana.Domain.Commands;
using EBana.Services.Command;

namespace EBana.WindowsService
{
    public class WindowsWebBrowserService : ICommandService<OpenWebPage>
    {
        public void Execute(OpenWebPage command)
        {
            string url = command.Url;

            System.Diagnostics.Process.Start(url);
        }
    }
}
