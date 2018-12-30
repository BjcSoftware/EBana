using System.ComponentModel.DataAnnotations.Schema;

namespace EBana.Domain.Models
{
	[Table("EPI")]
	public class EPI : Banalise
	{
        public EPI()
        {

        }

        public EPI(Banalise banalise)
            : base(banalise)
        {
        }

        public virtual TypeEpi TypeEpi { get; set; }
	}
}