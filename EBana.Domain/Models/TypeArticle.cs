using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBana.Domain.Models
{
	[Table("TypeArticle")]
	public class TypeArticle
	{		
		[Key]
        [Column("IdTypeArticle")]
        public int IdTypeArticle { get; set; }
		
        [Column("Libelle")]
        public string Libelle { get; set; }

        public override string ToString()
		{
			return Libelle;
		}
	}
}
