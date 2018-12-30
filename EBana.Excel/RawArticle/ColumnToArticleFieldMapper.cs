namespace EBana.Excel
{
    /// <summary>
    /// Fait la correspondence entre les numéros de colonnes d'une feuille Excel et les champs d'un Article.
    /// </summary>
    public class ArticleFieldToColumnMapping
    {
        public int Ref = 0;
        public int Label = 1;
        public int Division = 2;
        public int MagasinId = 3;
        public int Localisation = 4;
        public int Quantity = 5;
        public int Flu = 6;
        public int AdditionalInfos = 7;
        public int EpiIdentifier = 8;
        public int EpiType = 9;
    }
}
