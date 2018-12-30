using System.ComponentModel.DataAnnotations.Schema;

namespace EBana.Domain.Models
{
    [Table("Banalise")]
    public class Banalise : Article
	{	
        public Banalise()
        {
        }

        public Banalise(Article article)
            : base(article)
        {
        }

        public Banalise(Banalise autre)
            : base(autre)
        {
            LienFlu = autre.LienFlu;
        }

    	[Column("LienFlu")]
    	public string LienFlu { get; set; }
	}
}