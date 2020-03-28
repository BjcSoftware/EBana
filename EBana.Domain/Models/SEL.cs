namespace EBana.Domain.Models
{
	/// <summary>
	/// Représente un article SEL
	/// </summary>
	public class Sel : Article
	{
        protected Sel()
        { }

        public Sel(
            ReferenceArticle reference,
            string libelle,
            string localisation,
            double? quantite,
            string infosSupplementaires)
            : base(
                  reference,
                  libelle,
                  localisation,
                  quantite,
                  infosSupplementaires)
        {
        }

        public Sel(Article article)
            : base(article)
        {
        }
	}
}