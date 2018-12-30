using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBana.Domain.Models
{
	[Table("TypeEpi")]
	public class TypeEpi
    {
		[Key]
        [Column("IdTypeEpi")]
        public int IdTypeEpi { get; set; }

        [Column("Libelle")]
        public string Libelle { get; set; }

        public override bool Equals(object obj)
        {
            var epi = obj as TypeEpi;
            return epi != null &&
                   IdTypeEpi == epi.IdTypeEpi &&
                   Libelle == epi.Libelle;
        }

        public override string ToString()
		{
			return Libelle;
		}
	}
}
