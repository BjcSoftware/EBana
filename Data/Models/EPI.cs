﻿using System.Data.Linq.Mapping;

namespace EBana.Models
{
	[Table(Name = "EPI")]
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