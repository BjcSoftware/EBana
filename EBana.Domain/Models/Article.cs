namespace EBana.Domain.Models
{
    public class Article
    {
        protected Article()
        { }

        public Article(
            ReferenceArticle reference,
            string libelle,
            string localisation,
            double? quantite,
            string infosSupplementaires)
        {
            Reference = reference;
            Libelle = libelle;
            Localisation = localisation;
            Quantite = quantite;
            InfosSupplementaires = infosSupplementaires;
        }

        public Article(Article autre)
        {
            Id = autre.Id;
            Reference = autre.Reference;
            Libelle = autre.Libelle;
            Localisation = autre.Localisation;
            Quantite = autre.Quantite;
            InfosSupplementaires = autre.InfosSupplementaires;
        }

        public int Id { get; private set; }

        public ReferenceArticle Reference { get; private set; }

        public string Libelle { get; private set; }

        public string Localisation { get; private set; }

        public double? Quantite { get; private set; }

        public string InfosSupplementaires { get; private set; }
    }
}