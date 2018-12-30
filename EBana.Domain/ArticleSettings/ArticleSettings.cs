namespace EBana.Domain
{
    public class ArticleSettings : IArticleSettings
    {
        public int GetIdMagasinBanalise()
        {
            return idMagasinBanalise;
        }

        public int GetIdMagasinSEL()
        {
            return idMagasinSEL;
        }

        private const int idMagasinBanalise = 1001;
        private const int idMagasinSEL = 6001;
    }
}
