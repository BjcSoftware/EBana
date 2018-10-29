using System;
using System.Data.Linq.Mapping;

namespace EBana.Models
{
    [Table(Name = "Banalise")]
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

    	[Column(Name = "LienFlu")]
    	public String LienFlu { get; set; }
	}
}