using System;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;
using SQLite.CodeFirst;

namespace EBana.Models
{
    [Table(Name = "Article")]
    public class Article
    {
        public Article()
        { }

        public Article(Article autre)
        {
            IdArticle = autre.IdArticle;
            Ref = autre.Ref;
            Libelle = autre.Libelle;
            Localisation = autre.Localisation;
            Quantite = autre.Quantite;
            InfosSupplementaires = autre.InfosSupplementaires;
        }

        [Key]
        [Column(Name = "IdArticle")]
        public int IdArticle { get; set; }
        
        [Column(Name = "Ref")]
        [Collate(CollationFunction.NoCase)]
        public String Ref { get; set; }
        
        [Column(Name = "Libelle")]
        [Collate(CollationFunction.NoCase)]
        public String Libelle { get; set; }

        [Column(Name = "Localisation")]
        public String Localisation { get; set; }

        [Column(Name = "Quantite")]
        public Double? Quantite { get; set; }
        
        [Column(Name = "InfosSupplementaires")]
        public String InfosSupplementaires { get; set; }
    }
}