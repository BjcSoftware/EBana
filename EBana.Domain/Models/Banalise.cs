namespace EBana.Domain.Models
{
    public class Banalise : Article
    {
        public string LienFlu { get; private set; }

        protected Banalise()
        { }

        public Banalise(
            ReferenceArticle reference,
            string libelle,
            string localisation,
            double? quantite,
            string infosSupplementaires,
            string lienFlu)
            : base(
                  reference,
                  libelle,
                  localisation,
                  quantite,
                  infosSupplementaires)
        {
            LienFlu = lienFlu;
        }

        public Banalise(
            Article article,
            string lienFlu)
            : base(article)
        {
            LienFlu = lienFlu;
        }

        public Banalise(Banalise autre)
            : base(autre)
        {
            LienFlu = autre.LienFlu;
        }
    }
}