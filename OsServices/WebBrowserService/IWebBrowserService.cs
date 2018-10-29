namespace OsServices.Web
{
    public interface IWebBrowserService
    {
        /// <summary>
        /// Ouvrir un navigateur à l'URL spécifié
        /// </summary>
        void Open(string url);
    }
}
