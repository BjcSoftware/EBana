namespace OsServices.Web
{
    public class WebBrowserService : IWebBrowserService
    {
        public void Open(string url)
        {
            System.Diagnostics.Process.Start(url);
        }
    }
}
