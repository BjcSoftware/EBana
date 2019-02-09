namespace EBana.Domain
{
    public class ArticleSettings : IArticleSettings
    {
        public int IdMagasinBanalise => m_IdMagasinBanalise;

        public int IdMagasinSEL => m_IdMagasinSEL;

        private const int m_IdMagasinBanalise = 1001;
        private const int m_IdMagasinSEL = 6001;
    }
}
