namespace EBana.Domain.Models
{
    public class Epi : Banalise
    {
        public TypeEpi TypeEpi { get; private set; }

        protected Epi()
        { }

        public Epi(
            ReferenceArticle reference,
            string libelle,
            string localisation,
            double? quantite,
            string infosSupplementaires,
            string lienFlu,
            TypeEpi typeEpi)
            : base(
                reference,
                libelle,
                localisation,
                quantite,
                infosSupplementaires,
                lienFlu)
        {
            TypeEpi = typeEpi;
        }

        public Epi(
            Banalise banalise, 
            TypeEpi typeEpi)
            : base(banalise)
        {
            TypeEpi = typeEpi;
        }
    }
}