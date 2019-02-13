using EBana.Domain.Models;
using System.Collections.Generic;

namespace EBana.Excel
{
	public class RawArticle
	{
		// partie commune à tous les articles
		public int IdArticle { get; set; }
		public string Ref { get; set; }
		public string Libelle { get; set; }
		public string Localisation { get; set; }
		public double Quantite { get; set; }
		public int? IdMagasin { get; set; }
		public string InfosSupplementaires { get; set; }
		
		// partie spécifique au banalisé
		public string LienFlu { get; set; }
		
		// partie spécifique aux EPIs
		public TypeEpi TypeEpi { get; set; }

		public override bool Equals(object obj)
		{
			var article = obj as RawArticle;
			return article != null &&
				   IdArticle == article.IdArticle &&
				   Ref == article.Ref &&
				   Libelle == article.Libelle &&
				   Localisation == article.Localisation &&
				   Quantite == article.Quantite &&
				   EqualityComparer<int?>.Default.Equals(IdMagasin, article.IdMagasin) &&
				   InfosSupplementaires == article.InfosSupplementaires &&
				   LienFlu == article.LienFlu &&
				   EqualityComparer<TypeEpi>.Default.Equals(TypeEpi, article.TypeEpi);
		}

		public override int GetHashCode()
		{
			var hashCode = -577979777;
			hashCode = hashCode * -1521134295 + IdArticle.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Ref);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Libelle);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Localisation);
			hashCode = hashCode * -1521134295 + Quantite.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(IdMagasin);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(InfosSupplementaires);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LienFlu);
			hashCode = hashCode * -1521134295 + EqualityComparer<TypeEpi>.Default.GetHashCode(TypeEpi);
			return hashCode;
		}
	}
}
