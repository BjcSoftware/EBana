namespace EBana.Domain
{
    public class ArticleSettings : IArticleSettings
    {
        public int IdMagasinBanalise => mIdMagasinBanalise;

        public int IdMagasinSEL => mIdMagasinSEL;

        private const int mIdMagasinBanalise = 1001;
        private const int mIdMagasinSEL = 6001;
    }
}
