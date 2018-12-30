using System.ComponentModel.DataAnnotations.Schema;

namespace EBana.Domain.Models
{
	/// <summary>
	/// Représente un article SEL
	/// </summary>
	[Table("SEL")]
	public class SEL : Article
	{
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public SEL()
        {

        }

        public SEL(Article article)
            : base(article)
        {
        }
	}
}