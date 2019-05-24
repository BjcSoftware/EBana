namespace EBana.Excel
{
    /// <summary>
    /// Fait la correspondence entre les numéros de champs d'un enregistrement et les champs d'un RawArticle.
    /// </summary>
    public class ArticleFieldToRecordFieldMapping
    {
        // doit être mis à jour en cas de changement dans le mapping
        public readonly uint ColumnCount = 10;

        public readonly int Ref = 0;
        public readonly int Label = 1;
        public readonly int Division = 2;
        public readonly int MagasinId = 3;
        public readonly int Localisation = 4;
        public readonly int Quantity = 5;
        public readonly int Flu = 6;
        public readonly int AdditionalInfos = 7;
        public readonly int EpiIdentifier = 8;
        public readonly int EpiLabel = 9;
    }
}
