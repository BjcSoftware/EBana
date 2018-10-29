/*
 * Par Benjamin Cararo
 * Email: benjamin.cararo@bjcperso.fr
 */

using System;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;

namespace EBana.Models
{
	/// <summary>
	/// Description of TypeArticle.
	/// </summary>
	[Table(Name = "TypeArticle")]
	public class TypeArticle
	{		
		[Key]
        [Column(Name = "IdTypeArticle")]
        public int IdTypeArticle { get; set; }
		
        [Column(Name = "Libelle")]
        public String Libelle { get; set; }

        public override string ToString()
		{
			return Libelle;
		}

	}
}
