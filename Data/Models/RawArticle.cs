namespace EBana.Models
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
	}
}
