using EBana.Services.Web;

namespace EBana.WindowsServices.Web
{
    public class WebBrowserService : IWebBrowserService
    {
        public void Open(string url)
        {
            System.Diagnostics.Process.Start(url);
        }
    }
}
