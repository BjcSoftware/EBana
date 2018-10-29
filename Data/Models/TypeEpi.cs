using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;

namespace EBana.Models
{
	[Table(Name = "TypeEpi")]
	public class TypeEpi
    {
		[Key]
        [Column(Name = "IdTypeEpi")]
        public int IdTypeEpi { get; set; }

        [Column(Name = "Libelle")]
        public string Libelle { get; set; }

        public override string ToString()
		{
			return Libelle;
		}
	}
}
