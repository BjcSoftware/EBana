namespace EBana.Domain.Updater
{
    public class UpdateArticles
    {
        public UpdateArticles(string source = "")
        {
            UpdateSource = source;
        }

        public string UpdateSource { get; set; }
    }
}
