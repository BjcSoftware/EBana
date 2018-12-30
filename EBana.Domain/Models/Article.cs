using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBana.Domain.Models
{
    [Table("Article")]
    public class Article
    {
        public Article()
        { }

        public Article(Article autre)
        {
            Id = autre.Id;
            Ref = autre.Ref;
            Libelle = autre.Libelle;
            Localisation = autre.Localisation;
            Quantite = autre.Quantite;
            InfosSupplementaires = autre.InfosSupplementaires;
        }

        [Key]
        [Column("Id")]
        public int Id { get; set; }
        
        [Column("Ref")]
        public string Ref { get; set; }
        
        [Column("Libelle")]
        public string Libelle { get; set; }

        [Column("Localisation")]
        public string Localisation { get; set; }

        [Column("Quantite")]
        public double? Quantite { get; set; }
        
        [Column("InfosSupplementaires")]
        public string InfosSupplementaires { get; set; }
    }
}