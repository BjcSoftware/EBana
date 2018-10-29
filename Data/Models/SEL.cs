/*
 * Par Benjamin Cararo
 * Email: benjamin.cararo@bjcperso.fr
 */
 
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace EBana.Models
{
	/// <summary>
	/// Représente un article SEL
	/// </summary>
	[Table(Name = "SEL")]
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